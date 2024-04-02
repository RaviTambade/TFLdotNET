using System;
using System.Collections.Generic;
using IOCWebApp.Models;

namespace IOCWebApp.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
         Product GetProductById(int id);
        bool Insert(Product product);
        bool Update(Product product);
        bool Delete(int id);
    }
}