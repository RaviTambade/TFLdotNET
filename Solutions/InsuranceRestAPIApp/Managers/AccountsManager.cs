namespace MaxNewYorkInsurance.Managers;

using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Agents;

public class AccountsManager
{
    public event PremiumAgent? premiumPaid;
    public event PremiumAgent? premiumRefunded;
    public event PremiumAgent? premiumReminderGenerated;
    public event PremiumAgent? lateFeeApplied;
    public event AccountsAgent? paymentReceiptGenerated;

    public void PayPremium(Premium premium)
    {
        premiumPaid?.Invoke(premium);
        Console.WriteLine("Premium paid successfully.");
    }

    public void RefundPremium(Premium premium)
        => premiumRefunded?.Invoke(premium);

    public void GeneratePremiumReminder(string policyNumber)
        => premiumReminderGenerated?.Invoke(policyNumber);

    public void ApplyLateFee(string policyNumber)
        => lateFeeApplied?.Invoke(policyNumber);
}