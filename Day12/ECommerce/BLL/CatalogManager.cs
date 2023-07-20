namespace BLL;
using BOL;
using DAL;
public class CatalogManager
{
    public List<Product> GetAllProducts(){
        List<Product> allProducts = new List<Product>();
        //allProducts=DBManager.GetAllProducts();
        allProducts=DBManager.GetAllProductsFromDatabase();
        //Apply Bussiness Logic as Company Policy
        //foreach ( Product p in allProducts)
        //{
        //   get each unit price of Product P
        //  unitprice of p= existing product unitprice+
        //                      5% TAX + 15% gst + 2% Sales Tax

        //}
        List<Product> updatedAllProducts = allProducts;
        return updatedAllProducts;
    }

     public Product GetProduct(int  id){
      List<Product> allProducts=GetAllProducts();
        //Bussiness Logic for receiving a particular Product 
        //Whose id matching

        //Arrow Function 
        //Lambda Express
        //Anonymous Delegate
        Product foundProduct = allProducts.Find((product) => product.ProductId == id);
        return foundProduct;

        //LINQ :Language Integrated Query
        //concept of C# language
        /*var product=from  p in allProducts 
         *                  where p.ProductId ==id 
         *                  select p  ;
        return product as Product;

        */


   

    }
}