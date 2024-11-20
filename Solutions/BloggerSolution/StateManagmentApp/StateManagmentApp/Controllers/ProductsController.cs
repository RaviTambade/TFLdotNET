using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace StateManagmentApp.Controllers
{
    public class ProductsController : Controller
    {

        //ON first request  data is fetched and kept in global cache
        //and the data is sent to repsonse
        //[OutputCache]
        [OutputCache(PolicyName = "CacheFor10Seconds")]
        public IActionResult Index()
        {
            List<string> products = new List<string>();
            products.Add("Gerbera");
            products.Add("Rose");
            products.Add("Jasmines");
            return Json(products);
        }

       
        public IActionResult Details()
        {
            List<string> products = new List<string>();
            products.Add("Gerbera");
            products.Add("Rose");
            products.Add("Jasmines");
            return Json(products);
        }

    }
}
