using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DIWebApp.Models;
using DIWebApp.Interfaces;
using DIWebApp.Services;
namespace DIWebApp.Controllers;
public class HomeController : Controller
{
    //Data members for invoking  Services
    //Data members are defined
    //by  interfaces and 
    //not  by their concrete classes
    private readonly IHelloWorldService _helloWorldService;
    private readonly IProductCatalogService _productCatalogService;

    // Constuctor Dependency Injection
    public HomeController(IHelloWorldService helloWorldService,
                          IProductCatalogService productCatalogSerivce)
    {
        Console.WriteLine("HomeController Instance is Initialized.........*******");
        //are used for initalization
        this._helloWorldService=helloWorldService;
        this._productCatalogService=productCatalogSerivce;
    }

    public IActionResult Index()
    {
        Console.WriteLine("HomeController Index Method id invoked");
        string message=this._helloWorldService.SaysHello();
        ViewData["message"]=message;
        bool status=this._productCatalogService.Insert();
        return View();
    }

    public IActionResult Privacy()
    {
        Console.WriteLine("HomeController Privacy Method id invoked");
        string message=this._helloWorldService.SaysHello();
        ViewData["message"]=message;
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}