
using MaxNewYorkInsurance.Models;
using System.Text.Json;

namespace MaxNewYorkInsurance.Repositories;

public class ClaimsRepository
{
    
    public List<Policy> GetAllClaims()    {
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\policies.json";
        string jsonString = File.ReadAllText(fileName);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<Policy>? policies = JsonSerializer.Deserialize<List<Policy>>(jsonString, options);
        return policies;
    }


    public bool SaveAllClaims(List<Claim> claims)
    {
        bool status = false;
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\policies.json";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        string jsonString = JsonSerializer.Serialize(claims, options);
        File.WriteAllText(fileName, jsonString);
        status = true;
        return status;
    }

}