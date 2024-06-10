using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class PaymentInfo {
    [Required]
    public required string id { get; set; }

    [Required]
    [JsonProperty("order_id")]
    public required string orderId { get; set; }

    [Required]
    public required string desc { get; set; }

    [Required]
    [JsonProperty("merchant_id")]
    public required string merchantId { get; set; }

    [Required]
    [JsonProperty("merchant_domain")]
    public required string merchantDomain { get; set; }

    [Required]
    public required string method { get; set; }

    [Required]
    public required float amount { get; set; }

    [Required]
    [RegularExpression("^(RUB|UAH|EUR|USD)$")]
    public required string currency { get; set; }

    public float? profit { get; set; }

    public float? commission { get; set; }

    [JsonProperty("commission_client")]
    public float? commissionClient { get; set; }

    [JsonProperty("commission_type")]
    public string? commissionType { get; set; }

    public string? email { get; set; }

    [Required]
    [RegularExpression("^(in_process|success|expired|hold)$")]
    public required string status { get; set; }

    [Required]
    public required string date { get; set; }

    [Required]
    [JsonProperty("expired_date")]
    public required string expiredDate { get; set; }

    [JsonProperty("complete_date")]
    public string? completeDate { get; set; }

    [Required]
    public required List<string> usVars { get; set; }

    public bool IsSuccess() => status == "success";

    public bool IsInProcess() => status == "in_process";

    public bool IsOnHold() => status == "hold";

    public bool IsExpired() => status == "expired";
}