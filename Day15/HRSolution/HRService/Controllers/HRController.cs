using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using HRService.BOL;
using HRService.DAL;
namespace HRService.Controllers;

[ApiController]
[Route("[controller]")]
public class HRController : ControllerBase
{
    private readonly ILogger<HRController> _logger;

    public HRController(ILogger<HRController> logger)
    {
        _logger = logger;
    }
    [HttpGet(Name = "GetEmployees")]
    //[EnableCors()]
    public IEnumerable<Employee> Get(){
        Console.WriteLine("Get all employees");
        
        List<Employee> allEmp=new List<Employee>();
        DBManager dbm=new DBManager();
        allEmp=dbm.GetAll();
        return allEmp;
    }
}
