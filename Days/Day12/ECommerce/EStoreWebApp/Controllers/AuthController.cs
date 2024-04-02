using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;


namespace EStoreWebApp.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }
   
    [HttpGet]
    public IActionResult Login(){
        Console.WriteLine("Login GET method is invoked at server side");
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password){
        Console.WriteLine("Login POST method is invoked at server side");
       if(email=="ravi.tambade@transflower.in" && password=="seed"){
        return RedirectToAction("index", "departments");
       }
        return View();
    }
     
}
