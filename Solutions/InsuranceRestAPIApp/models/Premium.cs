 
namespace MaxNewYorkInsurance.Models;

public class Premium
{
    // Unique identifier for the premium payment
    public int PremiumId { get; set; }

    // Associated policy information
    public int PolicyId { get; set; }

    public string PolicyNumber { get; set; } = string.Empty;

    // Customer information
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    // Payment details
    public decimal AmountPaid { get; set; }

    public DateTime PaymentDate { get; set; }

    // UPI, Credit Card, Debit Card, Net Banking, Cash, Cheque, etc.
    public string PaymentMode { get; set; } = string.Empty;

    // Transaction or reference number from the payment gateway/bank
    public string TransactionId { get; set; } = string.Empty;

    // Monthly, Quarterly, Half-Yearly, Annual
    public string PaymentFrequency { get; set; } = string.Empty;

    // Success, Pending, Failed, Refunded
    public string PaymentStatus { get; set; } = "Success";

    // Optional remarks
    public string Remarks { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"PremiumId: {PremiumId}, " +
               $"Policy: {PolicyNumber}, " +
               $"Customer: {CustomerName}, " +
               $"Amount: {AmountPaid:C}, " +
               $"Status: {PaymentStatus}";
    }
}
 
