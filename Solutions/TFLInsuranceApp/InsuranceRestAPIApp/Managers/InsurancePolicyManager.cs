namespace MaxNewYorkInsurance.Managers;
using MaxNewYorkInsurance.Agents;
using MaxNewYorkInsurance.Models;

public class InsurancePolicyManager
{

    // Sales Events
public event AccountsAgent? policyPurchased;
public event SalesAgent? policyQuoted;
public event SalesAgent? policyUpdated;
public event SalesAgent? policyCancelled;
public event SalesAgent? leadGenerated;
public event SalesAgent? discountOffered;

// Customer Events
public event CustomerAgent? customerRegistered;
public event CustomerAgent? customerUpdated;
public event CustomerAgent? customerDeactivated;
public event CustomerAgent? kycVerified;

// Premium & Accounts Events
public event PremiumAgent? premiumPaid;
public event PremiumAgent? premiumRefunded;
public event PremiumAgent? premiumReminderGenerated;
public event PremiumAgent? lateFeeApplied;
public event AccountsAgent? paymentReceiptGenerated;

// Renewal Events
public event RenewalAgent? policyRenewed;
public event RenewalAgent? renewalReminderSent;
public event RenewalAgent? renewalExpired;

// Claim Events
public event ClaimsAgent? claimRegistered;
public event ClaimsAgent? claimVerified;
public event ClaimsAgent? claimApproved;
public event ClaimsAgent? claimRejected;
public event ClaimsAgent? claimSettled;
public event ClaimsAgent? fraudCheckRequested;

// Policy Administration Events
public event PolicyAgent? policyIssued;
public event PolicyAgent? policyAssigned;
public event PolicyAgent? policyDocumentGenerated;
public event PolicyAgent? nomineeUpdated;
public event PolicyAgent? beneficiaryChanged;

// Notification Events
public event NotificationAgent? emailSent;
public event NotificationAgent? smsSent;
public event NotificationAgent? policyDocumentSent;

// Audit & Compliance Events
public event AuditAgent? policyAudited;
public event AuditAgent? complianceChecked;
public event AuditAgent? regulatoryReportGenerated;

// Agent & Employee Events
public event AgentManagementAgent? agentAssigned;
public event AgentManagementAgent? agentCommissionCalculated;
public event AgentManagementAgent? agentCommissionPaid;

// Reporting Events
public event ReportAgent? dailyReportGenerated;
public event ReportAgent? monthlyReportGenerated;
public event ReportAgent? annualReportGenerated;


    // public void PurchasePolicy(Policy policy)
    // {   
    //     policyPurchased?.Invoke(policy);
    // }

    // public void RegisterClaim(Claim claim)
    // {
    //     claimRegistered?.Invoke(claim);
    // }

    // public void  RenewPolicy(string policyNumber)
    // {
    //     policyRenewed?.Invoke(policyNumber);   
    // }
 
    // public void PayPremium(Premium premium)
    // {
    //     premiumPaid.Invoke(premium);
    //     Console.WriteLine("Premium is paid Successfully");
    // } 

//     public void RegisterCustomer(Customer customer)
// {
//     customerRegistered?.Invoke(customer);
// }

// public void UpdateCustomer(Customer customer)
// {
//     customerUpdated?.Invoke(customer);
// }

// public void DeactivateCustomer(int customerId)
// {
//     customerDeactivated?.Invoke(customerId);
// }

// public void CancelPolicy(string policyNumber)
// {
//     policyCancelled?.Invoke(policyNumber);
// }

// public void UpdatePolicy(Policy policy)
// {
//     policyUpdated?.Invoke(policy);
// }

// public void AssignPolicyAgent(string policyNumber, Agent agent)
// {
//     policyAssigned?.Invoke(policyNumber, agent);
// }

// public void GeneratePremiumReminder(string policyNumber)
// {
//     premiumReminderGenerated?.Invoke(policyNumber);
// }

// public void RefundPremium(Premium premium)
// {
//     premiumRefunded?.Invoke(premium);
// }

// public void ApplyLateFee(string policyNumber)
// {
//     lateFeeApplied?.Invoke(policyNumber);
// }


// public void ApproveClaim(Claim claim)
// {
//     claimApproved?.Invoke(claim);
// }

// public void RejectClaim(Claim claim)
// {
//     claimRejected?.Invoke(claim);
// }

// public void SettleClaim(Claim claim)
// {
//     claimSettled?.Invoke(claim);
// }

// public void GenerateLead(Customer customer)
// {
//     leadGenerated?.Invoke(customer);
// }


// ==================================================================================================
// public void LaunchCampaign(string campaignName)
// {
//     marketingCampaignLaunched?.Invoke(campaignName);
// }
// ==================================================================================================


    // public void OfferDiscount(string policyNumber)
    // {
    //     discountOffered?.Invoke(policyNumber);
    // }

    // public void SendPolicyDocument(string policyNumber)
    // {
    //     policyDocumentSent?.Invoke(policyNumber);
    // }

    // public void SendRenewalReminder(string policyNumber)
    // {
    //     renewalReminderSent?.Invoke(policyNumber);
    // }

    // public void SendPaymentReceipt(Premium premium)
    // {
    //     paymentReceiptSent?.Invoke(premium);
    // }


    // public void AuditPolicy(string policyNumber)
    // {
    //     policyAudited?.Invoke(policyNumber);
    // }

    // public void VerifyKYC(Customer customer)
    // {
    //     kycVerified?.Invoke(customer);
    // }

    // public void DetectFraud(Claim claim)
    // {
    //     fraudCheckRequested?.Invoke(claim);
    // }


}