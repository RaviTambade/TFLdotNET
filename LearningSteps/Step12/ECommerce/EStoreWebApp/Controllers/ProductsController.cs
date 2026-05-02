using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;
namespace EStoreWebApp.Controllers;
public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index(){  
        return View();
    }
    public IActionResult Insert(){
        Product product=new Product();
        return View(product);
    }
    [HttpPost]
    public IActionResult Insert( Product productToCreate){
        Console.WriteLine("Post method invoked....");
        if(!ModelState.IsValid){
          return View();
        }
        Console.WriteLine(productToCreate.Title + 
                           " " +
                           productToCreate.Description + " "+ 
                           productToCreate.UnitPrice + " "+
                           productToCreate.Quantity + " "+
                           productToCreate.ExpiryDate);
        return RedirectToAction("Index");     
    }
}
