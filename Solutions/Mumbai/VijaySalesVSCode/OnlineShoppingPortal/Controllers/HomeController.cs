using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingPortal.Models;

namespace OnlineShoppingPortal.Controllers;


//Incomming HTTP Request -> Middleware -> Routing -> Controller -> Model -> View -> Response
//Controller is a class that handles incomming HTTP requests
public class HomeController : Controller
{

    //HTTP Action Listeners

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    //for every action method there should be a view
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Aboutus()
    {
        return View();

    }
    public IActionResult Contact()
    {
        return View();
    }
    public IActionResult Services()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
