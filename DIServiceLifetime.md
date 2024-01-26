# Service LifeTime in ASP.NET Core MVC
Dependency injection (DI) is a technique for achieving loose coupling between objects and their collaborators, or dependencies. Mostly, classes declare their dependencies via  their constructors. We call it Constructor Dependency Injection. To implement Constructor Dependncy Injection, we need to configure external services  with Dependency Container at Startup defiend in Program.cs file. The lifeteime of  service depends on how dependencies configured using builder instance in Program.cs file.

## Lifetime of the services using Constructor Dependency Injection
1. <b>AddTransient:</b>
Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
2. <b>AddScoped:</b>
Scoped lifetime services are created once per request.
3. <b>AddSingleton:</b>
Singleton lifetime services are created the first time they are requested (or when ConfigureServices is run if you specify an instance there) and then every subsequent request will use the same instance.

```
//Program.cs file

builder.Services.AddTransient<ILeaveService, LeaveService>();
builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddSingleton<IHRService, HRService>();
```

- With a transient service, a new instance is provided every time an instance is requested whether it in the scope of same HTTP request  or across different HTTP requrests.
- With a scoped service we get  the same instance within the scope of a given HTTP requirest but a new instance across different HTTP requests.
- With Singleton service , there is only one instance  shared. An instance is created , when service  is first requested  and that single instance will be used by all subsequent HTTP request throughout the application.
