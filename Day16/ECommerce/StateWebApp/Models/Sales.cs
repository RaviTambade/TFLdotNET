
using System.Collections.Generic;

 namespace StateWebApp.Models;
    public class SalesRepository{
        public List<Product> products=new List<Product>();
        public List<Customer> customers=new List<Customer>();

        public SalesRepository(){
            products = FillProducts();
            customers = FillCustomers();     
        }
        private List<Product> FillProducts(){
            products.Add(new Product() { Title = "Gerbera", Description = "Wedding Flower", Quantity = 2450 });
            products.Add(new Product() { Title = "Lilly", Description = "Decoration Flower", Quantity = 9900 });
            products.Add(new Product() { Title = "Jasmine", Description = "Smelling Flower", Quantity = 12600 });
            products.Add(new Product() { Title = "Marigold", Description = "Festival Flower", Quantity = 3500 });
            products.Add(new Product() { Title = "Carnation", Description = "Beautiful Flower", Quantity = 1500 });
            return products;
        }
        private List<Customer> FillCustomers(){
            customers.Add(new Customer() { Name = "Rajan Patil", Age = 28, Location = "Mumbai" });
            customers.Add(new Customer() { Name = "Mangesh More", Age = 27, Location = "Chennai" });
            customers.Add(new Customer() { Name = "Shiv Kumar", Age = 23, Location = "Nashik" });
            customers.Add(new Customer() { Name = "Rajan Patil", Age = 28, Location = "Mumbai" });
            return customers;
        }
    }
