using BloggersWebAPI.Entities;
using Microsoft.VisualBasic;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//HTTP request handler functions mapping

app.MapGet("/products", () =>
{
    var products = new List<Product>
    {
        new Product { Id = 1, Name = "iPhone", Price = 10.99m },
        new Product { Id = 2, Name = "Galaxy Tab", Price = 19.99m },
        new Product { Id = 3, Name = "XBox", Price = 29.99m }
    };
    return products;
});
app.MapGet("/posts/ravit", () =>
{
    List<Post> posts = new List<Post>
    {
        new Post { Title = "Full Stack Development", Content = "360 degree Application Development" },
        new Post { Title = "Domain Centric App Development.", Content = "IT Solutions provide Online Automation processes for Businesses" },
    };
     Blog myBlog = new Blog { Title = "Transflower Blog", Description = "A blog about my Learning and  Teaching Passion", Posts = posts };
    return myBlog;
});


app.Run();