 
👨‍🏫 **JWT Authentication in ASP.NET Core**

“Alright team, imagine we are setting up the security gate for our application. Just like in a society or office, you don’t let people in without verifying who they are. In software, we do this using **authentication**, and in modern web APIs, the most common mechanism is the **JWT token**.

Now let’s see how we build this flow step by step.

 

### 🔹 Step 1: Restoring the Essentials

When we start an ASP.NET Core Web API project, we always run:

```sh
dotnet restore
```

Why?
Because this command looks at your **ItemGroup** inside the `.csproj` file, checks all package references, and downloads them if missing. Think of it as going to the market with a shopping list—if sugar, salt, and tea powder are missing, restore will bring them home.

Here, one of the important packages we’ll rely on is:

* `Microsoft.AspNetCore.Authentication.JwtBearer`

This package helps us in verifying JWT tokens.

 

### 🔹 Step 2: Organizing with Clean Architecture

I always tell my students—**a clean folder structure makes you a disciplined developer.**

So we create folders:

* **Entities** (Business Entity classes → e.g., `User.cs`)
* **Services** (Business logic, contracts, implementations)
* **Models** (Data transfer objects → request/response models)
* **Controllers** (API endpoints)

This separation makes life easier when your project grows.

  

### 🔹 Step 3: Service Contracts (Interfaces)

We start by defining an **interface** inside `Services` folder:

```csharp
namespace WebApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
```

This is just like writing the **rule book** before building the actual team. The interface enforces that whoever implements it must provide these behaviors.

 

### 🔹 Step 4: Models for Request and Response

Inside the **Models** folder we define:

📌 **AuthenticateRequest.cs**

```csharp
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
```

Here, `[Required]` is a **Data Annotation Attribute**. It ensures no one can send a blank username or password. This comes from `System.ComponentModel.DataAnnotations`.

📌 **AuthenticateResponse.cs**

```csharp
using WebApi.Entities;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Token = token;
        }
    }
}
```

Notice:
This class combines the **User details** with the **JWT token**. Just like an entry pass card contains your photo + ID + access code.

  

### 🔹 Step 5: Implementing the Service

Now let’s implement `IUserService` in `UserService.cs`:

```csharp
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        // Hardcoded user list (later we can move this to DB)
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Ravi", LastName = "Tambade", Username = "ravi", Password = "password" }
        };

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // if no user found → return null
            if (user == null) return null;

            // generate JWT token
            var token = "GENERATED_JWT_TOKEN"; // later we’ll implement real JWT generation
            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll() => _users;

        public User GetById(int id) => _users.FirstOrDefault(x => x.Id == id);
    }
}
```

For now, I hardcoded one user. But in real apps, we’ll pull data from a database. The key point: **Authenticate() returns a response that includes the token.**

 

### 🔹 Step 6: JWT Token Generation (coming next)

In reality, we won’t use `"GENERATED_JWT_TOKEN"`. We’ll implement a **JWT Utility Class** that signs a token with a secret key. That’s like stamping the access card with a hologram—so no one can forge it.

---

✅ By now, you’ve seen how:

* `dotnet restore` gets us the right packages.
* We structured our solution cleanly.
* Defined request/response models.
* Created interface + implementation for user authentication.
* Prepared for JWT token integration.

👉 Next step in our journey will be writing the **JWT Helper** that actually generates the signed token.



👨‍🏫 ** Part 2: JWT Token with User Collection and AppSettings**

“Team, last time we saw how to structure our project and return a fake token. But no society allows fake passes for long, right? Security must be **real**.

So today, let’s build our authentication flow properly with:

1. A **user collection** for testing.
2. **LINQ queries** for searching users.
3. **JWT token generation** using a **secret key from appsettings.json**.



### 🔹 Step 1: The User Collection (Data Members)

Inside `UserService`, we define a private list of users. Think of it as our **mini database**:

```csharp
using WebApi.Entities;
using WebApi.Models;
using Microsoft.Extensions.Options;
using System.Linq;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        // ✅ Line #20: private user collection
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Ravi", LastName = "Tambade", Username = "ravi", Password = "password" },
            new User { Id = 2, FirstName = "Pratik", LastName = "Patil", Username = "pratik", Password = "12345" }
        };

        // constructor with dependency injection for AppSettings
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
```

Here:

* `_users` = mock data.
* Later, we’ll replace this with **database calls**.
* For now, good enough to test authentication.

---

### 🔹 Step 2: GetAll & GetById with LINQ

Now, let’s fetch users. Earlier, you used `FirstOrDefault` in your notes. Let’s implement it:

```csharp
        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
```

See how **LINQ makes it simple**:

* No `for loop`, no manual checks.
* Just one compact expression.
* `FirstOrDefault` means → return first matching element OR return null if none found.


