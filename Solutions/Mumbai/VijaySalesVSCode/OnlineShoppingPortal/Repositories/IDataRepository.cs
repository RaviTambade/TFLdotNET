
using OnlineShoppingPortal.Models;
namespace OnlineShoppingPortal.Repositories
{
    public interface IDataRepository
    {
        //curd operations methods
        List<Product> GetAll ();
        Product GetById(int id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
