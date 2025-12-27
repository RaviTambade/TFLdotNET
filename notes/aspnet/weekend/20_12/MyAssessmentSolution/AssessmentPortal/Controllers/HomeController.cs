using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AssessmentPortal.Models;
using System.Text.Json.Nodes;
using System.ComponentModel.Design;
using System.Reflection;
using AssessmentPortal.Models;
using System.Buffers;
using System.Reflection.PortableExecutable;

namespace AssessmentPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        //ViewResult
        return View();
    }

    public IActionResult Aboutus()
    {
        //JsonResult
        //Anonymous Type
        var company= new
        {
            Title="Transflower",
            Founder="Ravi Tambade",
            RegistrationYear=2011,
            Stregth=30
        };
        //JOSN Serialization
        return Json(company);
    }

    public IActionResult Contact()
    {

        //Deserialization from existing file

        //fetch data from  data source
        var organization=new Organization
        {
            Title="Transflower",
            Owner="Ravi Tambade",
            Location="Pune",
            Strength=56 
        };
        //return View(organization);
        ViewData["theOrg"]=organization;
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
