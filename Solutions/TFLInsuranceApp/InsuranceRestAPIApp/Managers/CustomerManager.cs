namespace MaxNewYorkInsurance.Managers;

using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Agents;

public class CustomerManager
{
    public event CustomerAgent? customerRegistered;
    public event CustomerAgent? customerUpdated;
    public event CustomerNumberAgent? customerDeactivated;
    public event CustomerAgent? kycVerified;

    public void RegisterCustomer(Customer customer)
        => customerRegistered?.Invoke(customer);

    public void UpdateCustomer(Customer customer)
        => customerUpdated?.Invoke(customer);

    public void DeactivateCustomer(int customerId)
        => customerDeactivated?.Invoke(customerId);

    public void VerifyKYC(Customer customer)
        => kycVerified?.Invoke(customer);
}