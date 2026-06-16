using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Repositories;

namespace MaxNewYorkInsurance.Departments;


//Accounts Bookkeeping  logic

public class AccountsDepartment
{
    PremiumRepository PayPremium = new PremiumRepository();
    public void OnPurchasePolicy(Policy policy)
    {

        PolicyRepository repo=new PolicyRepository();
        List<Policy> policies = repo.GetAllPolicies();
        policies.Add(policy);
        repo.SaveAllPolicies(policies);
         
        Console.WriteLine("Payment recorded successfully.");
    }


     public void OnPayPremiums(Premium premium)
    {
        List<Premium> premiums = PayPremium.GetAllPremimum();
        premiums.Add(premium);
        PayPremium.SaveAllPremium(premiums);
        Console.WriteLine("Premium is paid Successfully");
    }
}