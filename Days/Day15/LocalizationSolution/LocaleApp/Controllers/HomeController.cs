using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LocaleApp.Models;
using Microsoft.Extensions.Localization;

namespace LocaleApp.Controllers;

public class HomeController : Controller
{ 
    private readonly IStringLocalizer<HomeController> _localizer;
    public HomeController(IStringLocalizer<HomeController> localizer)
    {
        _localizer = localizer;
    }
    public IActionResult Index()
    {
        Console.WriteLine( _localizer["Greeting"]);
        
        ViewData["Greeting"] = _localizer["Greeting"];
        ViewData["Thanks"] = _localizer["Thanks"];
        return View();
    }
}
