using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;

using BOL;
namespace EStoreWebApp.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public ActionResult Index()
    {
        return View();
    }

    public ViewResult Privacy()
    {
        return View();
    }

    public JsonResult Trainers()
    {
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


    //Every Action method is stateless by its nature
    //variables declared in scope of Action method
    //are visible in stack till control of execution is
    //inside action method

  
    public double amount;
    public ActionResult List()
    {
        int i = 56;
        bool status = false;
        amount = 567;
        TempData["welcome"] = "Welcome to Masters";
        //Chaining of Action methods
        //you can redirect to same controllers action method
        return RedirectToAction("Details");
        //You can redirect to another controllers action method
        //return RedirectToAction("index", "Products");
    }
    public ActionResult Details()
    {
        double retrivedAmount = amount;
        var someName = TempData["welcome"];
        ViewBag.messageFromAction=someName;
        return View();
    }

    public ActionResult Instructor()
    {
        //Model Binding Example
        Mentor theMentor = new Mentor();

        //Assign default properties to theMentor
        theMentor.FirstName = "Narendra";
        theMentor.LastName = "Pawar";
        theMentor.Email = "narendra.pawar@iacsd.com";
        theMentor.Certification = "Microsoft Certified Trainer";

        //Access object from Bussiness Logic Layer
        //Assign retrived object  to Model
        //Send Model to View
        return View(theMentor);
    }
    public ActionResult Combo()
        {
           var featuredProduct = new  EStoreWebApp.Models.Product
            {
              Title = "Special Cupcake Assortment!",
              Description = "Delectable vanilla and chocolate cupcakes",
              ExpiryDate = DateTime.Today.AddDays(7),
              UnitPrice = 5.99M,
              Quantity = 12
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
