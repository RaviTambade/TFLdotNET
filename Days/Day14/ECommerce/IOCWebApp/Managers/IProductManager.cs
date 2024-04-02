
using System.Collections.Generic;
using IOCWebApp.Models;

namespace IOCWebApp.Repositories
{
    public interface IProductManager{
        List<Product> GetAll();
        Product GetById(int id);
        bool Insert(Product product);
        bool Update(Product product);
        bool Delete(int id);
    }
}