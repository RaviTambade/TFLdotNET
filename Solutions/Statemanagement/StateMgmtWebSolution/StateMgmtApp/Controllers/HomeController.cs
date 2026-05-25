using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StateMgmtApp.Models;

namespace StateMgmtApp.Controllers
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

     

            //create cookie
            this.Response.Cookies.Append("username",
                                          "ravi",
                                           new CookieOptions { Expires = DateTime.UtcNow.AddDays(1) });


            //read cookie
            var username = this.Request.Cookies["username"];

            return View();
        }

        public IActionResult About()
        {

             
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult List()
        {
            //http://localhost:5000/home/list?category=fruits
            //read query string
            var category = this.Request.Query["category"].ToString();
            if (string.IsNullOrEmpty(category))
            {
                return View("Error", new ErrorViewModel { RequestId = "Category is required" });
            }
            //create a list of items based on the category
            List<string> items = new List<string>();
            switch (category)
            {
                case "flowers":
                    {
                        items.Add("Gerbera");
                        items.Add("Jasmine");
                        items.Add("Marigold");
                    }
                    break;
                case "fruits":
                    items.Add("Apple");
                    items.Add("Banana");
                    items.Add("Orange");
                    break;
            }
            return View(items);
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
