using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class Balance {
    [Required]
    public required float balance { get; set; }

    [Required]
    public required float referral { get; set; }

    [Required]
    public required float hold { get; set; }
}