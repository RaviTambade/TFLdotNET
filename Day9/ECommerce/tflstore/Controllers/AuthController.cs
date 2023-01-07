using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tflstore.Models;

namespace tflstore.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        Console.WriteLine("Auth Controller instance initialized......");
        _logger = logger;
    }

[HttpGet]
    public IActionResult Login(){
         Console.WriteLine("Invoking Home Controller Login method. ");
       

        return View();
    }
    public IActionResult Validate(string email, string password){

         Console.WriteLine("Validating User credentials.... ");
       

        if(email =="ravi.tambade@transflower.in" && 
           password=="seed"){
             Console.WriteLine("successfull validation of user..... ");
             Console.WriteLine("Redirecting to welcome..... ");   
            return Redirect("/home/Welcome");
           }
        return View();
    }  
    public IActionResult Welcome(){
         Console.WriteLine("Invoking Home Controller Welcome  method. ");
       
        return View();
    }

    public IActionResult Register(){
        return View();
    }

}
