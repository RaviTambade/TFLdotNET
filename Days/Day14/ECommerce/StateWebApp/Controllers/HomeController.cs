using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StateWebApp.Models;

namespace StateWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //Adding cookie to 
        var cookieOptions = new CookieOptions(); 
        cookieOptions.Expires = DateTime.Now.AddDays(1);
        cookieOptions.Path = "/"; 
        Response.Cookies.Append("SSN", "56456456456456456456", cookieOptions);

        return View();
    }

    public IActionResult Aboutus()
    {
        //string  name=this.Request.QueryString.ToString();
        var cookie = Request.Cookies["SSN"]; 

       // Console.WriteLine(name);
        return View();
    }

     public IActionResult Contact()
    {
        //string connString = this.Configuration.GetConnectionString("MyConn");

        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
