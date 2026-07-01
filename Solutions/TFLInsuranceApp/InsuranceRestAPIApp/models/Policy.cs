using System.Text.Json.Serialization;
namespace MaxNewYorkInsurance.Models;


//POCO class  (POJO)
//Plain Old  CLR Object 

public class Policy
{
    public string PolicyNumber { get; set; }   //auto property
    public string CustomerName { get; set; }
    public string PolicyType { get; set; }
    public decimal PolicyAmount { get; set; }
    public bool IsRenewed { get; set; } = false;

    public string Status{get;set;}="Active";

    public Policy()
    {
        
    }
}
