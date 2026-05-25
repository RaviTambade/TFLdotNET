using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StateWebApp.Models;

namespace StateWebApp.Controllers;

public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ILogger<DashboardController> logger)
    {
        _logger = logger;
    }
        public JsonResult CityRevenue(){
     
            var revenueList=RevenueDataAccessLayer.GetCityRevenueList();
            return Json(revenueList);
        }

        public JsonResult StateRevenue()  
        {  
            var RevenueList = RevenueDataAccessLayer.GetStateRevenueList();
            return Json(RevenueList);  
        }  
        public IActionResult Index()  
        {  
            return View();  
        }  
        public IActionResult BarChart()  
        {  
            return View();  
        }  
        public IActionResult LineChart()  
        {  
            return View();  
        }  
        public IActionResult PieChart()  
        {  
            return View();  
        }  
}
