using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EStoreWebApp.Models;
using BOL;
using BLL;
namespace EStoreWebApp.Controllers;

public class DepartmentsController : Controller
{
    
    private readonly ILogger<DepartmentsController> _logger;

    public DepartmentsController(ILogger<DepartmentsController> logger)
    {
        _logger = logger;
    }

    //Action Method
    //Every View (cshtml) is associated with respective Action method
    //Generally view name(index.cshtml) is like
    //action method name(Index() of Controller)

    public IActionResult Index()
    {
        HRManager mgr=new HRManager();
        List<Department> departments=mgr.GetAllDepartments();
        //View Data is a Dictionary Object
        //data is maintained inside dictionary using key - Value pair
        this.ViewData["departments"]=departments;
        return View();
    }

    public IActionResult Details(int id)
    {
        Department theDepartment=new Department();
        HRManager mgr = new HRManager();
        theDepartment=mgr.GetDepartment(id);
        //Ways to transfer object from Action method to view
        //1.ViewData
        //2.ViewBag
        this.ViewBag.selectedDepartment = theDepartment;
        return View();

    }
    
    public JsonResult Trainers()
    {
        List<Trainer> trainers=new List<Trainer>();
        trainers.Add(new Trainer () {   FirstName="Ravi",LastName="Tambade",Email="ravi.tambade@transflower.in"} );
        trainers.Add(new Trainer () {   FirstName="Amit",LastName="Khedkar",Email="amit.khedkar@contoso.in"} );
       //Create bussiness Logic  manager instance
       //Invoke Bussiness Manager GetAll Trainers method
       //retrieved list<Trainer>  pass to Json Method
        return Json(trainers);
    }




    [HttpPost]
    public IActionResult InsertTrainer(Trainer trainer){
        Console.WriteLine(trainer.FirstName+ "  "+ trainer.LastName + " "+ trainer.Email);
        //Invoking HRManager Bussiness Logic Class
        //Inserting new Trainer to Bussiness Logic 
       // return RedirectToAction("Index");  //redirecting to existing controllers index method
         return RedirectToAction("Index","Home");
        //redirecting to  Home controllers index method

    }
}
