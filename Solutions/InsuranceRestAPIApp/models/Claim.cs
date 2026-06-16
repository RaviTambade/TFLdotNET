namespace MaxNewYorkInsurance.Models;

public class Claim
{
    // Unique identifier for the claim
    public int ClaimId { get; set; }

    // Policy against which the claim is raised
    public string PolicyNumber { get; set; } = string.Empty;

    // Customer details
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    // Claim information
    public DateTime ClaimDate { get; set; }

    public string ClaimType { get; set; } = string.Empty;
    // Examples: Health, Life, Vehicle, Travel

    public string Reason { get; set; } = string.Empty;

    public decimal ClaimAmount { get; set; }

    public decimal ApprovedAmount { get; set; }

    // Status: Registered, Under Review, Approved, Rejected, Settled
    public string Status { get; set; } = "Registered";

    // Optional remarks from claim officer
    public string Remarks { get; set; } = string.Empty;

    // Settlement information
    public DateTime? SettlementDate { get; set; }

    public override string ToString()
    {
        return $"ClaimId: {ClaimId}, " +
               $"Policy: {PolicyNumber}, " +
               $"Customer: {CustomerName}, " +
               $"Amount: {ClaimAmount:C}, " +
               $"Status: {Status}";
    }
}


/*



/*
Claim claim = new Claim
{
    ClaimId = 1001,
    PolicyNumber = "POL-2026-001",
    CustomerId = 101,
    CustomerName = "Ravi Tambade",
    ClaimDate = DateTime.Now,
    ClaimType = "Health",
    Reason = "Hospitalization due to illness",
    ClaimAmount = 50000m,
    ApprovedAmount = 45000m,
    Status = "Approved",
    Remarks = "Documents verified successfully",
    SettlementDate = DateTime.Now.AddDays(3)
};
*/


