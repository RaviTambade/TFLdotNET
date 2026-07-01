namespace MaxNewYorkInsurance.Managers;

using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Agents;

public class PremiumManager
{
    public event PremiumAgent? premiumPaid;
    public event PremiumNumberAgent? premiumReminderGenerated;
    public event PremiumAgent? premiumRefunded;
    public event PremiumNumberAgent? lateFeeApplied;
    public event PremiumAgent? paymentReceiptSent;
    public void PayPremium(Premium premium)
    =>  premiumPaid.Invoke(premium);

    public void GeneratePremiumReminder(string policyNumber)
      => premiumReminderGenerated?.Invoke(policyNumber);

    public void RefundPremium(Premium premium)
    =>premiumRefunded?.Invoke(premium);


    public void ApplyLateFee(string policyNumber)
    =>lateFeeApplied?.Invoke(policyNumber);

    public void SendPaymentReceipt(Premium premium)
 => paymentReceiptSent?.Invoke(premium);

}
