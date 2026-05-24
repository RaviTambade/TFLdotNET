using CacheApi.Models;

namespace CacheApi.Repositories;

public class ProductRepository
{
    public async Task<List<Product>> GetProductsAsync()
    {
        // Simulate database delay
        await Task.Delay(3000);

        return new List<Product>
        {
            new Product{ Id=1, Name="Laptop", Price=65000, Category="Electronics"},
            new Product{ Id=2, Name="Mobile", Price=25000, Category="Electronics"},
            new Product{ Id=3, Name="Shoes", Price=3000, Category="Fashion"},
            new Product{ Id=4, Name="Watch", Price=5000, Category="Accessories"}
        };
    }
}