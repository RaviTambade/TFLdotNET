using AuthLib;
using AuthLib.Entities;
using AuthLib.Repositories;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//I have written two REST API for urls

app.MapGet("/api/catalog", () =>
{
    string[] products = { "iPhone", "Galaxy", "HP Pavillion", "Sony PlayStation"};
    return products;
});

app.MapGet("/api/customers", () =>
{
    string[] products = { "Microsoft", "IBM", "Oracle", "Tata Motors" };
    return products;
});

app.MapGet("/api/login", () =>
{
    string message = "";
   // AuthManager mgr=new AuthManager();
   // bool status = mgr.Login("ravi", "ravi");
   
    CredentialRepository repo=new CredentialRepository();
    bool status = repo.Validate("ravi.tambade@transflower.in", "ravi");
        
    if (status) {
            message = "User is  trusted User";
    }
    else {
        message = "Invalid User";
    }
    return message;
});

app.MapGet("/api/register", () =>
{
    string message = "User is successfully registered";
    return   message = "User is successfully registered";
});

app.Run();