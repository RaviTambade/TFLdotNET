using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tflstore.Models;

namespace tflstore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        Console.WriteLine("Home Controller instance initialized......");
        _logger = logger;
    }

    //Action Methods

    public IActionResult Index()
    {
        Console.WriteLine("Invoking Home Controller index method.. ");
        return View();
    }

    public IActionResult Privacy()
    {
        Console.WriteLine("Invoking Home Controller Privacy method. ");
        return View();
    }

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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
