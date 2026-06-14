namespace MaxNewYorkInsurance.Departments;

public class SalesDepartment
{
    public void OnPolicyPurchased(string policyNumber)
    {
        Console.WriteLine($"Policy Sold  {policyNumber}" );
    }
    
}