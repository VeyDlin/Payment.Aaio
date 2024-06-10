using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class PayoffRates {
    [Required]
    [JsonProperty("USD")]
    public required float usd { get; set; }

    [Required]
    [JsonProperty("UAH")]
    public required float uah { get; set; }

    [Required]
    [JsonProperty("USDT")]
    public required float usdt { get; set; }

    [Required]
    [JsonProperty("BTC")]
    public required float btc { get; set; }
}