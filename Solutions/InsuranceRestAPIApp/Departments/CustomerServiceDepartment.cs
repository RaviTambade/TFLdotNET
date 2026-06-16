using MaxNewYorkInsurance.Models;

namespace MaxNewYorkInsurance.Departments;

public class CustomerServiceDepartment
{
    public void OnCustomerRegistered(Customer customer)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Customer Service Department");
        Console.WriteLine($"Customer '{customer.FullName + customer.LastName}' registered successfully.");
        Console.WriteLine("Welcome email and onboarding process initiated.");
        Console.WriteLine("====================================");
    }

    public void OnCustomerUpdated(Customer customer)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Customer profile updated.");
        Console.WriteLine($"Customer: {customer.FullName + customer.LastName}");
        Console.WriteLine("====================================");
    }

    public void OnCustomerDeactivated(int customerId)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Customer with Id {customerId} has been deactivated.");
        Console.WriteLine("====================================");
    }

    public void OnPolicyDocumentSent(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Policy document sent for Policy No: {policyNumber}");
        Console.WriteLine("====================================");
    }

    public void OnRenewalReminderSent(string policyNumber)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Renewal reminder sent for Policy No: {policyNumber}");
        Console.WriteLine("====================================");
    }

    public void OnKycVerified(Customer customer)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"KYC verification completed for {customer.FullName + customer.LastName}");
        Console.WriteLine("====================================");
    }

    public void OnCustomerQueryRaised(string query)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Customer Query");
        Console.WriteLine(query);
        Console.WriteLine("Support ticket created successfully.");
        Console.WriteLine("====================================");
    }

    public void OnFeedbackReceived(string feedback)
    {
        Console.WriteLine("====================================");
        Console.WriteLine("Customer Feedback Received");
        Console.WriteLine(feedback);
        Console.WriteLine("====================================");
    }
}