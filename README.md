```c#
using Newtonsoft.Json;
using Payment.Aaio;

void Print(object obj) => Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));


var aaio = new AaioClient("api key");
var merchant = aaio.CreateMerchant("merchant is", "secret 1");


// Retrieve payment methods
var paymentMethods = await merchant.GetPaymentMethodsAsync();
Print(paymentMethods);


// Retrieve IP addresses
var ips = await aaio.GetIpsAsync();
Print(ips);


// Create payment
var orderId = Guid.NewGuid().ToString();

var paymentUrl = merchant.CreatePayment(new() {
    amount = 100,                          
    orderId = orderId,                     
    description = "My order description",  
    method = "qiwi",                       
    email = "support@email.com",                  
    referral = "123456",             
    currency = "USD",                      
    language = "en"                        
});

Console.WriteLine(paymentUrl);


// Wait payment
var infoPayment = await merchant.WaitPaymentAsync(orderId, timeoutSec: 60 * 10); // Wait 10 minutes
if(!infoPayment.IsSuccess()) {
    throw new Exception($"Payment {orderId} error, status: {infoPayment.status}"); 
}
Console.WriteLine($"Payment {orderId} success");
Print(infoPayment);


// Retrieve balances
var balances = await aaio.GetBalancesAsync();
Print(balances);


// Retrieve payoff methods
var payoffMethods = await aaio.GetPayoffMethodsAsync();
Print(payoffMethods);


// Retrieve payoff rates
var payoffRates = await aaio.GetPayoffRatesAsync();
Print(payoffRates);


// Retrieve SBP banks for payoff
var payoffSbpBanks = await aaio.GetPayoffSbpBanksAsync();
Print(payoffSbpBanks);


// Create payoff
var payoff = await aaio.CreatePayoffAsync(
    method: "method",
    amount: 100,
    wallet: "wallet",
    payoffId: "payoffId", 
    commissionType: 0
);


var infoPayoff = await aaio.WaitPayoffAsync(payoff.id, "aaioId", timeoutSec: 60 * 10); // Wait 10 minutes
if (!infoPayoff.IsSuccess()) {
    throw new Exception($"Payoff {payoff.id} error, status: {infoPayoff.status}");
}
Console.WriteLine($"Payoff {payoff.id} success");
Print(infoPayoff);


Console.ReadKey();
```

