using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionDemoApp.Models;

namespace SessionDemoApp.Controllers;

public class OrdersController : Controller
{

    public IActionResult Index()
    {
        
        return View();
    }

   

}