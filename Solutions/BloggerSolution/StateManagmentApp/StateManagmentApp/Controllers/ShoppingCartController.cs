using Microsoft.AspNetCore.Mvc;
using StateManagmentApp.Models;
using System.Text.Json;

namespace StateManagmentApp.Controllers
{
    //Server Side State Management
    //Session Management

    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View(); 
        }


        /* public IActionResult AddToCart()
         {
             Cart theCart = new Cart();
             theCart.Items.Add("laptop");
             string json = JsonSerializer.Serialize(theCart);
             TempData["mycart"] = json;
            // return View();
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

         */

        public IActionResult AddToCart([FromQuery] string product)
        {
            string json = null;
            json = HttpContext.Session.GetString("cart");
            if (json != null)
            {
                Cart theCart = JsonSerializer.Deserialize<Cart>(json);
                theCart.Items.Add(product);
                json = JsonSerializer.Serialize(theCart);
            }
            else
            {
                Cart theCart=new Cart();
                theCart.Items.Add(product);
                json = JsonSerializer.Serialize(theCart);
            }
            HttpContext.Session.SetString("cart", json);
            return RedirectToAction("cart");
        }

       
        public IActionResult Cart()
        {
            string json = null;
            json = HttpContext.Session.GetString("cart");
            if (json != null)
            {
                Cart existingCart = JsonSerializer.Deserialize<Cart>(json);
                ViewData["mycart"] = existingCart;
            }
            return View();
        }

        /*
        public IActionResult RemoveFromCart()
        {
            string json = null;
            json=HttpContext.Session.GetString("cart");

            if (json!=null)
            {
                Cart existingCart = JsonSerializer.Deserialize<Cart>(json);
                existingCart.Items.Remove("laptop");
                ViewData["mycart"] = existingCart;
            }
            return View();
        }

        */

    }
}
