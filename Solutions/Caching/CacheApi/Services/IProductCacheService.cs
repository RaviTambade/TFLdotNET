using CacheApi.Models;
namespace CacheApi.Services;

public interface IProductCacheService
{
    Task<List<Product>> GetProductsAsync();
}
