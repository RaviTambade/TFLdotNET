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
}


    // Check whether a policy is eligible for renewal
    public bool IsEligibleForRenewal(string policyNumber)
    {
        return false;
    }

    // Calculate the renewal premium
    public decimal CalculateRenewalPremium(string policyNumber)
    {
        return 0;
    }

    // Extend the expiry date after renewal
    public bool ExtendPolicyValidity(string policyNumber)
    {
        return false;
    }

    // Mark policy as renewed
    public bool MarkAsRenewed(string policyNumber)
    {
        return false;
    }

    // Send renewal reminder to the customer
    public void SendRenewalReminder(string policyNumber)
    {
    }

    // Send renewal confirmation after successful renewal
    public void SendRenewalConfirmation(string policyNumber)
    {
    }

    // Get policies that are due for renewal
    public List<Policy> GetPoliciesDueForRenewal()
    {
        return new List<Policy>();
    }

    // Get expired policies
    public List<Policy> GetExpiredPolicies()
    {
        return new List<Policy>();
    }

    // Get renewed policies
    public List<Policy> GetRenewedPolicies()
    {
        return new List<Policy>();
    }

    // Check whether a policy has expired
    public bool IsPolicyExpired(string policyNumber)
    {
        return false;
    }

    // Update renewal date
    public bool UpdateRenewalDate(string policyNumber, DateTime renewalDate)
    {
        return false;
    }

    // Cancel a pending renewal request
    public bool CancelRenewal(string policyNumber)
    {
        return false;
    }

    // Generate renewal report
    public void GenerateRenewalReport()
    {
    }

    // Generate receipt for the renewal payment
    public void GenerateRenewalReceipt(string policyNumber)
    {
    }

    // Process renewal payment
    public bool ProcessRenewalPayment(string policyNumber, decimal amount)
    {
        return false;
    }
}