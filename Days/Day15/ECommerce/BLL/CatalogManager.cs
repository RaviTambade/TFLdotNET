namespace BLL;
using BOL;
using DAL;
public class CatalogManager
{
    public List<Product> GetAllProducts(){
        List<Product> allProducts = new List<Product>();
        allProducts=DBManager.GetAllProducts();
        return allProducts;
    }

     public Product GetProduct(int  id){
      List<Product> allProducts=GetAllProducts();
      /*var product=from  p in allProducts
                   where p.ProductId ==id
                    select p  ;     
    */
       Product foundProduct=allProducts.Find((product)=>product.ProductId ==id);
      return foundProduct ;
     }


     public bool Update(Product product){
        return true;
     }
}