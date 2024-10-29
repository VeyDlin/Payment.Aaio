using Payment.Aaio.Types;
using System.Collections.Concurrent;

namespace Payment.Aaio;


public class PaymentWaiter {
    public delegate Task SuccessEvent(string orderId, PaymentInfo info);
    public delegate Task ErrorEvent(string orderId, string error);

    private AaioMerchant merchant;
    public TimeSpan timeout { get; set; } = TimeSpan.FromDays(3);
    public TimeSpan startDelay { get; set; } = TimeSpan.FromSeconds(1);
    public TimeSpan maxDelay { get; set; } = TimeSpan.FromMinutes(5);

    private ConcurrentDictionary<string, (SuccessEvent success, ErrorEvent error)> events = new();
    private ConcurrentDictionary<string, CancellationTokenSource> tokens = new();

    public event SuccessEvent? outsideSuccess;
    public event ErrorEvent? outsideError;



    public PaymentWaiter(AaioMerchant merchant) {
        this.merchant = merchant;
    }





    public void Startup(IEnumerable<string> orderIds, TimeSpan runSscatter) {
        var random = new Random();

        foreach (var orderId in orderIds) {
            Task.Run(async () => {
                var delay = random.Next(0, (int)runSscatter.TotalMilliseconds);
                await Task.Delay(delay);

                AddWaiter(orderId,
                   async (id, info) => {
                       if (outsideSuccess is not null) {
                           await outsideSuccess(id, info);
                       }
                   },
                   async (id, error) => {
                       if (outsideError is not null) {
                           await outsideError(id, error);
                       }
                   }
                );
            });
        }
    }






    public bool Cancel(string orderId) {
        CancellationTokenSource cts;
        if (tokens.TryGetValue(orderId, out cts!)) {
            cts.Cancel();
            return true;
        }
        return false;
    }





    public bool AddWaiter(string orderId, SuccessEvent success, ErrorEvent error) {
        if (!events.TryAdd(orderId, (success, error))) {
            return false;
        }

        Task.Run(async () => {
            var startTime = DateTime.UtcNow;
            var cts = tokens.GetOrAdd(orderId, k => new());
            var delay = (int)startDelay.TotalMilliseconds;

            try {
                while (true) {
                    var elapsedTime = DateTime.UtcNow - startTime;
                    var remainingTime = timeout - elapsedTime;

                    if (remainingTime <= TimeSpan.Zero) {
                        await CallErrorAsync(orderId, "timeout");
                        return;
                    }

                    delay = Math.Max(delay, (int)maxDelay.TotalMilliseconds);
                    delay = Math.Min(delay, (int)remainingTime.TotalMilliseconds);

                    await Task.Delay(delay, cts.Token);
                    delay *= 2;

                    try {
                        var info = await merchant.GetPaymentInfoAsync(orderId);

                        if (info.IsExpired()) {
                            await CallErrorAsync(orderId, info.status);
                            return;
                        }

                        if (info.IsSuccess()) {
                            await CallSuccessAsync(orderId, info);
                            return;
                        }
                    } catch { }
                }
            } catch (TaskCanceledException) {
                await CallErrorAsync(orderId, "waiter-cancel");
            }
        });

        return true;
    }





    private async Task CallSuccessAsync(string orderId, PaymentInfo info) {
        tokens.TryRemove(orderId, out _);

        (SuccessEvent success, ErrorEvent error) calls;
        if (events.TryRemove(orderId, out calls)) {
            await calls.success(orderId, info);
        }
    }





    private async Task CallErrorAsync(string orderId, string error) {
        tokens.TryRemove(orderId, out _);

        (SuccessEvent success, ErrorEvent error) calls;
        if (events.TryRemove(orderId, out calls)) {
            await calls.error(orderId, error);
        }
    }
}
