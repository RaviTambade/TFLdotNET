namespace MaxNewYorkInsurance.Models;

public class Claim
{
    public string? PolicyNumber { get; set; }
    public decimal ClaimAmount { get; set; }
    public string? Reason { get; set; }
}