using Microsoft.AspNetCore.Mvc;
using TFLECommerce_May2024.Models;

namespace TFLECommerce_May2024.Controllers
{
    public class ProductsController : Controller
    {
        //action method from Product Inventory Point of view
        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product{Id = 23, Title = "IBM ThinkPad", Description = "Smart Laptop",StockAvailable = 600});
            products.Add(new Product{Id = 24,Title = "Apple iPad",Description = "Smart Pad",StockAvailable = 654});
            products.Add(new Product {Id = 25,Title = "HP Pavillion",Description = "Smart Notebook",StockAvailable = 60});
            products.Add(new Product { Id = 26, Title = "Hololense", Description = "Smart Glass", StockAvailable =90 });

            ViewData["allproducts"]=products;
            return View();

            /*return Json(new { Id=23, Title="IBM ThinkPad",
                               Description="Smart Laptop", Quantity=600,
                               RAM=16, Core=8});
            */
        }

        public IActionResult Details(int id) { 

            return View();

        }
        public IActionResult Insert()
        {
            return View();
        }

        public IActionResult Update() {
            return View();
        }
    }
}
