using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IOCApp.Models;

namespace IOCApp.Controllers;

public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;

    public HomeController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
 
}
