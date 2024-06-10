using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class PaymentMethods {
    [Required]
    public required Dictionary<string, PaymentMethod> list { get; set; }
}



public class PaymentMethod {
    [Required]
    public required string name { get; set; }

    [Required]
    public required PaymentMethodAmounts min { get; set; }

    [Required]
    public required PaymentMethodAmounts max { get; set; }

    [Required]
    [JsonProperty("my_id")]
    public required float commissionType { get; set; }
}



public class PaymentMethodAmounts {
    [Required]
    [JsonProperty("RUB")]
    public required float rub { get; set; }

    [Required]
    [JsonProperty("UAH")]
    public required float uah { get; set; }

    [Required]
    [JsonProperty("USD")]
    public required float usd { get; set; }

    [Required]
    [JsonProperty("EUR")]
    public required float eur { get; set; }
}