 
using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Repositories;

namespace MaxNewYorkInsurance.Departments;

public class AccountsDepartment
{
    public void OnPolicyPurchased(Policy policy)
    {
        PolicyRepository policyRepository= new PolicyRepository();
        List<Policy> policyList=policyRepository.GetAllPolicies();
        policyList.Add(policy);
        policyRepository.SaveAllPolicies(policyList);
        
        Console.WriteLine("====================================");
        Console.WriteLine("Accounts Department");
        Console.WriteLine($"New policy purchased: {policy.PolicyNumber}");
        Console.WriteLine("Financial records updated.");
        Console.WriteLine("====================================");
    }

    public void OnPolicyCancel(string policyno)
    {
        bool status=false;
        PolicyRepository policyRepository = new PolicyRepository();
        List<Policy> policyList = policyRepository.GetAllPolicies();
      
      foreach(Policy policy in policyList)
        {
            if(policy.PolicyNumber == policyno)
            {
                policy.Status = "Deactive";
                status = true;
                policyRepository.SaveAllPolicies(policyList);
            }
        }

        if(status)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("Accounts Department");
            Console.WriteLine($" policy Cancel: {policyno}");
            Console.WriteLine("Financial records updated.");
            Console.WriteLine("====================================");
        }
        else
        {
            Console.WriteLine("====================================");
            Console.WriteLine("Accounts Department");
            Console.WriteLine($"failed to Cancel policy: {policyno}");
            Console.WriteLine("Financial records updated.");
            Console.WriteLine("====================================");
        }
    }


    public void OnPremiumPaid(Premium premium)
    {

        PremiumRepository premiumRepository = new PremiumRepository();
        List<Premium> premiumList = premiumRepository.GetAllPremimum();
        premiumList.Add(premium);
        premiumRepository.SaveAllPremium(premiumList);
        Console.WriteLine("====================================");
        Console.WriteLine("Premium Payment Received");
        Console.WriteLine($"Policy Id      : {premium.PolicyId}");
        Console.WriteLine($"Amount Paid    : {premium.AmountPaid}");
        Console.WriteLine($"Payment Mode   : {premium.PaymentMode}");
        Console.WriteLine("Payment recorded successfully.");
        Console.WriteLine("====================================");
    }

    public void OnPremiumRefunded(Premium premium)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Premium Refund");
        Console.WriteLine($"Policy Id      : {premium.PolicyId}");
        Console.WriteLine($"Refund Amount  : {premium.AmountPaid}");
        Console.WriteLine("Refund processed successfully.");
        Console.WriteLine("====================================");
    }

    public void OnClaimApproved(Claim claim)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Claim Approved");
        Console.WriteLine($"Claim Id       : {claim.ClaimId}");
        Console.WriteLine("Awaiting settlement.");
        Console.WriteLine("====================================");
    }

    public void OnClaimSettled(Claim claim)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Claim Settlement");
        Console.WriteLine($"Claim Id         : {claim.ClaimId}");
        Console.WriteLine($"Approved Amount  : {claim.ApprovedAmount}");
        Console.WriteLine("Settlement payment released.");
        Console.WriteLine("====================================");
    }

    public void OnAgentCommissionCalculated(Agent agent)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Agent Commission");
        Console.WriteLine($"Agent            : {agent.FullName}");
        Console.WriteLine($"Commission Rate  : {agent.CommissionRate:P}");
        Console.WriteLine($"Total Earned     : {agent.TotalCommissionEarned}");
        Console.WriteLine("====================================");
    }

    public void OnPaymentReceiptGenerated(Premium premium)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Payment Receipt");
        Console.WriteLine($"Policy Id    : {premium.PolicyId}");
        Console.WriteLine($"Amount       : {premium.AmountPaid}");
        Console.WriteLine("Receipt generated successfully.");
        Console.WriteLine("====================================");
    }

    public void OnLateFeeApplied(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Late payment fee applied to policy {policyNumber}.");
        Console.WriteLine("====================================");
    }

    public void OnDailyReportGenerated(DateTime reportDate)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Daily financial report generated on {reportDate:d}.");
        Console.WriteLine("====================================");
    }

    public void OnMonthlyReportGenerated(DateTime reportDate)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Monthly financial report generated for {reportDate:MMMM yyyy}.");
        Console.WriteLine("====================================");
    }
}
 
