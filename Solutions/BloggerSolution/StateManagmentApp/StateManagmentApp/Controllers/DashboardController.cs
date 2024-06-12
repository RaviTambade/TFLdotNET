using Microsoft.AspNetCore.Mvc;
using StateManagmentApp.Models;

namespace StateManagmentApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
         
            
            return View();
        }
    }
}
