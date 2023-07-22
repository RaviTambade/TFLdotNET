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

    
    public IActionResult Index(){
        return View();
    }

    
    //New Customer registration
    

   
    //You can set another name in url such as 
    //http://localhost:7878/Customers/insert
    //[HttpGet("insert")]

    public IActionResult Register(){
        //POCO Object
        Customer customer=new Customer();
        customer.Name="Chandrakant Jadhav";
        customer.Budget=784500;
        customer.City="Satara";
        customer.DateOfBirth=new DateTime(1997,4,4);
        //Model Transfer to View
        return View(customer);
    }

    [HttpPost]
    public IActionResult Register( Customer customer){
        Console.WriteLine(customer.City + "  "+ customer.Name + "  "+ customer.Gender);
        Console.WriteLine("Post method invoked....");
     if(!ModelState.IsValid){
            
          return View();
        }
    
        Console.WriteLine(customer.Name  + "  "+ customer.City);
        return RedirectToAction("Index");     
    }
}