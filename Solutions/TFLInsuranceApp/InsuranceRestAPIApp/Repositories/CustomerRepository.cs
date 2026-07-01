
using MaxNewYorkInsurance.Models;
using System.Text.Json;

namespace MaxNewYorkInsurance.Repositories;

public class CustomerRepository
{

    public List<Customer> GetAllCustomers()
    {
        string fileName = @"A:\TAP\GitHub\DotNet\insuranceapp\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\customers.json";
        string jsonString = File.ReadAllText(fileName);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        List<Customer>? customers = JsonSerializer.Deserialize<List<Customer>>(jsonString, options);
        return customers;
    }


    public bool SaveAllCustomers(List<Customer> customers)
    {
        bool status = false;
        string fileName = @"A:\TAP\GitHub\DotNet\insuranceapp\InsuranceRestAPIApp\InsuranceRestAPIApp\Data\customers.json";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        string jsonString = JsonSerializer.Serialize(customers, options);
        File.WriteAllText(fileName, jsonString);
        status = true;
        return status;
    }

}