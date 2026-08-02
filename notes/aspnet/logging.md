# ASP.NET Core Logging with Serilog

## *Applying Logging as a Cross-Cutting Concern in Web Applications*

> *"Imagine you are managing a large hospital. Doctors treat patients, nurses provide care, pharmacists dispense medicines, and receptionists register patients. Each department has its own responsibility. But there is one team that works silently across every department—the CCTV and Security Control Room. They don't treat patients, yet they monitor every activity, record every event, detect problems, and help investigate incidents later.*

> *In software engineering, **Logging** plays exactly the same role. It is not part of the business logic, but it watches everything happening inside the application."*

# Why Do We Need Logging?

When students build their first Web API, everything works perfectly.

```
User --> Controller --> Service --> Repository --> Database
```

After deployment, a customer reports:

> "The application is slow."

Another says

> "I clicked Submit, but nothing happened."

Another reports

> "Payment failed."

How do you know what actually happened? 

Without logs...

```
Developer:"I don't know."
Customer:"It worked yesterday."
Manager:"Please fix it."
Developer:"I have no idea where to start."
```
Logging gives developers the answers.

# Logging is a Cross-Cutting Concern

Business modules are:

* Customer
* Product
* Order
* Policy
* Payment
* Claims

Logging is different. It applies to

* every controller
* every service
* every repository
* every exception
* every HTTP request

That is why Logging is called a **Cross-Cutting Concern**.

```
                    Logging
-------------------------------------------------
Customer Module        ✔
Policy Module          ✔
Payment Module         ✔
Claims Module          ✔
Authentication         ✔
Authorization          ✔
Database               ✔
External APIs          ✔
```

Logging crosses every layer.


# Typical ASP.NET Core Architecture

```
                Browser
                   │
                   ▼
             HTTP Request
                   │
                   ▼
           Middleware Pipeline
                   │
     ┌─────────────┴──────────────┐
     │                            │
 Authentication              Logging
     │                            │
     └─────────────┬──────────────┘
                   ▼
              Controller
                   │
              Business Service
                   │
               Repository
                   │
                MySQL
```

Notice

Logging is present before reaching the Controller.


# Logging Flow

Suppose user requests

```
GET /api/policies
```

The flow becomes
```
Request Arrives
↓
Logging Middleware
↓
Authentication
↓
Authorization
↓
Controller
↓
Business Logic
↓
Repository
↓
Database
↓
Response
↓
Logging Middleware
```

The middleware logs

* Request
* Response
* Execution Time
* Errors

without modifying business logic.



# Step 1 Install Packages

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
```

Optional

```bash
dotnet add package Serilog.Settings.Configuration
```

# Step 2 Configure Serilog

Program.cs

```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddControllers();
var app = builder.Build();
```

# Step 3 Create Request Logging Middleware

```
Middlewares/
    RequestLoggingMiddleware.cs
```

```csharp
using Serilog;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Log.Information("Incoming Request {Method} {Path}",
                        context.Request.Method,
                        context.Request.Path);

        await next(context);

        Log.Information("Response Status Code : {StatusCode}",
            context.Response.StatusCode);
    }
}
```

# Step 4 Register Middleware

```csharp
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

Every request now passes through the middleware.

# Step 5 Logging Inside Controller

```csharp
private readonly ILogger<PoliciesController> logger;

public PoliciesController(ILogger<PoliciesController> logger)
{
    this.logger = logger;
}
```

Now log

```csharp
[HttpGet]
public IActionResult GetPolicies()
{
    logger.LogInformation("Fetching all policies");
    return Ok();
}
```

# Different Log Levels

| Level       | Purpose                 |
| ----------- | ----------------------- |
| Trace       | Very detailed debugging |
| Debug       | Developer information   |
| Information | Normal application flow |
| Warning     | Unexpected but handled  |
| Error       | Operation failed        |
| Critical    | Application crash       |

Example

```csharp
logger.LogDebug("Repository Started");
logger.LogInformation("Customer Logged In");
logger.LogWarning("Policy Expiring Soon");
logger.LogError(exception,"Payment Failed");
logger.LogCritical("Database Server Down");
```


# Logging Exceptions

```csharp
try
{
    repository.Save();
}
catch(Exception ex)
{
    logger.LogError(ex,
        "Error while saving policy");

    throw;
}
```

# Logging in Service Layer

```csharp
public class PolicyService
{
    private readonly ILogger<PolicyService> logger;

    public PolicyService(
        ILogger<PolicyService> logger)
    {
        this.logger = logger;
    }

    public List<Policy> GetPolicies()
    {
        logger.LogInformation(
            "Getting policies from repository");

        return repository.GetAll();
    }
}
```

# Logging in Repository

```csharp
logger.LogInformation(
    "Executing SQL Query");

logger.LogDebug(
    "SELECT * FROM Policies");
```

# Logging During Authentication

```
User Login Attempt
↓
Username Received
↓
Password Validated
↓
JWT Generated
↓
Login Successful
```

Each step can be logged.

# Logging During Payment

```
Payment Started
↓
Policy Found
↓
Premium Calculated
↓
Payment Gateway Called
↓
Transaction Success
↓

Receipt Generated
```

Every important event becomes part of the application history.


# Logging in Insurance Management System

Imagine our Insurance System.

```
Customer purchases policy
↓
Controller
      "Purchase Request Received"
↓
Service
      "Eligibility Checking"
↓
Repository
      "Saving Policy"
↓

Payment Service
      "Payment Successful"
↓
Response
      "Policy Created Successfully"
```

If anything fails

```
Repository
↓
MySQL Connection Failed
↓
Log Error
↓
Return Exception
```

Developers immediately know where the problem occurred.

# Benefits of Logging

* Easier debugging
* Faster production issue diagnosis
* Performance monitoring
* Security auditing
* User activity tracking
* Exception analysis
* API usage monitoring
* Compliance and auditing support

# Mentor's Takeaway

> **"A good software engineer writes code that works. A great software engineer writes software that can explain what it was doing when something goes wrong."**

Logging is that explanation.

As applications grow from a few hundred lines to millions of lines of code, developers cannot rely on memory or guesswork. Well-designed logging provides a clear, chronological record of requests, business operations, database interactions, and exceptions. By implementing logging as a **cross-cutting concern** through middleware and dependency injection, you keep business logic clean while making the application observable, maintainable, and production-ready.

**Remember:** Logging is not just about recording errors—it's about understanding the complete story of your application's execution.