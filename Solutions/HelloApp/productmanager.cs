namespace catalog;

//Business Logic class
public class ProductManager{
    public bool Insert(Product prd){
        return false;
    }
    public bool Update(Product prd){
        // Logic to update the product
        // For example, you can update the product in a list or database
        // Here, we will just return true to indicate the product was updated successfully
        return true;
    }
    public bool Delete(int productId){
        return false;
    }
    public List<Product> GetAll(){

      List<Product> allProducts=new List<Product>();  //empty list
        
        allProducts.Add(new Product{
            Id = 1,
            Title = "Rose",
            Description = "Valentine Flower",
            Quantity = 6000,
            Price = 12.0
        });
        allProducts.Add(new Product{
            Id = 2,
            Title = "Tulip",
            Description = "Spring Flower",
            Quantity = 3000,
            Price = 8.0
        });
        allProducts.Add(new Product{
            Id = 3,
            Title = "Sunflower",
            Description = "Summer Flower",
            Quantity = 1500,
            Price = 10.0
        });
        return allProducts;
    }

    public Product GetProductById(int productId){
        // Logic to get the product by Id
        // For example, you can retrieve the product from a list or database
        // Here, we will just return a new product for demonstration purposes
        return new Product{
            Id = productId,
            Title = "Rose",
            Description = "Valentine Flower",
            Quantity = 6000,
            Price =12
        };
    }
}
