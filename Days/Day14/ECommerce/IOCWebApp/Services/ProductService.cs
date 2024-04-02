using System.Collections.Generic;
using IOCWebApp.Models;
using IOCWebApp.Repositories;

namespace IOCWebApp.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repo;
        public ProductService()
        {
            ProductRepository repository=new ProductRepository();
             this._repo = repository;
        }

        public List<Product> GetProducts()
        {
            if (_repo != null)
            {   
                return _repo.GetProducts();
            }
            return null;
        }
        Product IProductService.GetProductById(int id)
        {
          return    _repo.GetProductById(id);
        }

        bool IProductService.Insert(Product product)
        {
            return _repo.Insert(product); 
        }

        bool IProductService.Update(Product product)
        {
            return _repo.Update(product); 
        }

        bool IProductService.Delete(int id)
        {
           return _repo.Delete(id);
        }
    }
}