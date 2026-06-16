namespace MaxNewYorkInsurance.Managers;
using MaxNewYorkInsurance.Agents;
using MaxNewYorkInsurance.Models;

public class InsurancePolicyManager
{
    //define evnets
    public event  AccountsAgent? policyPurchased;
    public event ClaimsAgent? claimRegistered;
    public event  PremiumAgent? premiumPaid;
    public event RenewalAgent? policyRenewed;

    public void PurchasePolicy(Policy policy)
    {   
        policyPurchased?.Invoke(policy);
    }

    public void RegisterClaim(Claim claim)
    {
        claimRegistered?.Invoke(claim);
    }

    public void  RenewPolicy(string policyNumber)
    {
        policyRenewed?.Invoke(policyNumber);   
    }
 
    public void PayPremium(Premium premium)
    {
        premiumPaid.Invoke(premium);
        Console.WriteLine("Premium is paid Successfully");
    } 
}