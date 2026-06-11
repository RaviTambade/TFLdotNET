namespace MaxNewYorkInsurance.Managers;

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using MaxNewYorkInsurance.Actions;
using MaxNewYorkInsurance.Models;
using Microsoft.AspNetCore.Routing.Constraints;

public class InsurancePolicyManager
{
    //evnets
    public event InsuranceAction? policyPurchased;
    public event InsuranceAction? claimRegistered;
    public event InsuranceAction? premiumPaid;
    public event InsuranceAction? policyRenewed;

    public void PurchasePolicy(Policy policy)
    {
        List<Policy> policies = GetAllPolicies();
        policies.Add(policy);
        this.SaveAllPolicies(policies);
        policyPurchased?.Invoke();


    }
    public void RegisterClaim(Claim claim)
    {
        List<Claim> claims = GetAllRegisterClaim();
        claims.Add(claim);
        this.SaveRegisterClaim(claims);
        claimRegistered?.Invoke();
        Console.WriteLine("Claim Registered Successfully");
    }

   public bool RenewPolicy(string policyNumber)
{
    List<Policy> policies = GetAllPolicies();

    bool status = false;

    foreach (Policy policy in policies)
    {
        if (policy.PolicyNumber == policyNumber)
        {
            policy.IsRenewed = true;
            status = true;
            break;
        }
    }

    if (status)
    {
        SaveAllPolicies(policies);
        policyRenewed?.Invoke();
    }

    return status;
}
    public void PayPremium(Premium premium)
    {
        List<Premium> premiums = GetAllPremimum();
        premiums.Add(premium);
        this.SaveAllPremium(premiums);
        policyPurchased.Invoke();
        premiumPaid.Invoke();
        Console.WriteLine("Premium is paid Successfully");
    }

    public List<Policy> GetAllPolicies()
    {
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\policies.json";
        string jsonString = File.ReadAllText(fileName);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<Policy>? policies = JsonSerializer.Deserialize<List<Policy>>(jsonString, options);
        return policies;
    }


    public bool SaveAllPolicies(List<Policy> policies)
    {
        bool status = false;
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\policies.json";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        string jsonString = JsonSerializer.Serialize(policies, options);
        File.WriteAllText(fileName, jsonString);
        status = true;
        return status;
    }

    public List<Premium> GetAllPremimum()
    {
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\payPremuim.json";
        string jsonString = File.ReadAllText(fileName);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<Premium>? premiums = JsonSerializer.Deserialize<List<Premium>>(jsonString, options);
        return premiums;
    }


    public bool SaveAllPremium(List<Premium> premiums)
    {
        bool status = false;
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\payPremuim.json";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        string jsonString = JsonSerializer.Serialize(premiums, options);
        File.WriteAllText(fileName, jsonString);
        status = true;
        return status;
    }


    public List<Claim> GetAllRegisterClaim()
    {
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\registerclaim.json";
        string jsonString = File.ReadAllText(fileName);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<Claim>? RegisterClaims = JsonSerializer.Deserialize<List<Claim>>(jsonString, options);
        return RegisterClaims;
    }

    public bool SaveRegisterClaim(List<Claim> claims)
    {
        bool status = false;
        string fileName = @"C:\TAP\MygitRepo\.NET\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\registerclaim.json";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        string jsonString = JsonSerializer.Serialize(claims, options);
        File.WriteAllText(fileName, jsonString);
        status = true;
        return status;
    }
}