using MaxNewYorkInsurance.Repositories;
using  MaxNewYorkInsurance.Models;
using  MaxNewYorkInsurance.Managers;
using MaxNewYorkInsurance.Agents;
namespace MaxNewYorkInsurance.Departments;

public class RenewalDepartment
{
 PolicyRepository repo =new PolicyRepository();

 
public bool OnRenewPolicy(string policyNumber)
{
    List<Policy> policies = repo.GetAllPolicies();

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
        repo.SaveAllPolicies(policies);
    }
    return status;
}}