using Microsoft.AspNetCore.Mvc;
using StateManagmentApp.Models;
using System.Text.Json;

//ECommerce Solution
//Product Gallery is managed by Products Controller
//Shopping Cart for every user is managed by ShoppringCart Controller
//Each Shopping Cart will contain list of items
//User can add or remove items from existing Shopping Cart
//User can view number of items available in existing Shopping Cart maintained at server

namespace StateManagmentApp.Controllers
{
    //Server Side State Management
    //Session Management
    public class ShoppingCartController : Controller
    {
        //Step 3: Session Management  : Show all items from Cart maintained at server session
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


        //Step 4: Write logic for Adding item to shopping Cart maintained  inside Session
        public IActionResult AddToCart([FromQuery] string product)
        {
            // get existing session  using HttpContext.Session  
            // if session contains data then Deserialize data from session into cart object
            // add new item to cart
            // serialized theCart and add to sesssion
            // else
            // create new cart
            // add new item to the cart
            // serialized cart into json and  add to session

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
                Cart theCart = new Cart();
                theCart.Items.Add(product);
                json = JsonSerializer.Serialize(theCart);
            }
            HttpContext.Session.SetString("cart", json);
            return RedirectToAction("cart");
        }


        //Step 4: Write logic for removing item from shopping Cart maintained  inside Session

        public IActionResult RemoveFromCart([FromQuery] string product)
        {
            string json = null;
            json = HttpContext.Session.GetString("cart");
            if (json != null)
            {
                Cart existingCart = JsonSerializer.Deserialize<Cart>(json);
                existingCart.Items.Remove(product);
                json = JsonSerializer.Serialize(existingCart);
                HttpContext.Session.SetString("cart", json);
            }
            return RedirectToAction("cart");
        }


        //Shpping Cart management using TempData  one more alternative


        /* public IActionResult AddToCart()
         {
             Cart theCart = new Cart();
             theCart.Items.Add("laptop");
             string json = JsonSerializer.Serialize(theCart);
             TempData["mycart"] = json;
            // return View();
             return RedirectToAction("RemoveFromCart");
         }
        */

        //session is essentail when data is supposed to accessed
        //across any action method belong to any controller
        /*   public IActionResult RemoveFromCart()
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
    }
}
