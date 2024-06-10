namespace Payment.Aaio;


public struct PaymentParameters {
    public required float amount { get; set; }
    public required string orderId { get; set; }
    public string? description { get; set; }
    public string? method { get; set; }
    public string? email { get; set; }
    public string? referral { get; set; }
    public string? usKey { get; set; }
    public required string currency { get; set; }
    public string language { get; set; } = "en";

    public PaymentParameters() { }
}

