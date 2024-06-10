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
    }
}
