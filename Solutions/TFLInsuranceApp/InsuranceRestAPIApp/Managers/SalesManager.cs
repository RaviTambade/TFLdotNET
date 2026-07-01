namespace MaxNewYorkInsurance.Managers;

using MaxNewYorkInsurance.Agents;
using MaxNewYorkInsurance.Models;

public class SalesManager
{
    public event AccountsAgent? policyPurchased;
    public event SalesAgent? policyQuoted;
    public event SalesNumberAgent? policyCancelled;
    public event SalesAgent? policyUpdated;
    public event LeadAgent? leadGenerated;
    public event SalesNumberAgent? discountOffered;

    public void PurchasePolicy(Policy policy)
    =>  policyPurchased?.Invoke(policy);
  

    public void CancelPolicy(string policyNumber)
     =>policyCancelled?.Invoke(policyNumber);

    public void UpdatePolicy(Policy policy)
    =>policyUpdated?.Invoke(policy);

    public void GenerateLead(Customer customer)
    =>leadGenerated?.Invoke(customer);

    public void OfferDiscount(string policyNumber)
    =>discountOffered?.Invoke(policyNumber);
    
}