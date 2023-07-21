using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;
namespace EStoreWebApp.Controllers
{
    public class WelcomeController : Controller
    {
            public IActionResult Index()
            {
                HttpContext.Session.SetString("Name", "Ravi");
                HttpContext.Session.SetInt32("Age", 48);
                return View();
            }
            public IActionResult Get()
            {
                User newUser = new User()
                {
                    Name = HttpContext.Session.GetString("Name"),
                    Age = HttpContext.Session.GetInt32("Age").Value
                };
                return View(newUser);
            }
        }
    }
 