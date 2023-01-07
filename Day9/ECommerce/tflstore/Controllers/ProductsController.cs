using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tflstore.Models;

namespace tflstore.Controllers;

public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
        Console.WriteLine("Products Controller instance initialized......");
        _logger = logger;
    }

    public IActionResult Index(){
        //fetch data from Model
        //send list of products to ViewData Collection
        
        List<Product>  allProducts=ProductManager.GetProducts();
        ViewData["catalog"]=allProducts;
        return View();
    }
}
