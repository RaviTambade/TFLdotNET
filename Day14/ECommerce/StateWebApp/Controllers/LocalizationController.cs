using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StateWebApp.Models;

namespace StateWebApp.Controllers;

public class LocalizationController : Controller
{
    private readonly IStringLocalizer<LocalizationController> _localizer;
    public LocalizationController(IStringLocalizer<LocalizationController> localizer)
    {
        _localizer = localizer;
    }
    public IActionResult Index()
    {
        ViewData["Greeting"] = _localizer["Greeting"];
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
