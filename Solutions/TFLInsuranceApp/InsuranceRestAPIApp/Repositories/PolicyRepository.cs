
using MaxNewYorkInsurance.Models;
using System.Text.Json;

namespace MaxNewYorkInsurance.Repositories;

public class PolicyRepository
{
    
    public List<Policy> GetAllPolicies()
    {
        string fileName = @"A:\TAP\GitHub\DotNet\insuranceapp\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\policies.json";
        string jsonString = File.ReadAllText(fileName);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<Policy>? policies = JsonSerializer.Deserialize<List<Policy>>(jsonString, options);
        return policies;
    }


    public bool SaveAllPolicies(List<Policy> policies)
    {
        bool status = false;
        string fileName = @"A:\TAP\GitHub\DotNet\insuranceapp\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\policies.json";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        string jsonString = JsonSerializer.Serialize(policies, options);
        File.WriteAllText(fileName, jsonString);
        status = true;
        return status;


        
    }

}