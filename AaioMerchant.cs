using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Payment.Aaio.Types;

namespace Payment.Aaio;


public class AaioMerchant {
    private AaioClient aaio;
    public string merchantId { get; private set; }
    public string secretKey1 { get; private set; }
    public string? secretKey2 { get; private set; }



    public AaioMerchant(AaioClient aaio, string merchantId, string secretKey1, string? secretKey2 = null) {
        this.aaio = aaio;
        this.merchantId = merchantId;
        this.secretKey1 = secretKey1;
        this.secretKey2 = secretKey2;
    }


    public Task<PaymentMethods> GetPaymentMethodsAsync() => aaio.GetPaymentMethodsAsync(merchantId);

    public string CreatePayment(PaymentParameters parameters) => aaio.CreatePayment(merchantId, parameters, secretKey1);

    public Task<PaymentInfo> GetPaymentInfoAsync(string orderId) => aaio.GetPaymentInfoAsync(merchantId, orderId);

    public Task<PaymentInfo> WaitPaymentAsync(string orderId, int? timeoutSec = null, CancellationTokenSource? cts = null) {
        return aaio.WaitPaymentAsync(merchantId, orderId, timeoutSec, cts);
    }

    public bool IsValidPayment(PaymentWebhookData payment) {
        if (secretKey2 is null) {
            throw new ArgumentNullException(nameof(payment));
        }
        return aaio.IsValidPayment(payment, secretKey2);
    }
}