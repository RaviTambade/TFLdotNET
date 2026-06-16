using   MaxNewYorkInsurance.Models;
 
namespace MaxNewYorkInsurance.Agents;

// Sales Delegates
public delegate void SalesAgent(Policy policy);
public delegate void DiscountAgent(string policyNumber);
public delegate void LeadAgent(Customer customer);

// Customer Delegates
public delegate void CustomerAgent(Customer customer);

// Accounts & Premium Delegates
public delegate void AccountsAgent(Policy policy);
public delegate void PremiumAgent(Premium premium);
public delegate void PaymentReceiptAgent(Premium premium);

// Renewal Delegates
public delegate void RenewalAgent(string policyNumber);

// Claims Delegates
public delegate void ClaimsAgent(Claim claim);

// Policy Administration Delegates
public delegate void PolicyAgent(Policy policy);
public delegate void PolicyNumberAgent(string policyNumber);

// Notification Delegates
public delegate void NotificationAgent(string message);
public delegate void PolicyNotificationAgent(string policyNumber);

// Audit & Compliance Delegates
public delegate void AuditAgent(string policyNumber);
public delegate void ComplianceAgent(Customer customer);

// Agent Management Delegates
public delegate void AgentManagementAgent(Agent agent);

// Reporting Delegates
public delegate void ReportAgent(DateTime generatedOn);