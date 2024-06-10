using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class PayoffMethods {
    [Required]
    public required Dictionary<string, PayoffMethod> list { get; set; }
}



public class PayoffMethod {
    [Required]
    public required string name { get; set; }

    [Required]
    public required float min { get; set; }

    [Required]
    public required float max { get; set; }

    [Required]
    [JsonProperty("commission_percent")]
    public required float commissionPercent { get; set; }

    [Required]
    [JsonProperty("commission_sum")]
    public required float commissionSum { get; set; }
}