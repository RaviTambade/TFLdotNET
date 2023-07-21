using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;
namespace EStoreWebApp.Controllers;
public class CustomersController : Controller
{
    private readonly ILogger<CustomersController> _logger;

    public ProductsController(ILogger<CustomersController> logger)
    {
        _logger = logger;
    }

    // Get All products from BLL
    // bind all products to ViewData
    // send viewData to View
    public IActionResult Index(){  
        return View();
    }
 
    public IActionResult Insert(){
        
        return View();
    }

    [HttpPost]
    public IActionResult Insert( Customer customer){
        Console.WriteLine("Post method invoked....");
        if(!ModelState.IsValid){

               
          return View();

        }
       
        return RedirectToAction("Index");     
    }
}