
using MaxNewYorkInsurance.Models;
using System.Text.Json;

namespace MaxNewYorkInsurance.Repositories;

public class ClaimsRepository
{
    
    public List<Claim> GetAllRegisterClaim()
    {
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\claims.json";
        string jsonString = File.ReadAllText(fileName);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<Claim>? RegisterClaims = JsonSerializer.Deserialize<List<Claim>>(jsonString, options);
        return RegisterClaims;
    }

    public bool SaveRegisterClaim(List<Claim> claims)
    {
        bool status = false;
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\claims.json";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        string jsonString = JsonSerializer.Serialize(claims, options);
        File.WriteAllText(fileName, jsonString);
        status = true;
        return status;
    }
}