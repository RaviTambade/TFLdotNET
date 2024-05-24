using BloggersWebAPI.Entities;
using Microsoft.VisualBasic;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//HTTP request handler functions mapping

app.MapGet("/products", () =>
{
    var products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", Price = 10.99m },
        new Product { Id = 2, Name = "Product 2", Price = 19.99m },
        new Product { Id = 3, Name = "Product 3", Price = 29.99m }
    };
    return products;
});
app.MapGet("/posts/ravi", () =>
{
    List<Post> posts = new List<Post>
    {
        new Post { Title = "This is my first post on the blog.", Content = "sdfjlasjflasjldfjsaldfjlsadjflsadj" },
        new Post { Title = "This is my second post on the blog.", Content = "sdfyutyutyu 888888888888888888888888 adj" },
    };
     Blog myBlog = new Blog { Title = "My Personal Blog", Description = "A blog about my life and interests", Posts = posts };
    return myBlog;
});


app.Run();