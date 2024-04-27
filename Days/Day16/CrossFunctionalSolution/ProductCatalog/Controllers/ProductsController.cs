using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsrv;
        private readonly ILogger<ProductsController> _logger;

        //Parametrized constructor
        public ProductsController(IProductService productsrv, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _productsrv = productsrv;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> products = await _productsrv.GetAll();
            _logger.LogInformation("Get all products method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return products;
        }

        [HttpGet]
        [Route("getproductdetails/{id}")]
        public async Task<Product> GetById(int id)
        {
            Product product = await _productsrv.GetById(id);
            _logger.LogInformation("Get details of product method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return product;
        }

        // [Authorize(Roles = Role.Admin)]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<bool> UpdateAsync(int id, [FromBody] Product product)
        {
            Product oldProduct = await _productsrv.GetById(id);
            if (oldProduct.ProductId == 0)
            {
                return false;
            }
            product.ProductId = id;
            bool status = await _productsrv.Update(product);
            _logger.LogInformation("Update product method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return status;
        }

        //[Authorize(Roles = Role.Admin + "," + Role.Customer)]
        [HttpPost]
        [Route("addproduct")]
        public async Task<bool> Insert([FromBody] Product product)
        {
            bool status = await _productsrv.Insert(product);
            _logger.LogInformation("Insert product method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return status;
        }

        // [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<bool> Delete(int id)
        {
            bool status = await _productsrv.Delete(id);
            _logger.LogInformation("Delete product method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return status;
        }

        [HttpPut]
        [Route("hikeprice/{id}")]
        public async Task<bool> HikePrice(double id)
        {
            bool status = await _productsrv.HikePrice(id);
            _logger.LogInformation("Hike price of products method invoked at  {DT}", DateTime.UtcNow.ToLongTimeString());
            return status;

        }
    }
}
