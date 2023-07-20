using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;
namespace EStoreWebApp.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Mentor()
    {
       /* 
            var trainer=new {
            FirstName="Ravi",
            LastName="Tambade"
        } ;
        */
        List<Trainer> trainers=new List<Trainer>();
        trainers.Add(new Trainer () {   FirstName="Ravi",LastName="Tambade",Email="ravi.tambade@transflower.in"} );
        trainers.Add(new Trainer () {   FirstName="Amit",LastName="Khedkar",Email="amit.khedkar@contoso.in"} );
        return Json(trainers);
        //return View();
    }

    public IActionResult Aboutus(){
        ViewData["company"] = "Transflower Learning Pvt. Ltd.";
        return View();
    }

    public IActionResult Services(){
        List<string> services=new List<string>();
        services.Add("CDAC Mentoring");
        services.Add("Corporate Trainings");
        services.Add("DevCamps");
        services.Add("Skillbased Learning Program");
        services.Add("Consulting");
        ViewBag.trainings=services;
        return View();
    }



    public ActionResult List()
    {
        TempData["welcome"] = "Welcome to Masters";
        return RedirectToAction("Details");
    }


    public ActionResult Details()
    {
        var someName = TempData["welcome"];
        return View();
    }




    public ActionResult Combo()
        {
            var featuredProduct = new Product
            {
                Name = "Special Cupcake Assortment!",
                Description = "Delectable vanilla and chocolate cupcakes",
                CreationDate = DateTime.Today,
                ExpirationDate = DateTime.Today.AddDays(7),
                ImageName = "cupcakes.jpg",
                Price = 5.99M,
                QtyOnHand = 12
            };

            ViewData["FeaturedProduct"] = featuredProduct;
            ViewBag.Product = featuredProduct;
            TempData["FeaturedProduct"] = featuredProduct;  

            return View();
        }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
