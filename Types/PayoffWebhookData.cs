using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class PayoffWebhookData {
    [Required]
    public required string id { get; set; }

    [Required]
    [JsonProperty("my_id")]
    public required string myId { get; set; }

    [Required]
    [JsonProperty("my_id")]
    public required string method { get; set; }

    [Required]
    public required string bank { get; set; }

    [Required]
    public required string wallet { get; set; }

    [Required]
    public required float amount { get; set; }

    [Required]
    [JsonProperty("amount_in_currency")]
    public required float amountInCurrency { get; set; }

    [Required]
    [JsonProperty("amount_currency")]
    public required string amountCurrency { get; set; }

    [Required]
    [JsonProperty("amount_rate")]
    public required float amountRate { get; set; }

    [Required]
    [JsonProperty("amount_down")]
    public required float amountDown { get; set; }

    [Required]
    public required float commission { get; set; }

    [Required]
    [Range(0, 1)]
    [JsonProperty("commission_type")]
    public required int commissionType { get; set; }

    [Required]
    [RegularExpression("^(cancel|success)$")]
    public required string status { get; set; }

    [JsonProperty("cancel_message")]
    public string? cancelMessage { get; set; }

    [Required]
    public required string date { get; set; }

    [Required]
    [JsonProperty("complete_date")]
    public required string completeDate { get; set; }

    [Required]
    public required string sign { get; set; }
}