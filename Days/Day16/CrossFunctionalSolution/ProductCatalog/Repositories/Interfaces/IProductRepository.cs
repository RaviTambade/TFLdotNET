using ProductCatalog.Models;

namespace ProductCatalog.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int productId);
        Task<bool> Insert(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(int ProductId);
        Task<bool> HikePrice(double percentage);
    }
}
