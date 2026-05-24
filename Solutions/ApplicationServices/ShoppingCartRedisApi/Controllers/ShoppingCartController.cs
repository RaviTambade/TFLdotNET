using Microsoft.AspNetCore.Mvc;
using ShoppingCartRedisApi.Models;
using ShoppingCartRedisApi.Services;

namespace ShoppingCartRedisApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _cartService;

    public ShoppingCartController(IShoppingCartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(string userId)
    {
        var cart = await _cartService.GetCartAsync(userId);

        return Ok(cart);
    }

    [HttpPost("{userId}/items")]
    public async Task<IActionResult> AddItem(
        string userId,
        CartItem item)
    {
        await _cartService.AddItemAsync(userId, item);

        return Ok("Item added to cart");
    }

    [HttpDelete("{userId}/items/{productId}")]
    public async Task<IActionResult> RemoveItem(
        string userId,
        int productId)
    {
        await _cartService.RemoveItemAsync(userId, productId);

        return Ok("Item removed");
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> ClearCart(string userId)
    {
        await _cartService.ClearCartAsync(userId);

        return Ok("Cart cleared");
    }
}