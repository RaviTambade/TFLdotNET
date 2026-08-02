# Saving Log Entries to a Log File using Serilog

## *Cross-Cutting Logging in ASP.NET Core*

> *"Imagine a security guard at a company gate. Every visitor who enters or leaves is recorded in a register with the date, time, purpose, and person's name. Months later, if an incident occurs, the management doesn't rely on memory—they open the register. A log file in a web application serves the same purpose. It is the application's permanent diary."*

# Why Save Logs to a File?

Console logs are useful during development.

```text
Application Running...
Request Received
Database Connected
Policy Created
```

But once the application is deployed on a server:

* Nobody watches the console.
* If the application restarts, console logs disappear.
* Developers need historical records for troubleshooting.

That's why production applications save logs to **files**.


# Architecture

```text
              Browser
                  │
                  ▼
          ASP.NET Core Web API
                  │
                  ▼
             Serilog Logger
          ┌────────┴─────────┐
          │                  │
      Console Sink      File Sink
                              │
                              ▼
                 Logs/log-2026-08-02.txt
```

One log entry is written to multiple destinations.

# Step 1 Install Required Packages

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
```

# Step 2 Configure Serilog

**Program.cs**

```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate:
        "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddControllers();
var app = builder.Build();
```

# What Does This Configuration Mean?

```csharp
.WriteTo.File(
    "Logs/log-.txt",
    rollingInterval: RollingInterval.Day)
```

| Setting               | Meaning                           |
| --------------------- | --------------------------------- |
| `Logs`                | Folder where log files are stored |
| `log-.txt`            | Base filename                     |
| `RollingInterval.Day` | Creates a new file every day      |

Generated files:

```text
Logs/
    log-2026-08-01.txt
    log-2026-08-02.txt
    log-2026-08-03.txt
```

# Logging an Information Message

```csharp
logger.LogInformation("Fetching all policies");
```
Saved in the log file as:

```text
2026-08-02 10:15:25 [INF] Fetching all policies
```

# Logging a Warning

```csharp
logger.LogWarning("Policy expires within 7 days.");
```

Log entry:

```text
2026-08-02 10:16:05 [WRN] Policy expires within 7 days.
```

# Logging an Error

```csharp
try
{
    repository.Save(policy);
}
catch(Exception ex)
{
    logger.LogError(ex,
        "Error while saving policy");
}
```

Log file:

```text
2026-08-02 10:18:10 [ERR] Error while saving policy

System.NullReferenceException
...
Stack Trace...
```

# Logging Every HTTP Request

Middleware:

```csharp
public async Task Invoke(HttpContext context)
{
    Log.Information("Incoming {Method} {Path}",context.Request.Method, context.Request.Path);
    await _next(context);

    Log.Information("Response {StatusCode}", context.Response.StatusCode);
}
```

Log file:

```text
2026-08-02 10:20:05 [INF] Incoming GET /api/policies
2026-08-02 10:20:05 [INF] Response 200
```

# Example Log File

```text
2026-08-02 09:00:01 [INF] Application Started
2026-08-02 09:00:15 [INF] Incoming GET /api/policies
2026-08-02 09:00:15 [INF] Fetching Policies
2026-08-02 09:00:16 [INF] SQL Executed Successfully
2026-08-02 09:00:16 [INF] Response 200
2026-08-02 09:05:42 [ERR] Database Connection Failed
System.TimeoutException...
```

# Folder Structure

```text
InsuranceManagementSystem

│
├── Controllers
├── Services
├── Repositories
├── Models
├── Logs
│      log-2026-08-01.txt
│      log-2026-08-02.txt
│      log-2026-08-03.txt
│
├── Program.cs
├── appsettings.json
```

# Advanced File Configuration

```csharp
.WriteTo.File("Logs/log-.txt",rollingInterval: RollingInterval.Day,retainedFileCountLimit: 30,
    fileSizeLimitBytes: 10_000_000,rollOnFileSizeLimit: true,shared: true)
```

| Option                   | Description                                             |
| ------------------------ | ------------------------------------------------------- |
| `RollingInterval.Day`    | Creates a new file every day                            |
| `retainedFileCountLimit` | Keeps only the latest 30 log files                      |
| `fileSizeLimitBytes`     | Maximum size of a log file (10 MB)                      |
| `rollOnFileSizeLimit`    | Creates a new file when the size limit is reached       |
| `shared`                 | Allows multiple processes to write to the same log file |


# Production Logging Flow

```text
User Request
      │
      ▼
Logging Middleware
      │
      ▼
Controller
      │
      ▼
Service
      │
      ▼
Repository
      │
      ▼
MySQL Database
      │
      ▼
Response
      │
      ▼
Serilog writes entries
      │
      ▼
Logs/log-2026-08-02.txt
```

Every request, warning, error, and exception becomes part of a searchable audit trail.

# Mentor's Takeaway

> **"Code tells us what the application is supposed to do. Log files tell us what the application actually did."**

Professional software teams rarely debug production issues by guessing. They inspect log files to trace the sequence of events, identify failures, and understand user behavior. By configuring Serilog to write structured log entries to rolling log files, you make your ASP.NET Core application easier to monitor, maintain, and support in real-world production environments.