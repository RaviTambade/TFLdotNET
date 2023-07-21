using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;
using BOL;
namespace EStoreWebApp.Controllers;
public class CustomersController : Controller
{
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger)
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

        Customer customer=new Customer();
        
        return View(customer);
    }

    [HttpPost]
    public IActionResult Insert( Customer customer){
        Console.WriteLine("Post method invoked....");
        if(!ModelState.IsValid){
        

               
          return View();

        }
        Console.WriteLine(customer.Name  + "  "+ customer.City);
        return RedirectToAction("Index");     
    }
}