using ChartApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChartApp.Controllers
{
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

        public IActionResult GetData()
        { 
            var data = new[]
            {
                new { Category = "Seed", Value = 300 },
                new { Category = "CDAC", Value = 120 },
                new { Category = "IBM", Value = 40 },
                new { Category = "Microsoft", Value = 80 }
            };
            return Json(data);
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
}
