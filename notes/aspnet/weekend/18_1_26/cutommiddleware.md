### 🛣️ Middleware Pipeline Flow

- **Request Flow (Left to Right):**  
  When a client sends an HTTP request, it enters the pipeline starting from the left. Each middleware component can inspect, modify, or short-circuit the request before passing it to the next.

- **Response Flow (Right to Left):**  
  After reaching the final component (typically MVC), the response travels back through the pipeline in reverse order, allowing each middleware to process or modify the outgoing response.


### 🔧 Middleware Components Explained

| Middleware         | Purpose                                                                 |
|--------------------|-------------------------------------------------------------------------|
| **Exception Handling** | Catches unhandled exceptions and generates error responses.           |
| **HSTS Protocol**      | Enforces HTTPS by instructing browsers to always use secure connections. |
| **HTTPS Redirect**     | Redirects HTTP requests to HTTPS for secure communication.           |
| **Static Files**       | Serves static content like HTML, CSS, JS, and images directly.       |
| **Cookie Policy**      | Manages cookie consent and compliance with privacy regulations.      |
| **Auth**               | Handles authentication (e.g., verifying user identity).              |
| **Session**            | Manages user session data across requests.                           |
| **MVC**                | Processes requests using the Model-View-Controller pattern.          |


### 🧠 Why It Matters

This pipeline is central to how ASP.NET Core applications handle web traffic. Each middleware plays a role in security, performance, and functionality. Developers can customize this pipeline to suit their application's needs by adding, removing, or reordering components.



### 🧱 Basic Middleware Configuration in `Program.cs`

In ASP.NET Core, middleware is configured in the `Program.cs` file inside the `Main` method:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Session setup
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Authentication setup (example with cookies)
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "MyCookieAuth";
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Cookie policy
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

var app = builder.Build();

// 1. Exception Handling
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // 2. HSTS Protocol
}

// 3. HTTPS Redirect
app.UseHttpsRedirection();

// 4. Static Files
app.UseStaticFiles();

// 5. Cookie Policy
app.UseCookiePolicy();

// 6. Authentication
app.UseAuthentication();

// 7. Session
app.UseSession();

// 8. Routing and MVC
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```
 

### 🧩 Notes on Each Middleware

- **Order Matters:** Middleware is executed in the order it's added. The response flows back in reverse.
- **Custom Middleware:** You can create your own using `app.Use(...)` or `app.UseMiddleware<YourMiddleware>()`.
- **Session & Auth:** These require proper configuration in `ConfigureServices`.

 
 Let’s put everything together into a **real-world ASP.NET Core example** where we use:

- ✅ **Custom Middleware** (logging requests/responses)  
- ✅ **Authentication** (cookie-based login)  
- ✅ **Session** (store user-specific data)  


## 📂 Step 1: Configure Services in `Startup.cs`

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Session setup
    services.AddDistributedMemoryCache();
    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(20);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    // Authentication setup
    services.AddAuthentication("MyCookieAuth")
        .AddCookie("MyCookieAuth", options =>
        {
            options.Cookie.Name = "MyCookieAuth";
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

    services.AddControllersWithViews();
}
```

## 📂 Step 2: Configure Middleware Pipeline

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    // Custom middleware for logging
    app.UseRequestLogging();

    app.UseRouting();

    // Authentication & Session
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSession();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

## 📂 Step 3: Custom Middleware Example

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"➡️ Request: {context.Request.Method} {context.Request.Path}");

        await _next(context);

        Console.WriteLine($"⬅️ Response: {context.Response.StatusCode}");
    }
}

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
```

## 📂 Step 4: Using Authentication + Session in a Controller

```csharp
public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        if (username == "ravi" && password == "password123")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            // Store session data
            HttpContext.Session.SetString("UserRole", "Admin");

            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
```

### 🔑 What Happens Here
- **Custom Middleware** logs every request and response.  
- **Authentication** ensures only logged-in users can access protected routes.  
- **Session** stores user-specific data (like role or preferences).  


## 🔐 Step 1: Add Roles to Authentication

When signing in, we can assign roles to users via claims:

```csharp
public async Task<IActionResult> Login(string username, string password)
{
    if (username == "ravi" && password == "password123")
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin") // Assign role here
        };

        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("MyCookieAuth", principal);

        HttpContext.Session.SetString("UserRole", "Admin");

        return RedirectToAction("Index", "Home");
    }
    else if (username == "guest" && password == "guest123")
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "User") // Different role
        };

        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("MyCookieAuth", principal);

        HttpContext.Session.SetString("UserRole", "User");

        return RedirectToAction("Index", "Home");
    }

    return View();
}
```

## 🛂 Step 2: Protect Controllers/Actions with `[Authorize]`

You can restrict access based on roles:

```csharp
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}

[Authorize(Roles = "User")]
public class UserController : Controller
{
    public IActionResult Profile()
    {
        return View();
    }
}
```

Or even at the **action level**:

```csharp
public class HomeController : Controller
{
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return Content("Welcome Admin!");
    }

    [Authorize(Roles = "User")]
    public IActionResult UserOnly()
    {
        return Content("Welcome User!");
    }

    [AllowAnonymous]
    public IActionResult Public()
    {
        return Content("Anyone can see this.");
    }
}
```

## 🧩 Step 3: Middleware + Session Integration

Since we stored the role in session (`HttpContext.Session.SetString("UserRole", "Admin")`), you can also use it for custom logic in middleware:

```csharp
public class RoleLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RoleLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var role = context.Session.GetString("UserRole") ?? "Guest";
        Console.WriteLine($"User Role: {role}");

        await _next(context);
    }
}

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRoleLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RoleLoggingMiddleware>();
    }
}
```

Register it in `Startup.cs`:
```csharp
app.UseRoleLogging();
```

### 🔑 What You Get
- **Admins** can access `/Admin/Dashboard` and admin-only actions.  
- **Users** can access `/User/Profile` and user-only actions.  
- **Guests** (not logged in) only see public pages.  
- Middleware logs the role for every request.  


👉 This is now a **complete pipeline**:  
- Custom middleware (logging + role tracking)  
- Authentication (cookie-based login)  
- Authorization (role-based access)  
- Session (store user role/state)  

