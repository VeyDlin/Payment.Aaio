using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class PayoffSbpBanks {
    [Required]
    public required List<SbpBank> list { get; set; }
}



public class SbpBank {
    [Required]
    public required string bankId { get; set; }

    [Required]
    public required string bankName { get; set; }

    [Required]
    public required string bankIcon { get; set; }
}