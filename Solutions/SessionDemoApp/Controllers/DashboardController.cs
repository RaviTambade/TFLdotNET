using Microsoft.AspNetCore.Mvc;
using Core.Services.Interfaces;

namespace SessionDemoApp.Controllers;

public class DashboardController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
    
}