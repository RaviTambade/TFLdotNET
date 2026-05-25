using Microsoft.AspNetCore.Mvc;
 using StateManagmentApp.Models;
using System.Reflection.Metadata.Ecma335;
namespace StateManagmentApp.Controllers
{
    public class StateMgmtController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Client Side State Management using LocalStorage, SessionStorage
        public IActionResult LocalStorage()
        {
            return View();
        }
        public IActionResult SessionStorage()
        {
            return View();
        }

        public IActionResult SetQueryString([FromQuery] string FirstName, 
                                            [FromQuery] string LastName)
        {
            var data = new { FullName = FirstName + "  " + LastName };

            return Ok(data);
        }
        

        //Cookie
        //Cookies are written by server and sent to client side
        public IActionResult SetCookie() {

            //Cookie is an information written by server applicaiton at client side
            //Cookie collection is sent to client Side through Reponse
            string key = "DemoCookie";
            string value = "Bhupendra is tecnically strong"; ////JWTToken is sent as cookie at client side from identity

            //you can write data of size 4KB

            CookieOptions obj = new CookieOptions();
            obj.Expires = DateTime.Now.AddMinutes(10);

            Response.Cookies.Append(key, value, obj);
            return RedirectToAction("DisplayCookie");
        }

        public IActionResult DisplayCookie()
        {
            //Cookies collection is retrived from Client through Request Object
            string key = "DemoCookie";
            var CookieValue = Request.Cookies[key];
            ViewBag.cookieVal = CookieValue;
            return View();
        }




    }
}
