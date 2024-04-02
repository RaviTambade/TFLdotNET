using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DIWebApp.Models;
using DIWebApp.Interfaces;
using DIWebApp.Services;
namespace DIWebApp.Controllers;
public class ProductsController : Controller
{
    private readonly IProductCatalogService _productCatalogService;
    // Constuctor Dependency Injection
    public ProductsController(IProductCatalogService productCatalogSerivce)
    {
        Console.WriteLine("ProductsController Instance is Initialized.........*******");
        //are used for initalization
        this._productCatalogService=productCatalogSerivce;
    }

    public IActionResult Insert()
    { 
        bool status=this._productCatalogService.Insert();
         ViewData["message"]="New Product is inserted status="+status;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}