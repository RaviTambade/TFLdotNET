using SimpleBlazorApp.Entities;
namespace SimpleBlazorApp.Services;
public class ProductService
{
    private List<Product> products;
    public async   Task<List<Product>> GetAll()
    {
        await Task.Delay(5000);
        this.products = new List<Product>();
        products.Add(new Product{ Id = 1, Title="Gerbera",Description="Wedding Flower",UnitPrice=5, Quantity=5000});
        products.Add(new Product{ Id = 2, Title="Rose",Description="Wedding Flower",UnitPrice=5, Quantity=765});
        products.Add(new Product{ Id = 3, Title="Lily",Description="Wedding Flower",UnitPrice=5, Quantity=1500});
        products.Add(new Product{ Id = 4, Title="Marigold",Description="Wedding Flower",UnitPrice=5, Quantity=320});
        products.Add(new Product{ Id = 5, Title="Jasmine",Description="Wedding Flower",UnitPrice=5, Quantity=560});
        return  products;
    }
}
