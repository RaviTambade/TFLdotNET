var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var app = builder.Build();

//API to get machine data
app.MapGet("/api/machinedata", () =>
{ 
    var machinedata=new[] {
        new { MachineId = 1, Status = "Running", Temperature = 75.5, LastMaintenance = DateTime.Now.AddDays(-10) },
        new { MachineId = 2, Status = "Stopped", Temperature = 0.0, LastMaintenance = DateTime.Now.AddDays(-30) },
        new { MachineId = 3, Status = "Running", Temperature = 80.0, LastMaintenance = DateTime.Now.AddDays(-5) }
    };
    return machinedata;
});

app.MapGet("/api/factoryenvironment", () =>
{
    var  factoryenvironment = new[] {
        new { FactoryId = 1, Environment = "Normal", Humidity = 50.0, Pressure = 1013.25 },
        new { FactoryId = 2, Environment = "Critical", Humidity = 80.0, Pressure = 980.0 },
        new { FactoryId = 3, Environment = "Normal", Humidity = 45.0, Pressure = 1015.0 }
    };
    return factoryenvironment;
});

app.Run();