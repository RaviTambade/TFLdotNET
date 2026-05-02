namespace BLL;
using BOL;
using DAL;
public class CatalogManager
{
    public List<Product> GetAllProducts(){
        List<Product> allProducts = new List<Product>();
        allProducts=DBManager.GetAllProductsFromDatabase();
        List<Product> updatedAllProducts = allProducts;
        return updatedAllProducts;
    }
     public Product GetProduct(int  id){
      List<Product> allProducts=GetAllProducts();
        Product foundProduct = allProducts.Find((product) => product.ProductId == id);
        return foundProduct;
    }
}
