
# *The Security Gate, the Inspection Room, and the Quality Checker of MVC Palace*

> *"Imagine your MVC application is a high-security palace where each request is a visitor. Before the visitor meets the royal family (your controller and actions), they pass through several security checks, room inspections, and final approvals. These 'checks' are none other than our heroes — the **Filters**!"*


## 🧩 What Are Filters?

Filters are like **plug-in checkpoints** in the MVC processing pipeline.

They allow you to **hook into the execution** of your app at specific points — **before** or **after** something happens, like:

* A controller action runs,
* A view renders,
* Or even when an error explodes in the system.

## 🧰 Types of Filters – Meet the Five Guards

| Filter Type       | Timing                              | Example Use Cases                        |
| ----------------- | ----------------------------------- | ---------------------------------------- |
| **Authorization** | Before everything else              | Check if user is allowed                 |
| **Resource**      | Before/After model binding          | Performance logging, caching             |
| **Action**        | Before/After controller action runs | Logging, auditing                        |
| **Result**        | Before/After result is executed     | Modify result, custom headers            |
| **Exception**     | When an exception occurs            | Global error handling, fallback messages |

## 🛠️ Real-Life Example: Creating a Custom Action Filter

Let’s create a **custom action filter** to log requests entering and leaving any action.

### 🔧 Step 1: Create the Filter

```csharp
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation($"[START] Action: {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation($"[END] Action: {context.ActionDescriptor.DisplayName}");
    }
}
```

### 🔧 Step 2: Register It

#### 📍 Option A: Apply **Globally** to All Controllers

```csharp
services.AddControllersWithViews(options =>
{
    options.Filters.Add<LoggingActionFilter>();
});
```

#### 📍 Option B: Apply **Locally** to Specific Controller

```csharp
[TypeFilter(typeof(LoggingActionFilter))]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```

### 🔧 Step 3: Setup Dependency Injection

Make sure to **register the filter and logger** so that the DI system can provide them.

```csharp
services.AddScoped<LoggingActionFilter>(); // Register your filter
services.AddLogging(builder => builder.AddConsole()); // Logging service
```

## 🧪 Execution Flow – Behind the Curtain

When someone hits a URL:

1. 🔄 `OnActionExecuting()` runs first – Logging begins.
2. 🎯 Your action method executes – Business logic runs.
3. ✅ `OnActionExecuted()` runs last – Logging ends.

This lets you **wrap extra logic around your core logic** without touching controller code!

## 🧠 Summary – What Makes Filters Powerful?

✨ Filters help with **cross-cutting concerns**:

* Logging
* Authorization
* Error handling
* Caching
* Performance tracking

✅ Clean, DRY (Don't Repeat Yourself) code
✅ Easy to apply globally or selectively
✅ Enhances **separation of concerns**


## 🎯 Mentor's Tip

> “Whenever you feel tempted to repeat some code before or after multiple actions — like logging or validation — don’t copy-paste. **That’s a job for a filter!** Create it once and plug it wherever needed.”

