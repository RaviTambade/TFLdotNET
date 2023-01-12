var builder = WebApplication.CreateBuilder(args);


//*************************************************************
// Install the Microsoft.AspNetCore.Cors Nuget package.
 builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//******************************************************

app.UseCors(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
});

/*
We have also used other method which are described below:
1. AllowAnyMethod() – To allow all HTTP methods.
2. AllowAnyHeader() – To allow all request headers.
3. AllowCredentials() – the server must allow the credentials.
*/


app.UseAuthorization();
app.MapControllers();
app.Run();
