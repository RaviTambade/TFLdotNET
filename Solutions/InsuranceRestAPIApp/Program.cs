using MaxNewYorkInsurance.Managers;
using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Services;
using MaxNewYorkInsurance.Departments;
using MaxNewYorkInsurance.Agents;
using MaxNewYorkInsurance.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();


app.MapGet("/api/policies", () =>
{
        PolicyRepository repo=new   PolicyRepository();
        return repo.GetAllPolicies(); 
});


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
app.Run();