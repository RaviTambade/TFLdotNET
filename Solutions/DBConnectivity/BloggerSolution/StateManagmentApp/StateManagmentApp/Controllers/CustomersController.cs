using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.OutputCaching;
using StateManagmentApp.Models;

namespace StateManagmentApp.Controllers
{
    public class CustomersController : Controller
    {

        [OutputCache(PolicyName = "CacheForTenSeconds")]

        public IActionResult Index()
        {
            return View();
        }
    }
}