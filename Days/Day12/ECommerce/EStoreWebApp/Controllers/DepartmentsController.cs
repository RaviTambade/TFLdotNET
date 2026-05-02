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
    public IActionResult Index()
    {
        HRManager mgr=new HRManager();
        List<Department> departments=mgr.GetAllDepartments();
        this.ViewData["departments"]=departments;
        return View();
    }
    public IActionResult Details(int id)
    {
        Department theDepartment=new Department();
        HRManager mgr = new HRManager();
        theDepartment=mgr.GetDepartment(id);
        this.ViewBag.selectedDepartment = theDepartment;
        return View();
    }
    public JsonResult Trainers()
    {
        List<Trainer> trainers=new List<Trainer>();
        trainers.Add(new Trainer () {   FirstName="Ravi",LastName="Tambade",Email="ravi.tambade@transflower.in"} );
        trainers.Add(new Trainer () {   FirstName="Amit",LastName="Khedkar",Email="amit.khedkar@contoso.in"} );
        return Json(trainers);
    }
    [HttpPost]
    public IActionResult InsertTrainer(Trainer trainer){
        Console.WriteLine(trainer.FirstName+ "  "+ trainer.LastName + " "+ trainer.Email);
         return RedirectToAction("Index","Home");
    }
}
