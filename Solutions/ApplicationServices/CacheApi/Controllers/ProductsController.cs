using CacheApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CacheApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductCacheService _cacheService;

    public ProductsController(IProductCacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _cacheService.GetProductsAsync();

        return Ok(products);
    }
}