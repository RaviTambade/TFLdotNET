namespace MaxNewYorkInsurance.Models;

public class Agent
{
    public int AgentId { get; set; }

    public string AgentCode { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string MobileNumber { get; set; } = string.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public string Branch { get; set; } = string.Empty;

    public string Designation { get; set; } = string.Empty;

    public decimal CommissionRate { get; set; }

    public decimal TotalCommissionEarned { get; set; }

    public DateTime DateOfJoining { get; set; }

    public bool IsActive { get; set; }

    public override string ToString()
    {
        return $"{AgentCode} - {FullName} ({Designation})";
    }
}


/*


Agent agent = new Agent
{
    AgentId = 1,
    AgentCode = "AGT1001",
    FullName = "Ramesh Sharma",
    Email = "ramesh.sharma@example.com",
    MobileNumber = "9876543210",
    LicenseNumber = "LIC-2026-1001",
    Branch = "Pune",
    Designation = "Senior Insurance Advisor",
    CommissionRate = 0.10m,
    TotalCommissionEarned = 250000m,
    DateOfJoining = new DateTime(2023, 5, 15),
    IsActive = true
};


*/
