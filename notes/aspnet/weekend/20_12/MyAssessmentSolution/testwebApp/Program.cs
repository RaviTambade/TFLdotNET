//Entry Point function

public class Program
{
    public static void Main(string [] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //Singleton object

        var app = builder.Build();

        //HTTP request mapping with HttpRequest handler
        //Add services for web appliction

        //Add Middleware for web appliation
        //Map incomming HTTP urls with callback function

        app.MapGet("/", () => "Hello World!");
        app.MapGet("/transflower",()=>"Welcome to Transflower");
        app.MapGet("/aboutus",()=>"<h2> TFL Portal</h2>");
        //Keep asp.net core application listening on port number

        app.Run();
        
    }
}
