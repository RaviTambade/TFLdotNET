using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingCartRedisApi.Models;

namespace ShoppingCartRedisApi.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<ShoppingCartService> _logger;

    public ShoppingCartService(
        IDistributedCache cache,
        ILogger<ShoppingCartService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    private string GetCartKey(string userId)
    {
        return $"cart:{userId}";
    }

    public async Task<ShoppingCart> GetCartAsync(string userId)
    {
        var key = GetCartKey(userId);

        var cartData = await _cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(cartData))
        {
            _logger.LogInformation("Cart not found in Redis");

            return new ShoppingCart
            {
                UserId = userId
            };
        }

        _logger.LogInformation("Cart loaded from Redis");

        return JsonSerializer.Deserialize<ShoppingCart>(cartData)!;
    }

    public async Task AddItemAsync(string userId, CartItem item)
    {
        var cart = await GetCartAsync(userId);

        var existingItem = cart.Items
            .FirstOrDefault(x => x.ProductId == item.ProductId);

        if (existingItem is not null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            cart.Items.Add(item);
        }

        await SaveCartAsync(cart);
    }

    public async Task RemoveItemAsync(string userId, int productId)
    {
        var cart = await GetCartAsync(userId);

        var item = cart.Items
            .FirstOrDefault(x => x.ProductId == productId);

        if (item is not null)
        {
            cart.Items.Remove(item);
        }

        await SaveCartAsync(cart);
    }

    public async Task ClearCartAsync(string userId)
    {
        var key = GetCartKey(userId);

        await _cache.RemoveAsync(key);

        _logger.LogInformation("Cart cleared");
    }

    private async Task SaveCartAsync(ShoppingCart cart)
    {
        var key = GetCartKey(cart.UserId);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };

        var cartData = JsonSerializer.Serialize(cart);

        await _cache.SetStringAsync(key, cartData, options);

        _logger.LogInformation("Cart saved into Redis");
    }
}