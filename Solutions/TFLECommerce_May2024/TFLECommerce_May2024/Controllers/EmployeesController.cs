using Microsoft.AspNetCore.Mvc;
using HRORMApp.DbContexts;
using HRORMApp.Entities;
using HRORMApp.Repositories;

namespace TFLECommerce_May2024.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {

            using (HRContext _context = new HRContext())
            {
                EmployeeRepository repo=new EmployeeRepository(_context);
                List<Employee> employees = repo.GetAll();
                ViewData["allEmployees"]=employees;
            }
            return View();
        }
    }
}
