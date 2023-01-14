using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
 
using BOL;
using BLL;
namespace EStoreWebApp.Controllers;
public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    // Get All products from BLL
    // bind all products to ViewData
    // send viewData to View
    public IActionResult Index(){  
        return View();
    }
    
    //[Route("/transflwoer")]  //attribute routing

    public IActionResult Details(int id){
        CatalogManager manager=new CatalogManager();
        Product product=manager.GetProduct(id);
        this.ViewData["product"]=product;
        return View();
    }
 
    public IActionResult Insert(){
        Product product=new Product();
        return View(product);
    }

    [HttpPost]
    public IActionResult Insert( Product productToCreate){
        Console.WriteLine("Post method invoked....");
        if(!ModelState.IsValid){

               
          return View();

        }
        Console.WriteLine(productToCreate.Title + 
                           " " +
                           productToCreate.Description + " "+ 
                           productToCreate.UnitPrice + " "+
                           productToCreate.Balance + " "+
                           productToCreate.ExpiryDate);
        //insert product data into mysql using database connectivity
        return RedirectToAction("Index");     
    }

    public IActionResult Update(int id){
        CatalogManager manager=new CatalogManager();
        Product product=manager.GetProduct(id);   
        return View(product);
    }

    [HttpPost]
    public IActionResult Update(Product productToUpdate){
        if(!ModelState.IsValid){       
            return View();
            }
        CatalogManager manager=new CatalogManager();
        if(manager.Update(productToUpdate))
        {
            return RedirectToAction("Index");
        }
        return View ();
    }
}