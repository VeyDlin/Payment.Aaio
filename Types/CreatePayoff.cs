using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class CreatePayoff {
    [Required]
    public required string id { get; set; }

    [Required]
    [JsonProperty("my_id")]
    public required string myId { get; set; }

    [Required]
    public required string method { get; set; }

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

    [JsonProperty("amount_rate")]
    public float? amountRate { get; set; }

    [Required]
    [JsonProperty("amount_down")]
    public required float amountDown { get; set; }

    [Required]
    public required float commission { get; set; }

    [Required]
    [JsonProperty("commission_type")]
    public required int commissionType { get; set; }

    [Required]
    [RegularExpression("^(in_process|cancel|success)$")]
    public required string status { get; set; }

    public bool IsSuccess() => status == "success";

    public bool IsInProcess() => status == "in_process";

    public bool IsCancelled() => status == "cancel";
}