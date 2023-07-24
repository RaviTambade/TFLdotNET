namespace DIWebApp.Services
{
    //Each services should be defined by using contract
    public interface IProductCatalogService
    {
        bool Insert();
        bool Update();
        bool Delete();
    }

    public class ProductCatalogService : IProductCatalogService
    {
        public ProductCatalogService(){ }
        public bool Insert()
        {
            bool status=false;
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