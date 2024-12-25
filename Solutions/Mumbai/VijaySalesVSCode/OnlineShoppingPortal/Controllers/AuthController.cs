using Microsoft.AspNetCore.Mvc;

namespace OnlineShoppingPortal.Controllers
{
    public class AuthController : Controller
    {

        //set of action methods

        //Http Get Action Method

        [HttpGet]   //Attribute : Metadata about the method
        public IActionResult Login()
        {
            return View();
        }

        
        public IActionResult Logout() {

            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
