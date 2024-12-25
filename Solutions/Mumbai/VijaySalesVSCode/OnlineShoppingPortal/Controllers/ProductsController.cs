using Microsoft.AspNetCore.Mvc;

namespace OnlineShoppingPortal.Controllers
{
    public class ProductsController : Controller
    {

        //set of action methods
        public IActionResult Index()
        {

           //List of products

            return View();
        }
        public IActionResult Details(int id)
        {
            //Product details
            return View();
        }
    }
}