### 🔹 Step 3: AppSettings for Secret Key

Now, JWT requires a **secret key** to sign tokens.
So, in `appsettings.json` we add:

```json
{
  "AppSettings": {
    "Secret": "THIS_IS_MY_SUPER_SECRET_KEY"
  }
}
```

This is like the **master stamp** for your access card.
Without this key, no token can be validated.


### 🔹 Step 4: The AppSettings Model

We need a class to map this JSON:

📌 **AppSettings.cs**

```csharp
namespace WebApi.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
```

### 🔹 Step 5: JWT Token Generation in Authenticate

Now comes the fun part → the **Authenticate()** method.

```csharp
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => 
                x.Username == model.Username && x.Password == model.Password);

            // ❌ If user not found → return null
            if (user == null) return null;

            // ✅ JWT Token creation
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7), // token validity
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return new AuthenticateResponse(user, jwtToken);
        }
    }
}
```

### 🔹 Step 6: Registering AppSettings in Program.cs

Now we must tell ASP.NET Core to use `AppSettings`:

📌 In **Program.cs**

```csharp
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));
```

This enables **Dependency Injection** of the secret key into `UserService`.

---

✅ So, now the flow is complete:

1. **AuthenticateRequest** → Username/Password.
2. **Check in \_users list** with LINQ.
3. If found → **generate JWT token** with secret.
4. Return **AuthenticateResponse** with user info + token.



👨‍🏫 **Mentor’s Wrap-up**
“See team, we just implemented a **mini authentication system**. We moved from fake tokens → real tokens, from hardcoded values → secrets in `appsettings.json`. This is exactly how professional applications are built.”



👉 Next, I can walk you through **how to secure your API endpoints** so only valid JWT tokens can access them.


👨‍🏫 ** Part 3 – GenerateJWTToken Helper**

“Team, we already have our **Authenticate()** method working, but right now it’s a little messy because we wrote the token generation logic directly inside it.

👉 Best practice says: *‘Never clutter your core method. Put specialized logic in helper functions.’*

So today, we will **extract token creation into a helper function** inside our `UserService` class. Think of it like calling a government printing press to make an official passport instead of making one at your home printer.


### 🔹 Step 1: Create the Helper Method

At the **bottom of the `UserService` class**, let’s add:

```csharp
private string GenerateJwtToken(User user)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username)
            // we can add more claims if needed, like role, email, etc.
        }),
        Expires = DateTime.UtcNow.AddDays(7), // token valid for 7 days
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
        )
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}
```

### 🔹 Step 2: Update Authenticate() to Call Helper

Now, instead of creating the token inline, we call our helper:

```csharp
public AuthenticateResponse Authenticate(AuthenticateRequest model)
{
    var user = _users.SingleOrDefault(x => 
        x.Username == model.Username && x.Password == model.Password);

    if (user == null) return null;

    // ✅ Call helper method
    var jwtToken = GenerateJwtToken(user);

    return new AuthenticateResponse(user, jwtToken);
}
```

### 🔹 Step 3: Claims Explained

“Now, students, let’s pause. What exactly is this `ClaimsIdentity` we are setting?”

Think of **claims** as *‘what a person says about themselves, and what the government confirms’*.

* `ClaimTypes.Name` → Passport ID (we stored `user.Id`).
* `ClaimTypes.NameIdentifier` → Username (unique handle).
* We can add more like:

  * `ClaimTypes.Role` → Admin / Student / Teacher.
  * `ClaimTypes.Email` → Verified Email.

When the token is verified, these claims become available in your controllers.


### 🔹 Step 4: Security Aspects

Why do we use:

* **SymmetricSecurityKey**? → One secret key for both signing & verifying.
* **HmacSha256**? → Secure hashing algorithm.
* **Expires = 7 days**? → Like a visa validity. After 7 days, token is invalid, and user must log in again.

👉 This is how **modern apps remain secure** while avoiding storing passwords everywhere.


### 🔹 Step 5: Big Picture Recap

So now the flow looks like this:

1. User sends **username & password**.
2. `Authenticate()` checks inside `_users` (LINQ).
3. If valid → `GenerateJwtToken(user)` is called.
4. Token is **signed with secret key from appsettings.json**.
5. Response returns user info + signed JWT token.

That token can now be attached to every API request as a **Bearer token** in headers.


👨‍🏫 **Mentor’s Wrap-up**
“See team, how we pulled token generation into its own helper method? This makes our service cleaner, reusable, and much easier to test.

Always remember: **Authentication logic = who the user is**,
**JWT helper = giving them a passport with an expiry date.**”



### 📖 From *appsettings.json* to `UsersController`

