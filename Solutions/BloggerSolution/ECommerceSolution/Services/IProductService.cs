using ECommerceSolution.Models;
namespace ECommerceSolution.Services
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        void Remove(int id);
        void Insert(Product product);
        void Update(Product product);
    }
}
