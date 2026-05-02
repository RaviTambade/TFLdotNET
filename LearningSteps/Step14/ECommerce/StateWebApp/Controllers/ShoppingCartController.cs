using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StateWebApp.Models;
namespace StateWebApp.Controllers;
public class ShoppingCartController : Controller
{
    private readonly ILogger<ShoppingCartController> _logger;
    public ShoppingCartController(ILogger<ShoppingCartController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        string sessionData = HttpContext.Session.GetString("product");
        Console.WriteLine("Session data is retrived from server "+ sessionData);
        HttpContext.Session.SetString("product","laptop");
        return View();
    }
    public IActionResult AddToCart(int id)
    {
        HttpContext.Session.SetString("product","laptop" + id);
        Console.WriteLine("Session data is added to server side");
        return RedirectToAction("Index");
    }
    public IActionResult RemoveFromCart(int id)
    {
       return RedirectToAction("Index");
    }
}
