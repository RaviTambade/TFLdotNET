using MaxNewYorkInsurance.Managers;
using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Services;
using MaxNewYorkInsurance.Departments;
using MaxNewYorkInsurance.Agents;
using MaxNewYorkInsurance.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapPost("/api/policies/purchase", (Policy policy) =>
{  
        InsurancePolicyManager insruanceManager=new InsurancePolicyManager();
        SalesDepartment sales=new SalesDepartment();
        AccountsDepartment accounts=new AccountsDepartment();
        insruanceManager.policyPurchased+=sales.OnPolicyPurchased;
        insruanceManager.PurchasePolicy(policy);
        return " Policy Purchased Successfully";
});

app.MapPost("/api/policies/paypremium", (Premium premium) =>
{
  
        InsurancePolicyManager insruanceManager=new InsurancePolicyManager();
        AccountsDepartment accounts=new AccountsDepartment();
        SalesDepartment sales=new SalesDepartment();
        ClaimDepartment claims=new ClaimDepartment();
        RenewalDepartment renewals=new RenewalDepartment();

        EmailNotificationService emailSvc=new EmailNotificationService();
               
        insruanceManager.policyPurchased+=sales.OnPolicyPurchased;



        insruanceManager.claimRegistered+=claims.OnClaimRegistered;


        insruanceManager.PayPremium(premium);
        return " Preimum paid succefully";
  
});

app.MapPost("/api/policies/registerclaim", (Claim claim) =>
{
  
        InsurancePolicyManager insruanceManager=new InsurancePolicyManager();
        AccountsDepartment accounts=new AccountsDepartment();
        SalesDepartment sales=new SalesDepartment();
        ClaimDepartment claims=new ClaimDepartment();
        RenewalDepartment renewals=new RenewalDepartment();
        EmailNotificationService emailSvc=new EmailNotificationService();
        insruanceManager.policyPurchased+=sales.OnPolicyPurchased;
        insruanceManager.claimRegistered+=claims.OnClaimRegistered;
        insruanceManager.RegisterClaim(claim);
        return "Claim is Register";

});

app.MapPost("/api/policies/renew", (Policy policy) =>
{
  
        InsurancePolicyManager insruanceManager=new InsurancePolicyManager();
        AccountsDepartment accounts=new AccountsDepartment();
        SalesDepartment sales=new SalesDepartment();
        ClaimDepartment claims=new ClaimDepartment();
        RenewalDepartment renewals=new RenewalDepartment();

        EmailNotificationService emailSvc=new EmailNotificationService();
        insruanceManager.policyPurchased+=sales.OnPolicyPurchased;
        insruanceManager.claimRegistered+=claims.OnClaimRegistered;
        return Results.Ok("Policy Renewed Successfully");
});

 
// Get all policies
app.MapGet("/api/policies", () =>
{
    PolicyRepository repo = new PolicyRepository();
    return repo.GetAllPolicies();
});

// Purchase a new policy
app.MapPost("/api/policies/purchase", (Policy policy) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    SalesDepartment sales = new SalesDepartment();
    AccountsDepartment accounts = new AccountsDepartment();
    manager.policyPurchased += sales.OnPolicyPurchased;
    manager.PurchasePolicy(policy);

    return Results.Ok("Policy purchased successfully.");
});

// Register a customer
app.MapPost("/api/customers/register", (Customer customer) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    CustomerServiceDepartment customerService =
        new CustomerServiceDepartment();

    manager.customerRegistered += customerService.OnCustomerRegistered;

    manager.RegisterCustomer(customer);

    return Results.Ok("Customer registered successfully.");
});

// Pay premium
app.MapPost("/api/policies/paypremium", (Premium premium) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    AccountsDepartment accounts = new AccountsDepartment();

    manager.premiumPaid += accounts.OnPremiumPaid;

    manager.PayPremium(premium);

    return Results.Ok("Premium paid successfully.");
});

// Register claim
app.MapPost("/api/policies/registerclaim", (Claim claim) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    ClaimDepartment claims = new ClaimDepartment();

    manager.claimRegistered += claims.OnClaimRegistered;

    manager.RegisterClaim(claim);

    return Results.Ok("Claim registered successfully.");
});

// Approve claim
app.MapPost("/api/claims/approve", (Claim claim) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    ClaimDepartment claims = new ClaimDepartment();

    manager.claimApproved += claims.OnClaimApproved;

    manager.ApproveClaim(claim);

    return Results.Ok("Claim approved successfully.");
});

// Reject claim
app.MapPost("/api/claims/reject", (Claim claim) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    ClaimDepartment claims = new ClaimDepartment();

    manager.claimRejected += claims.OnClaimRejected;

    manager.RejectClaim(claim);

    return Results.Ok("Claim rejected successfully.");
});

// Settle claim
app.MapPost("/api/claims/settle", (Claim claim) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    ClaimDepartment claims = new ClaimDepartment();
    AccountsDepartment accounts = new AccountsDepartment();

    manager.claimSettled += claims.OnClaimSettled;
    manager.claimSettled += accounts.OnClaimSettled;

    manager.SettleClaim(claim);

    return Results.Ok("Claim settled successfully.");
});

// Renew policy
app.MapPost("/api/policies/renew/{policyNumber}", (string policyNumber) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    RenewalDepartment renewals = new RenewalDepartment();

    manager.policyRenewed += renewals.OnRenewPolicy;

    manager.RenewPolicy(policyNumber);

    return Results.Ok("Policy renewed successfully.");
});

// Cancel policy
app.MapPost("/api/policies/cancel/{policyNumber}", (string policyNumber) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    manager.CancelPolicy(policyNumber);

    return Results.Ok("Policy cancelled successfully.");
});

// Send renewal reminder
app.MapPost("/api/policies/reminder/{policyNumber}", (string policyNumber) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    EmailNotificationService emailService =
        new EmailNotificationService();

    manager.renewalReminderSent += emailService.OnRenewalReminderSent;

    manager.SendRenewalReminder(policyNumber);

    return Results.Ok("Renewal reminder sent.");
});

// Generate policy document
app.MapPost("/api/policies/document/{policyNumber}", (string policyNumber) =>
{
    InsurancePolicyManager manager = new InsurancePolicyManager();

    EmailNotificationService emailService =
        new EmailNotificationService();

    manager.policyDocumentSent += emailService.OnPolicyDocumentSent;

    manager.SendPolicyDocument(policyNumber);

    return Results.Ok("Policy document sent.");
});
 

app.Run();