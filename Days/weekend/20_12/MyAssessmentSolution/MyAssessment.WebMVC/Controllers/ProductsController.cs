using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyAssessment.WebMVC.Models;

namespace MyAssessment.WebMVC.Controllers;

public class ProductsController : Controller
{
    public ProductsController()
    {
     
    }

    //Request processing logic using Action Methods


    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Details(int id)
    {
        return View();
    }

    public IActionResult Insert( )
    {

        //Returns object of ViewResult
        
        return View();
    }

    public IActionResult Update( )
    {
        return View();
    }

}