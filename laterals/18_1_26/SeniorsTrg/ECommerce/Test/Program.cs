

using System;
using Cataog;
class Program
{
    static void Main(string[] args)
    {

        Product product = new Product
        {
            Id = 1,
            Title = "Gerbera",
            Description = "Wedding flowers.",
            Price = 4.99M
        };


        Console.WriteLine($"Product Details: Id={product.Id}, Title={product.Title}, Description={product.Description}, Price={product.Price}");
        Console.WriteLine("Hello, World!");
    }
}