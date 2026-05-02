using Microsoft.AspNetCore.Mvc;
namespace ProductCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("Get all customers method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return new string[] { "IBM", "Microsoft" };
        }
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogInformation("Get single customer details  method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return "value";
        }
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _logger.LogInformation("Inserting new  customer details  method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _logger.LogInformation("Updating  existing customer details  method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation("Deleting existing  customer details  method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
        }
    }
}
