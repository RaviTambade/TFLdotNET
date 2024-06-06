using Microsoft.AspNetCore.Mvc;
using StateManagmentApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace StateManagmentApp.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            Student student = new Student { StudentId = 12, Branch = "CS", Gender = "M", Name = "Raj",Section  = "Web" };
            string json = JsonSerializer.Serialize(student);

            TempData["city"] = json;
            //return View();
            return RedirectToAction("Services");  //Consecutive new request
        }

        public IActionResult Services()
        {
            if (TempData["city"] is string json)
            {
                Student theStudent = JsonSerializer.Deserialize<Student>(json);
                ViewData["city"] = theStudent;
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
