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

    //Show all items available from Cart
    public IActionResult Index()
    {
        //get Value from session Variable
        string sessionData = HttpContext.Session.GetString("product");
        Console.WriteLine("Session data is retrived from server "+ sessionData);
        HttpContext.Session.SetString("product","laptop");

        
        return View();
    }

    //Add id as new item into Cart
    public IActionResult AddToCart(int id)
    {
        //change value in session variable
        HttpContext.Session.SetString("product","laptop" + id);
        Console.WriteLine("Session data is added to server side");
        return RedirectToAction("Index");
    }

    //Remove id as existing item from Cart
    public IActionResult RemoveFromCart(int id)
    {
       // List<int> items= this.Session["cart"];
     //   items.Remove(id);
       return RedirectToAction("Index");
    }
     
}
