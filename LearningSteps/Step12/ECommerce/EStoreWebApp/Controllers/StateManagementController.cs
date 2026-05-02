using Microsoft.AspNetCore.Mvc;
namespace EStoreWebApp.Controllers
{
    public class StateManagementController : Controller
    {
        public IActionResult Index()
        {
            string userName = Request.Cookies["UserName"];
            return View("Index", userName);
        }
        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            string userName = form["userName"].ToString();
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append("UserName", userName, option);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveCookie()
        {
            Response.Cookies.Delete("UserName");
            return View("Index");
        }
    }
}
