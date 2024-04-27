using ProductCatalog.Models;
using ProductCatalog.Repositories.Interfaces;

namespace ProductCatalog.Repositories
{

    public class ProductRepositoryJSON : IProductRepository
    {
        private IConfiguration _configuration;


        public ProductRepositoryJSON(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<IEnumerable<Product>> GetAll()
        {
            await Task.Delay(100);
            List<Product> products = new List<Product>();

            return products;
        }
        public async Task<Product> GetById(int productId)
        {
            Product product = new Product();
            await Task.Delay(100);

            return product;
        }
        public async Task<bool> Insert(Product product)
        {
            bool status = false;
            await Task.Delay(100);

            return status;
        }
        public async Task<bool> Update(Product product)
        {
            bool status = false;
            await Task.Delay(100);
            return status;
        }
        public async Task<bool> Delete(int productId)
        {
            bool status = false;
            await Task.Delay(100);
            return status;
        }
        public async Task<bool> HikePrice(double percentage)
        {
            bool status = false;
            await Task.Delay(100);
            return status;
        }
    }
}

