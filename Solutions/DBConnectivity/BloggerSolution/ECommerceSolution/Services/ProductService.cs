using ECommerceSolution.Models;

namespace ECommerceSolution.Services
{
    public class ProductService : IProductService
    {
        //data manipulation logic
        private List<Product> products;

        public ProductService()
        {
            products = new List<Product>();
            products.Add(new Product { Id = 12, Name = "Laptop", Price = 30000 });
            products.Add(new Product { Id = 13, Name = "Mobile Phone", Price = 38000 });
            products.Add(new Product { Id = 14, Name = "HeadPhone", Price = 3000 });
            products.Add(new Product { Id = 15, Name = "Desktop", Price = 15000 });
        }
    
        public List<Product> GetAll()
        {
             return products;
        }

        public Product GetById(int id)
        {
            return products.Find(p => p.Id == id);
        }

        public void Insert(Product product)
        {
             products.Add(product);
        }

        public void Remove(int id)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                products.Remove(existingProduct);
            }
        }

        public void Update(Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
              products[products.IndexOf(existingProduct)] = product;
            }
        }
    }
}