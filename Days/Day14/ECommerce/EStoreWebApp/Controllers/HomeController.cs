using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;
namespace EStoreWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
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

    public IActionResult Mentor()
    {
       /* 
            var trainer=new {
            FirstName="Ravi",
            LastName="Tambade"
        } ;
        */
        List<Trainer> trainers=new List<Trainer>();
        trainers.Add(new Trainer () {   FirstName="Ravi",LastName="Tambade",Email="ravi.tambade@transflower.in"} );
        trainers.Add(new Trainer () {   FirstName="Amit",LastName="Kedkar",Email="amit.khedkar@contoso.in"} );
        return Json(trainers);
        //return View();
    }

    public IActionResult Aboutus(){
        return View();
    }

    public IActionResult Services(){
        List<string> services=new List<string>();
        services.Add("CDAC Mentoring");
        services.Add("Corporate Trainings");
        services.Add("DevCamps");
        services.Add("Skillbased Learning Program");
        services.Add("Consulting");
        ViewBag.trainings=services;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
