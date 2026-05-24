using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cachingApp.Models;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.OutputCaching;

namespace cachingApp.Controllers;

public class HomeController : Controller
{
    [OutputCache(Duration = 30)]
    public IActionResult Index()
    {
       ViewBag.Message = DateTime.Now.ToString("hh:mm:ss tt");

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
