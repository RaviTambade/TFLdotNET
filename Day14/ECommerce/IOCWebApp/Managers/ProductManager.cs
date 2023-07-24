using System.Collections.Generic;
using System;
using System.Linq;
using IOCWebApp.Models;
using IOCWebApp.Contexts;

namespace IOCWebApp.Repositories
{
    public class ProductManager : IProductManager
    {
        public bool Delete(int id)
        {
            using(var context = new CollectionContext())
            {
                context.Products.Remove(context.Products.Find(id));
                context.SaveChanges();
            }
            return true;
        }

        public List<Product> GetAll()
        {
            using (var context = new CollectionContext())
            {
             var products = from prod in context.Products select prod;
             return products.ToList<Product>();
            }
        }

        public Product GetById(int id)
        {
            using (var context = new CollectionContext())
            {
             var product = context.Products.Find(id);
             return product;
            }
        }

        public bool Insert(Product product)
        {
            using(var context = new CollectionContext())
            {
                context.Products.Add(product);
                context.SaveChanges(); 
            }
            return true;
        }

        public bool Update(Product product)
        {
            using(var context = new CollectionContext())
            {
                var theProduct = context.Products.Find(product.Id);
                theProduct.Title =product.Title;
                theProduct.Quantity=product.Quantity;
                theProduct.Description=product.Description;
                theProduct.UnitPrice=product.UnitPrice;
                context.SaveChanges();
            }
            return true;
        }
    }
}