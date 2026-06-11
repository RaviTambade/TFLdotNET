
using System.Text.Json.Serialization;

namespace MaxNewYorkInsurance.Models;

 
public class Policy
{
    public string PolicyNumber { get; set; }
    public string CustomerName { get; set; }
    public string PolicyType { get; set; }
    public decimal PremiumAmount { get; set; }

    public bool IsRenewed { get; set; } = false;

    public Policy()
    {
        
    }
}
