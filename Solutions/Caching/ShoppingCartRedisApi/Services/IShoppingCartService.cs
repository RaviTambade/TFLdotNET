using ShoppingCartRedisApi.Models;

namespace ShoppingCartRedisApi.Services;

public interface IShoppingCartService
{
    Task<ShoppingCart> GetCartAsync(string userId);

    Task AddItemAsync(string userId, CartItem item);

    Task RemoveItemAsync(string userId, int productId);

    Task ClearCartAsync(string userId);
}