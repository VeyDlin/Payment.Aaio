using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class PaymentWebhookData {
    [Required]
    [JsonProperty("merchant_id")]
    public required string merchantId { get; set; }

    [Required]
    [JsonProperty("invoice_id")]
    public required string invoiceId { get; set; }

    [Required]
    [JsonProperty("order_id")]
    public required string orderId { get; set; }

    [Required]
    public required float amount { get; set; }

    [Required]
    [RegularExpression("^(RUB|UAH|EUR|USD)$")]
    public required string currency { get; set; }

    [Required]
    public required float profit { get; set; }

    [Required]
    public required float commission { get; set; }

    [Required]
    [JsonProperty("commission_client")]
    public required float commissionClient { get; set; }

    [Required]
    [JsonProperty("commission_type")]
    public required string commissionType { get; set; }

    [Required]
    public required string sign { get; set; }

    [Required]
    public required string method { get; set; }

    [Required]
    public required string desc { get; set; }

    [Required]
    public required string email { get; set; }

    [Required]
    [JsonProperty("us_key")]
    public required string usKey { get; set; }

    public bool IsValid(string secretKey2) => AaioClient.GenerateSign([merchantId, amount.ToString(), currency, secretKey2, orderId]) == sign;
}