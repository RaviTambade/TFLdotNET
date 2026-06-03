namespace MaxNewYorkInsurance.Managers;
using MaxNewYorkInsurance.Actions;

public class InsurancePolicyManager
{
    //evnets
    public event InsuranceAction policyPurchased;
    public event InsuranceAction claimRegistered;
    public event InsuranceAction premiumPaid;
    public event InsuranceAction policyRenewed;

    public void  PurchasePolicy()
    {
        policyPurchased.Invoke(); 
        Console.WriteLine("Policy Purchased Successfully");

    }
    public void RegisterClaim()
    {
        claimRegistered.Invoke();
        Console.WriteLine("Claim Registered Successfully");
    }

    public void  RenewPolicy()
    {
        policyRenewed.Invoke();
        Console.WriteLine(" Existing Policy renewed Successfully");
    }
      public void  PayPremium()
    {
        premiumPaid.Invoke();
        Console.WriteLine("Premium is paid Successfully");
    }
}