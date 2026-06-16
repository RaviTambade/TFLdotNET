using MaxNewYorkInsurance.Models;

namespace MaxNewYorkInsurance.Services;

public class EmailNotificationService
{
    public void OnEmailSent(string message)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("EMAIL SERVICE");
        Console.WriteLine($"Email sent with message: {message}");
        Console.WriteLine("====================================");
    }

    public void OnPolicyDocumentSent(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("EMAIL SERVICE - POLICY DOCUMENT");
        Console.WriteLine($"Policy document emailed for Policy No: {policyNumber}");
        Console.WriteLine("Attachment: Policy PDF generated and sent.");
        Console.WriteLine("====================================");
    }

    public void OnRenewalReminderSent(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("EMAIL SERVICE - RENEWAL REMINDER");
        Console.WriteLine($"Renewal reminder email sent for Policy No: {policyNumber}");
        Console.WriteLine("Message: Please renew your policy before expiry.");
        Console.WriteLine("====================================");
    }

    public void OnPaymentReceiptGenerated(Premium premium)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("EMAIL SERVICE - PAYMENT RECEIPT");
        Console.WriteLine($"Receipt emailed to Customer: {premium.CustomerName}");
        Console.WriteLine($"Policy: {premium.PolicyNumber}");
        Console.WriteLine($"Amount Paid: {premium.AmountPaid}");
        Console.WriteLine($"Transaction ID: {premium.TransactionId}");
        Console.WriteLine("====================================");
    }

    public void OnCustomerWelcomeEmail(Customer customer)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("EMAIL SERVICE - WELCOME EMAIL");
        Console.WriteLine($"Welcome email sent to: {customer.FirstName +  customer.LastName}");
        Console.WriteLine($"Email: {customer.Email}");
        Console.WriteLine("Onboarding instructions shared successfully.");
        Console.WriteLine("====================================");
    }

    public void OnClaimStatusEmail(Claim claim)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("EMAIL SERVICE - CLAIM STATUS UPDATE");
        Console.WriteLine($"Email sent to: {claim.CustomerName}");
        Console.WriteLine($"Claim ID: {claim.ClaimId}");
        Console.WriteLine($"Status: {claim.Status}");
        Console.WriteLine("====================================");
    }

    public void OnGenericNotification(string subject, string body)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("EMAIL SERVICE - NOTIFICATION");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");
        Console.WriteLine("Email delivered successfully.");
        Console.WriteLine("====================================");
    }
}