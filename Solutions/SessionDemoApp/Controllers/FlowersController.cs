using Microsoft.AspNetCore.Mvc;
using Core.Services.Interfaces;

namespace SessionDemoApp.Controllers;

public class FlowersController : Controller
    {
     private readonly ILogger<FlowersController> _logger;
         private readonly IFlowerService _flowerService;
         private readonly IConfiguration _configuration;
         public FlowersController(IConfiguration configuration, ILogger<FlowersController> logger, IFlowerService flowerService)
        {
            _flowerService = flowerService;
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var itemsSold = _flowerService.GetAll();
            _logger.LogInformation("FlowersController Index action called.");
            string connectionString = _configuration.GetConnectionString("ecommerceconnection");


            return View(itemsSold);
        }
    }