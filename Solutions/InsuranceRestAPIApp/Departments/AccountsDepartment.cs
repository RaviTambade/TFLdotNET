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

     // Collect premium payment
    public bool CollectPremium(string policyNumber, decimal amount)
    {
        return false;
    }

    // Record a payment transaction
    public bool RecordPayment(Premium payment)
    {
        return false;
    }

    // Generate payment receipt
    public void GenerateReceipt(string policyNumber)
    {
    }

    // Process claim settlement payment
    public bool ProcessClaimSettlement(int claimId, decimal amount)
    {
        return false;
    }

    // Issue refund to customer
    public bool ProcessRefund(string policyNumber, decimal amount)
    {
        return false;
    }

    // Calculate agent commission
    public decimal CalculateAgentCommission(int agentId)
    {
        return 0;
    }

    // Pay commission to an agent
    public bool PayAgentCommission(int agentId)
    {
        return false;
    }

    // Generate invoice for premium payment
    public void GenerateInvoice(string policyNumber)
    {
    }

    // Verify payment status
    public bool VerifyPayment(string transactionId)
    {
        return false;
    }

    // Get payment history for a policy
    public List<Premium> GetPaymentHistory(string policyNumber)
    {
        return new List<Premium>();
    }

    // Get pending premium payments
    public List<Policy> GetPendingPremiums()
    {
        return new List<Policy>();
    }

    // Get overdue premium payments
    public List<Policy> GetOverduePremiums()
    {
        return new List<Policy>();
    }

    // Calculate total premium collected
    public decimal GetTotalPremiumCollected()
    {
        return 0;
    }

    // Calculate total claim payouts
    public decimal GetTotalClaimPayouts()
    {
        return 0;
    }

    // Generate daily financial report
    public void GenerateDailyReport()
    {
    }

    // Generate monthly financial report
    public void GenerateMonthlyReport()
    {
    }

    // Generate annual financial report
    public void GenerateAnnualReport()
    {
    }

    // Export financial transactions
    public void ExportTransactions()
    {
    }

    // Reconcile accounts
    public bool ReconcileAccounts()
    {
        return false;
    }

    // Close accounting period
    public void CloseAccountingPeriod(DateTime closingDate)
    {
    }

}