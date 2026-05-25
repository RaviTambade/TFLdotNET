using Microsoft.AspNetCore.Mvc;
using StateManagmentApp.Models;

namespace StateManagmentApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
         
            
            return View();
        }

        public IActionResult Details()
        {
           

            return View();
        }

        public IActionResult UserData()
        {
            Result theResult = new Result();
            theResult.Year = 2023;
            theResult.Name = "Ravi Tambade";
            theResult.Marks = new List<int>() {65,65,67,89,78};
           
            return Json(theResult);
        }


        [HttpPost]
        public IActionResult Register([FromBody]MyUser user)
        {

            Console.WriteLine(user.FirstName + " " + user.Id);
            //
            return Ok("done");
        }
        }
}
