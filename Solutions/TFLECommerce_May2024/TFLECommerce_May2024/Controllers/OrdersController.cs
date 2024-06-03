using Microsoft.AspNetCore.Mvc;

namespace TFLECommerce_May2024.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
