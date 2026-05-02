using Microsoft.AspNetCore.Mvc;
using SecondWebApp.Models;
using System.Diagnostics;
namespace SecondWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Aboutus()
        {
            return View();
        }
        public IActionResult Index()
        {
            Console.WriteLine(" Index HTTP Get Request Processing Logic is called..");
            return View();
        }
        public IActionResult Privacy()
        {
            Console.WriteLine(" Privacy HTTP Get Request Processing Logic is called..");
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            Console.WriteLine(" Privacy HTTP Get Request Processing Logic is called..");
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            Console.WriteLine(" Privacy HTTP POST Request Processing Logic is called..");    
            if(email == "ravi.tambade@transflower.in" && password == "seed123") {
                return RedirectToAction("Welcome");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            Console.WriteLine(" register HTTP Get Request Processing Logic is called..");
            return View();
        }
        [HttpPost]
        public IActionResult Register(string email, string password, string firstname, string lastname, string contactnumber)
        {
            Console.WriteLine(" register HTTP POST Request Processing Logic is called..");
            Customer theCustomer=new Customer();
            theCustomer.Email = email;
            theCustomer.Password = password;
            theCustomer.ContactNumber = contactnumber;
            theCustomer.LastName = lastname;
            theCustomer.FirstName = firstname;
            Console.WriteLine(theCustomer.FirstName+ " "+theCustomer.LastName);
            return RedirectToAction("list");
        }
        public IActionResult Welcome()
        {
            Console.WriteLine(" Privacy HTTP Get Request Processing Logic is called..");
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
