# Filters

In ASP.NET Core, filters are a powerful mechanism for executing code before or after specific stages in the request processing pipeline. They can be used for a variety of purposes such as logging, authorization, exception handling, and caching. Here’s a sample approach to using filters in ASP.NET Core:

### 1. **Types of Filters**

ASP.NET Core provides several types of filters:

- **Authorization Filters:** Determine if a request is authorized before executing the action method.
- **Resource Filters:** Implement logic before and after model binding.
- **Action Filters:** Execute logic before and after an action method is called.
- **Result Filters:** Execute logic before and after the execution of action results.
- **Exception Filters:** Handle exceptions that occur during request processing.

### 2. **Creating a Custom Action Filter**

Let's create a sample custom action filter to log requests and responses:

```csharp
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Log the start of the action execution
        _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' starting...");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Log the completion of the action execution
        _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' completed.");
    }
}
```

### 3. **Registering the Action Filter**

You can register the action filter globally for all controllers or apply it selectively:

#### Global Registration (Startup.cs):

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews(options =>
    {
        options.Filters.Add<LoggingActionFilter>();
    });
}
```

#### Applying Filter to Specific Controller or Action Method:

```csharp
[TypeFilter(typeof(LoggingActionFilter))]
public class SampleController : Controller
{
    // Controller actions
}
```

### 4. **Using Dependency Injection**

Ensure that the dependencies required by the filter (such as ILogger in our example) are registered with the dependency injection container.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    services.AddLogging(builder =>
    {
        builder.AddConsole(); // Example: Configure logging
    });
    services.AddScoped<LoggingActionFilter>(); // Register the filter
}
```

### 5. **Executing the Action Filter**

When a request is made to an action method in the `SampleController`, the `LoggingActionFilter` will execute its `OnActionExecuting` method before the action method is called, and its `OnActionExecuted` method after the action method completes.

### Summary

Filters in ASP.NET Core provide a flexible way to add cross-cutting concerns to your application. They can be applied globally or selectively to controllers and actions, allowing you to encapsulate reusable logic for logging, authorization, caching, and more. Custom filters like the `LoggingActionFilter` shown above enhance code reusability and maintainability by centralizing common functionality across your application.