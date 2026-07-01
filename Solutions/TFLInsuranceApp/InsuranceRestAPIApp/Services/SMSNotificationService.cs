using MaxNewYorkInsurance.Models;
namespace MaxNewYorkInsurance.Services;

public class SMSNotificationService
{
    public void OnSmsSent(string message)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("SMS SERVICE");
        Console.WriteLine($"SMS sent: {message}");
        Console.WriteLine("Delivered successfully to mobile network.");
        Console.WriteLine("====================================");
    }

    public void OnPolicyDocumentSent(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("SMS SERVICE - POLICY ALERT");
        Console.WriteLine($"Policy document notification sent for Policy No: {policyNumber}");
        Console.WriteLine("SMS: Your policy document is now available.");
        Console.WriteLine("====================================");
    }

    public void OnRenewalReminderSent(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("SMS SERVICE - RENEWAL REMINDER");
        Console.WriteLine($"Renewal reminder sent for Policy No: {policyNumber}");
        Console.WriteLine("SMS: Your policy is due for renewal soon. Please renew to stay covered.");
        Console.WriteLine("====================================");
    }

    public void OnPaymentReceived(Premium premium)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("SMS SERVICE - PAYMENT CONFIRMATION");
        Console.WriteLine($"SMS sent to {premium.CustomerName}");
        Console.WriteLine($"Policy: {premium.PolicyNumber}");
        Console.WriteLine($"Amount Received: {premium.AmountPaid}");
        Console.WriteLine($"Transaction ID: {premium.TransactionId}");
        Console.WriteLine("SMS: Payment received successfully. Thank you!");
        Console.WriteLine("====================================");
    }

    public void OnCustomerRegistered(Customer customer)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("SMS SERVICE - WELCOME MESSAGE");
        Console.WriteLine($"Welcome SMS sent to {customer.FirstName+ customer.LastName}");
        Console.WriteLine($"Mobile: {customer.MobileNumber}");
        Console.WriteLine("SMS: Welcome to Max Insurance. Your account is active.");
        Console.WriteLine("====================================");
    }

    public void OnClaimStatusUpdate(Claim claim)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("SMS SERVICE - CLAIM UPDATE");
        Console.WriteLine($"SMS sent to {claim.CustomerName}");
        Console.WriteLine($"Claim ID: {claim.ClaimId}");
        Console.WriteLine($"Status: {claim.Status}");
        Console.WriteLine("SMS: Your claim status has been updated.");
        Console.WriteLine("====================================");
    }

    public void OnGenericSms(string mobileNumber, string message)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("SMS SERVICE - GENERIC MESSAGE");
        Console.WriteLine($"To: {mobileNumber}");
        Console.WriteLine($"Message: {message}");
        Console.WriteLine("SMS delivered successfully.");
        Console.WriteLine("====================================");
    }
}