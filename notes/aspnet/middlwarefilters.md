## **1. Middleware – The Global Request Pipeline**

### **What is Middleware?**

* Middleware is **code that processes HTTP requests and responses** as they pass through the ASP.NET Core pipeline.
* Every incoming request goes through a **sequence of middleware components** before reaching your endpoint/controller, and responses pass back through them in reverse order.

📌 **Think of middleware as checkpoints** at the gate of your application:

* **Authentication middleware** → “Who are you?”
* **Logging middleware** → “Let’s record your visit.”
* **Static files middleware** → “You’re asking for an image? Here it is, no need to go further.”
* **Routing middleware** → “Which controller should handle this?”

### **Middleware Flow Example**

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("Before next middleware");
    await next.Invoke(); // Call the next middleware
    Console.WriteLine("After next middleware");
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from last middleware!");
});
```

🔹 **Order matters** — Middleware is executed in the order you register it.
🔹 Some middleware short-circuits the pipeline (e.g., static file middleware can stop further processing).

### **Common Built-in Middleware**

* `UseRouting()`
* `UseAuthentication()`
* `UseAuthorization()`
* `UseStaticFiles()`
* `UseEndpoints()`

## **2. Filters – Request Processing Inside MVC/Web API**

### **What are Filters?**

* Filters are **inside** the MVC/Web API execution pipeline, **after routing**.
* They provide **hook points** at specific stages in the processing of a controller action.

📌 **Think of filters as kitchen checks** after the order has reached the chef (controller):

* **Authorization filters** → Check if the chef is allowed to cook for this customer.
* **Action filters** → Prepare ingredients before and after cooking.
* **Exception filters** → Handle if the chef burns the dish.
* **Result filters** → Adjust presentation before serving.

### **Filter Types in ASP.NET Core**

1. **Authorization Filters**

   * Run first, before any other filter.
   * Example: `[Authorize]`
2. **Resource Filters**

   * Run before/after model binding.
   * Example: caching results to avoid re-executing actions.
3. **Action Filters**

   * Run before/after the action method executes.
   * Example: logging input/output.
4. **Exception Filters**

   * Catch unhandled exceptions.
5. **Result Filters**

   * Run before/after the action result executes.


### **Example: Custom Action Filter**

```csharp
public class LogActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Before action executes");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("After action executes");
    }
}
```

**Register Globally:**

```csharp
services.AddControllersWithViews(options =>
{
    options.Filters.Add<LogActionFilter>();
});
```

**Or per-controller/action:**

```csharp
[ServiceFilter(typeof(LogActionFilter))]
public class HomeController : Controller { }
```


## **3. Middleware vs Filters – When to Use**

| **Middleware**                                                             | **Filters**                                                         |
| -------------------------------------------------------------------------- | ------------------------------------------------------------------- |
| Runs for **all requests**, even static files or non-MVC endpoints          | Runs **only** inside MVC/Web API pipeline                           |
| Great for cross-cutting concerns like logging, authentication, compression | Best for logic tied to MVC actions (validation, model manipulation) |
| Configured in `Program.cs` via `app.Use...()`                              | Applied globally, per-controller, or per-action                     |
| Works at a lower level (HTTP pipeline)                                     | Works at a higher level (MVC pipeline)                              |


💡 **Rule of Thumb**:

* Use **middleware** for global concerns that apply to all requests (security headers, logging, exception handling).
* Use **filters** when the concern is specific to **MVC/Web API actions** (validation, action timing, output formatting).
