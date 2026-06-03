namespace  MaxNewYorkInsurance;
using MaxNewYorkInsurance.Services;
using MaxNewYorkInsurance.Departments;
using MaxNewYorkInsurance.Managers;

public class Program {

    public static void Main(string [] args)
    {

        UIManager uiMgr=new UIManager();
        uiMgr.ShowMenu();
        int choice=uiMgr.GetChoice();

        InsurancePolicyManager insruanceManager=new InsurancePolicyManager();
        AccountsDepartment accounts=new AccountsDepartment();
        SalesDepartment sales=new SalesDepartment();
        ClaimDepartment claims=new ClaimDepartment();
        RenewalDepartment renewals=new RenewalDepartment();

        EmailNotificationService emailSvc=new EmailNotificationService();
               
        insruanceManager.policyPurchased+=sales.OnPolicyPurchased;
        insruanceManager.policyPurchased+=emailSvc.SendMessage;

        insruanceManager.premiumPaid+=accounts.RecordPayment;
        insruanceManager.claimRegistered+=claims.OnClaimRegistered;
        insruanceManager.premiumPaid+=renewals.OnPolicyRenewed;

      
        switch (choice)
        { 
            case 1:
                //1. Purchase Policy
                insruanceManager.PurchasePolicy();
            break;
            case 2:
                //2. Pay Premium
                insruanceManager.PayPremium();
            break;
            case 3:
                 //3. Register Claim
                 insruanceManager.RegisterClaim();
            break;
            case 4:
                 //4. Renew Policy
                 insruanceManager.RenewPolicy();
            break;
            case 5:
                Environment.Exit(0);
            break;
        }
    }
}