When you start building a **secure Web API** in ASP.NET Core, there are always a few puzzle pieces to put together. Let me walk you through how a team of developers discovered this path.


#### 🔑 Step 1: The Secret in `appsettings.json`

One day, the junior developer asked:
*“Sir, where should I keep my JWT secret key?”*

The senior smiled and said:
*“In the appsettings.json file. That’s our configuration home.”*

So they wrote something like this:

```json
{
  "AppSettings": {
    "Secret": "ThisIsMySecretKey123!"
  }
}
```


#### 📦 Step 2: Strongly-Typed Settings (The Wrapper Class)

But how do we read this secret?

The mentor explained:
*“We don’t want to read it as a loose string every time. Let’s create a **wrapper class**. That’s our `AppSettings.cs` inside the `Helpers` folder.”*

```csharp
namespace WebAPI.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; } = string.Empty;
    }
}
```

Now `AppSettings` becomes a **strongly-typed container** for that secret.


#### ⚙️ Step 3: Register AppSettings with Dependency Injection

In `Program.cs` (or `Startup.cs` if .NET 5), the mentor said:

```csharp
// Bind AppSettings section from appsettings.json
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));
```

That line means: *“Hey DI container, whenever someone asks for AppSettings, give them the values from appsettings.json.”*


#### 👨‍💻 Step 4: The User Service

Now comes the **UserService**.

The senior added `IUserService` interface:

```csharp
namespace WebAPI.Services
{
    public interface IUserService
    {
        string Authenticate(string username, string password);
    }
}
```

And its implementation:

```csharp
using Microsoft.Extensions.Options;
using WebAPI.Helpers;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string Authenticate(string username, string password)
        {
            // use _appSettings.Secret here to generate JWT
            return "token-based-on-secret";
        }
    }
}
```

Notice how **`IOptions<AppSettings>`** injects the secret into the service.


#### 🎮 Step 5: The UsersController

Finally, the **UsersController** was born:

```csharp
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginRequest request)
        {
            var token = _userService.Authenticate(request.Username, request.Password);
            if (token == null) return Unauthorized();
            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
```


#### 🧩 Step 6: Wire Up Services

In `Program.cs` again:

```csharp
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
```

And at the bottom:

```csharp
app.MapControllers();
```


### 🌟 Mentor’s Closing Note

What started as *“Where is my secret key?”* slowly became a **systematic flow**:

* `appsettings.json` → `AppSettings.cs` (strongly typed)
* Dependency Injection → `UserService`
* `UsersController` → `Authenticate` endpoint

That’s the **teamwork journey** from raw config to a functioning Web API with JWT authentication.



### 📖 From ASP.NET Core 1.0 to JWT Authentication in Web API

“Team, let me tell you a story. Imagine you are a **web application chef**. You started cooking in ASP.NET Framework 4.x—plenty of tools, lots of recipes, but heavy pans and slow stoves.

Then **ASP.NET Core 1.0** came. Lighter pans, cross-platform stoves. Then Core 2.0, 2.1, 3.1, and finally Core 5.0 came along. Each version was a **gift from the platform makers**:

* Faster execution.
* Loosely coupled pipelines.
* Lightweight, reusable code.
* Better performance under millions of requests.

Remember, these changes are **not random**. Microsoft designed them so your application can handle 3 million hits without crashing your server.


### 🔹 Step 1: MVC & Web API Pipeline

In ASP.NET Core, every request flows through a **pipeline**. Imagine it like a train track:

1. **Incoming HTTP request**
2. **Middleware passes request** (authentication, logging, routing)
3. **Controller executes action**
4. **Response sent back to client**

This is why performance improved from Core 2.0 → Core 5.0. You now have **optimized routing, lightweight controllers, and minimal overhead**.

### 🔹 Step 2: Controller & Action Method

Now, let’s bring it back to our **UsersController**.

