using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<CustomersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("Get all customers method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return new string[] { "IBM", "Microsoft" };
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogInformation("Get single customer details  method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return "value";
        }

        // POST api/<CustomersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _logger.LogInformation("Inserting new  customer details  method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());


        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _logger.LogInformation("Updating  existing customer details  method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());

        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            _logger.LogInformation("Deleting existing  customer details  method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());


        }
    }
}
