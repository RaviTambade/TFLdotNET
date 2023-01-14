using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;
namespace EStoreWebApp.Controllers;

public class PeopleController : Controller
{
    private readonly ILogger<PeopleController> _logger;
    public PeopleController(ILogger<PeopleController> logger)
    {
        _logger = logger;
    }

    public IActionResult List(int ssn)
    {
        Console.WriteLine( "Country Code :"+ ssn);
        return View();
    }  
    public IActionResult Details(int ssn)
    {
        return View();
    }  

}
