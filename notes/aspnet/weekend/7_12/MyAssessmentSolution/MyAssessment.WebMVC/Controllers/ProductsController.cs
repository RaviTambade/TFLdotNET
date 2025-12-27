using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyAssessment.WebMVC.Models;

namespace MyAssessment.WebMVC.Controllers;

public class ProductsController : Controller
{
    public ProductsController()
    {
     
    }

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
        return View();
    }

    public IActionResult Update( )
    {
        return View();
    }

}