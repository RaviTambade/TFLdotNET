using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Repositories;

namespace MaxNewYorkInsurance.Departments;

public class AccountsDepartment
{
    public void RecordPayment(Policy policy)
    {

        PolicyRepository repo=new PolicyRepository();

        List<Policy> policies = repo.GetAllPolicies();
        policies.Add(policy);
        repo.SaveAllPolicies(policies);
        Console.WriteLine("Payment recorded successfully.");
    }
}