using TFLECommerce_May2024.Models;

namespace TFLECommerce_May2024.Repositories
{
    public class CustomerRepository
    {
        public CustomerRepository() { }
        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer { Id = 12, Name = "Chirajivee", Phone = "987273423", Email = "chiranjivee.g@gmail.com" });
            customers.Add(new Customer { Id = 13, Name = "Manish", Phone = "9882376423", Email = "manish.g@gmail.com" });
            customers.Add(new Customer { Id = 14, Name = "Sameer", Phone = "9882347523", Email = "sameer.g@gmail.com" });
            customers.Add(new Customer { Id = 15, Name = "Raghu", Phone = "98823423673", Email = "raghu.g@gmail.com" });
            customers.Add(new Customer { Id = 16, Name = "Samay", Phone = "98823423423", Email = "rajan.g@gmail.com" });
            return customers;
        }
    }
}
