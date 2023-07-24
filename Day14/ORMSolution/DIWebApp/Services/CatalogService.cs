using DIWebApp.Interfaces;
namespace DIWebApp.Services
{
    //Each services should be defined by using contract
   public class ProductCatalogService : IProductCatalogService
    {

        public ProductCatalogService(){ }
        public bool Insert()
        {
            //Invoke BLL layer 
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