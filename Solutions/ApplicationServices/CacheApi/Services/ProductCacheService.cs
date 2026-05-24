using CacheApi.Models;
using CacheApi.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace CacheApi.Services;

public class ProductCacheService : IProductCacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ProductRepository _repository;
    private readonly ILogger<ProductCacheService> _logger;

    private const string CACHE_KEY = "PRODUCT_LIST";

    public ProductCacheService(
        IMemoryCache memoryCache,
        ProductRepository repository,
        ILogger<ProductCacheService> logger)
    {
        _memoryCache = memoryCache;
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        // Check cache
        if (_memoryCache.TryGetValue(CACHE_KEY, out List<Product>? products))
        {
            _logger.LogInformation("Products loaded from CACHE");

            return products!;
        }

        _logger.LogInformation("Products loaded from DATABASE");

        // Load from repository
        products = await _repository.GetProductsAsync();

        // Cache settings
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        // Store in cache
        _memoryCache.Set(CACHE_KEY, products, cacheOptions);

        return products;
    }
}