
using MaxNewYorkInsurance.Models;
using System.Text.Json;

namespace MaxNewYorkInsurance.Repositories;

public class PremiumRepository
{
public List<Premium> GetAllPremimum()
    {
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\premiums.json";
        string jsonString = File.ReadAllText(fileName);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<Premium>? premiums = JsonSerializer.Deserialize<List<Premium>>(jsonString, options);
        return premiums;
    }


    public bool SaveAllPremium(List<Premium> premiums)
    {
        bool status = false;
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\premiums.json";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        string jsonString = JsonSerializer.Serialize(premiums, options);
        File.WriteAllText(fileName, jsonString);
        status = true;
        return status;
    }


}