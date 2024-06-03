using Microsoft.AspNetCore.Mvc;

namespace TFLECommerce_May2024.Controllers
{
    public class ShoppingCartController : Controller
    {
        //action methods from Shopping Cart point of view
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult AddToCart()
        {

            return View();
        }
        public IActionResult RemoveFromCart()
        {
            return View();
        }
    }
}
