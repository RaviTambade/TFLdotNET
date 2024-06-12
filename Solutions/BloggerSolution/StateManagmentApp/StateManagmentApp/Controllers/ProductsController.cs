using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace StateManagmentApp.Controllers
{
    public class ProductsController : Controller
    {
        [OutputCache]
        public IActionResult Index()
        {
            List<string> products = new List<string>();
            products.Add("Gerbera");
            products.Add("Rose");
            products.Add("Jasmines");
            return Json(products);
        }
    }
}