We already created:

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
}
```

#### 🔹 Step 3: Authenticate Action

Remember, the mentor always says: *“Keep your controllers clean. Let the service do the work.”*

Here’s the **Authenticate POST method**:

```csharp
[HttpPost("authenticate")]
public IActionResult Authenticate([FromBody] AuthenticateRequest model)
{
    var response = _userService.Authenticate(model.Username, model.Password);

    if (response == null)
        return BadRequest(new { message = "Username or password is incorrect" });

    return Ok(response);
}
```

* `[HttpPost("authenticate")]` → maps the request to `api/users/authenticate`.
* `AuthenticateRequest` → contains username and password.
* `_userService.Authenticate()` → handles validation & JWT generation.
* Returns `BadRequest()` if invalid, `Ok()` if valid.


### 🔹 Step 4: GetAll Users Action

For testing and learning purposes, we can also **list all users**:

```csharp
[HttpGet("all")]
public IActionResult GetAll()
{
    var users = _userService.GetAll();
    return Ok(users);
}
```

* `[HttpGet("all")]` → maps to `api/users/all`.
* Useful for debugging, verifying authentication logic.


### 🔹 Step 5: Dependency Injection & Service Registration

As the mentor emphasized, **never create service objects manually in controllers**. Instead, register in `Program.cs`:

```csharp
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
```

This ensures:

* Controllers remain **lightweight**.
* Services can be **mocked for testing**.
* Cleaner, maintainable architecture.


### 🔹 Mentor Note: Version Changes Are Your Friend

The journey from **ASP.NET Framework → Core 1.0 → Core 2.0 → 3.1 → 5.0** is like upgrading your kitchen:

* Faster pipelines → milliseconds matter.
* Lightweight services → less RAM & CPU.
* Loosely coupled architecture → easier to maintain & test.
* Modern Web API patterns → ready for JWT, OAuth, microservices.

Never get frustrated by version changes—they **enable flexibility and better performance**.



✅ **Summary Flow for Students:**

1. Configure `appsettings.json` → store JWT secret.
2. Create `AppSettings` wrapper → strongly typed.
3. Implement `UserService` → authenticate users & generate JWT.
4. Inject `UserService` into `UsersController`.
5. Controller actions: `Authenticate` (POST) & `GetAll` (GET).
6. Register services in DI container (`Program.cs`).
7. Version upgrades = **optimized pipeline, better performance, cleaner architecture**.




### 📖 From Service Configuration to JWT Authorization

“Team, let me tell you a story. Think of your Web API as a **secure castle**. The castle has gates, guards, and secret keys. Now, we are going to learn how to **configure services, add authorization filters, and secure the castle using JWT middleware**.”


#### 🔹 Step 1: Configure Services in DI Container

First, the mentor reminded the team:

*"Remember, every service your controllers need must be registered in the DI container. There are different lifetimes: `AddSingleton`, `AddScoped`, `AddTransient`. Choose wisely."*

```csharp
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorizationFilter, AuthorizeAttribute>();
```

* `Scoped` → same service instance per HTTP request.
* `Singleton` → same instance across all requests.
* `Transient` → new instance each time requested.

> Best practice: UserService should be **scoped**, so each request handles authentication independently.


#### 🔹 Step 2: Create Custom Authorization Attribute

*"Not every request is trusted. We need a guard at the gate."*

* Create `AuthorizeAttribute.cs` under `Helpers` folder.
* Inherit from `Attribute` and implement `IAuthorizationFilter`.

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Helpers
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
```

* The filter checks if `HttpContext.Items["User"]` exists.
* If not, it blocks the request with a **401 Unauthorized**.
* Can be applied at **controller or action method level**:

```csharp
[Authorize]
public IActionResult GetAllUsers()
{
    // Only authorized users can access
}
```


#### 🔹 Step 3: JWT Middleware

*"Now, who provides the secret key? Who validates it? That’s the JWT middleware."*

* Create `JWTMiddleware.cs` in `Helpers`.
* Purpose: inspect **incoming request headers**, validate JWT, and attach user info to `HttpContext`.

```csharp
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using WebAPI.Helpers;

namespace WebAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // attach user to context
                context.Items["User"] = userService.GetById(userId);
            }
            catch
            {
                // do nothing if JWT validation fails
                // user is not attached to context so request won't be authorized
            }
        }
    }
}
```

* This middleware **validates the JWT**, extracts user info, and attaches it to `HttpContext.Items["User"]`.
* Any action with `[Authorize]` will check this user object.

---

#### 🔹 Step 4: Register Middleware in Startup/Program

Finally, the mentor emphasized: *“The middleware must be part of the request pipeline, and ordering matters!”*

```csharp
app.UseRouting();

app.UseMiddleware<JwtMiddleware>(); // JWT validation before controllers
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
```


#### 🔹 Mentor’s Life Lesson

*"Students, this is more than code. This is **facing errors, debugging, learning, and iterating**. Real learning happens when you implement, break, and fix things. Don’t fear errors—they are your stepping stones."*

* Encountered errors? Investigate, understand, and fix.
* If stuck, ask your mentor. Then try again.
* This journey—**services → custom filter → middleware → pipeline**—is what builds **real-world ASP.NET Core applications**.


✅ **Summary Flow**:

1. Register services with proper lifetime (`AddScoped`, etc.)
2. Create `AuthorizeAttribute` → filter to guard endpoints
3. Create `JWTMiddleware` → validate token and attach user
4. Register middleware in pipeline (`UseMiddleware<JwtMiddleware>()`)
5. Controller actions → decorate with `[Authorize]` for security
