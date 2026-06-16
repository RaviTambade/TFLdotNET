using MaxNewYorkInsurance.Models;

namespace MaxNewYorkInsurance.Departments;

public class RenewalDepartment
{
    public void OnRenewPolicy(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Renewal Department");
        Console.WriteLine($"Policy Renewal Requested: {policyNumber}");

        Console.WriteLine("Checking policy validity...");

        // Simulated validation logic
        bool isEligibleForRenewal = true;

        if (isEligibleForRenewal)
        {
            Console.WriteLine("Policy is eligible for renewal.");
            Console.WriteLine("Renewal process initiated.");
        }
        else
        {
            Console.WriteLine("Policy is not eligible for renewal.");
        }

        Console.WriteLine("====================================");
    }

    public void OnRenewalReminderSent(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Renewal Reminder Service");
        Console.WriteLine($"Reminder sent for Policy: {policyNumber}");
        Console.WriteLine("Customer notified via Email/SMS.");
        Console.WriteLine("====================================");
    }

    public void OnPolicyExpired(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Policy Expiry Handler");
        Console.WriteLine($"Policy expired: {policyNumber}");
        Console.WriteLine("Grace period started (if applicable).");
        Console.WriteLine("====================================");
    }

    public void OnRenewalCompleted(Policy policy)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Renewal Completed");
        Console.WriteLine($"Policy Renewed: {policy.PolicyNumber}");
        Console.WriteLine("New policy term activated.");
        Console.WriteLine("====================================");
    }

    public void OnRenewalRejected(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Renewal Rejected");
        Console.WriteLine($"Policy: {policyNumber}");
        Console.WriteLine("Reason: Policy not eligible or payment pending.");
        Console.WriteLine("====================================");
    }

    public void OnPremiumDue(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Premium Due Alert");
        Console.WriteLine($"Policy: {policyNumber}");
        Console.WriteLine("Payment reminder generated.");
        Console.WriteLine("====================================");
    }
}