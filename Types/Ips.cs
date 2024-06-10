using System.ComponentModel.DataAnnotations;

namespace Payment.Aaio.Types;


public class Ips {
    [Required]
    public required List<string> list { get; set; }
}