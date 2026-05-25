using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionDemoApp.Models;

namespace SessionDemoApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("HomeController Index action called.");
        return View();
    }

    public IActionResult Privacy()
    {
        return RedirectToAction("Index","flowers");
        //return View();
    }

    public IActionResult GetData()
    {
        var data = new { Name = "John Doe", Age = 30, City = "New York" };
        return Json(data);

    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


//interface: IActionResult
//class: JsonResult, ViewResult, 
//       RedirectResult, ContentResult, 
//       FileResult, Status