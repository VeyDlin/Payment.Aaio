using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class PayoffInfo {
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
    [JsonProperty("amount_down")]
    public required float amountDown { get; set; }

    [Required]
    public required float commission { get; set; }

    [Required]
    [Range(0, 1)]
    [JsonProperty("commission_type")]
    public required int commissionType { get; set; }

    [Required]
    [RegularExpression("^(in_process|cancel|success)$")]
    public required string status { get; set; }

    [JsonProperty("cancel_message")]
    public string? cancelMessage { get; set; }

    [Required]
    public required string date { get; set; }

    [JsonProperty("complete_date")]
    public string? completeDate { get; set; }

    public bool IsSuccess() => status == "success";

    public bool IsInProcess() => status == "in_process";

    public bool IsCanceled() => status == "cancel";
}