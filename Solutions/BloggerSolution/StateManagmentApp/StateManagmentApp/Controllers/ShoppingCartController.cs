using Microsoft.AspNetCore.Mvc;
using StateManagmentApp.Models;
using System.Text.Json;

namespace StateManagmentApp.Controllers
{
    //Server Side State Management
    //Session Management
        
    public class ShoppingCartController : Controller
    {
        public IActionResult AddToCart()
        {
            Cart theCart = new Cart();
            theCart.Items.Add("laptop");
            string json = JsonSerializer.Serialize(theCart);
            TempData["mycart"] = json;
            //return View();
            return RedirectToAction("RemoveFromCart");
        }

        //session is essentail when data is supposed to accessed
        //across any action method belong to any controller
        public IActionResult RemoveFromCart()
        {
            if (TempData["mycart"] is string json)
            {
                Cart existingCart = JsonSerializer.Deserialize<Cart>(json);
                existingCart.Items.Remove("laptop");

                ViewData["mycart"] = existingCart;
            }
            return View();
        }
    }
}
