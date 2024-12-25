namespace Catalog;


//Business entity class for Product
//POCO class for Product
public class Product
{

    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Category { get; set; }
    public string Id { get; set; }

    public Product(string name, decimal price, string description, string imageUrl, string category, string id)
    {
        Name = name;
        Price = price;
        Description = description;
        ImageUrl = imageUrl;
        Category = category;
        Id = id;
    }

     
    ~Product()
    {
    }

}
