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

    [HttpGet]
    public IActionResult Privacy()
    {
        Console.WriteLine("Invoking Home Controller Privacy method. ");
        return View();
    }
   
   [HttpGet]
    public IActionResult Aboutus()
    {
        Console.WriteLine("Invoking Home Controller About us method. ");
        return View();
    }

    [HttpGet]
    public IActionResult Contact()
    {
        Console.WriteLine("Invoking Home Controller About us method. ");
        return View();
    }
   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
