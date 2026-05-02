using DIWebApp.Interfaces;
namespace DIWebApp.Services
{
   public class ProductCatalogService : IProductCatalogService
    {
        public ProductCatalogService(){ }
        public bool Insert()
        {
            bool status=true;
            return status;      
        }
        public bool Update()
        {
             bool status=false;
            return status; 
        }
        public bool Delete()
        {
             bool status=false;
            return status;
        }
    }
}
