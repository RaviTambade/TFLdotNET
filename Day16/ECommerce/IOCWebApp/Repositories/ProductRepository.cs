using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IOCWebApp.Models;

namespace IOCWebApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        IProductManager _pManager;

        public ProductRepository(){
            _pManager=new ProductManager();
        }
        public List<Product> GetProducts()
        {
                return _pManager.GetAll();
        }
    
        public Product GetProductById(int id)
        {
             return _pManager.GetById(id); 
        }
        public bool Insert(Product product){
        return  _pManager.Insert(product);
        }
        public bool Update(Product product){  
            return _pManager.Update(product);
        }
        
        public bool Delete(int id){
            return  _pManager.Delete(id); 
        }
    }
}