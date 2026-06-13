## JWT Role based Authentication: Security Guard at a Corporate Building

Imagine you are building an Insurance Management System.

The company has different people:

* Customer
* Sales Executive
* Accounts Officer
* Manager
* Administrator

Not everyone should access everything.

For example:

| Role             | Allowed Actions   |
| ---------------- | ----------------- |
| Customer         | View own policies |
| Sales Executive  | Create policies   |
| Accounts Officer | Process payments  |
| Manager          | View reports      |
| Administrator    | Full access       |

How do we enforce this?

This is where **Role-Based JWT Authentication** comes into the picture.

# Step 1: User Tries to Login

Suppose a manager logs in.

```http
POST /api/auth/login
```

Request:

```json
{
   "username":"ravi",
   "password":"password123"
}
```

The Authentication Service verifies:

```csharp
if(userName=="ravi" && password=="password123")
{
    //valid user
}
```

In a real application, this data comes from a database.

# Step 2: System Identifies User Role

The database returns:

```json
{
   "id":101,
   "name":"Ravi",
   "role":"Manager"
}
```

Now the server knows:

```text
User = Ravi
Role = Manager
```


# Step 3: Create JWT Token

The server generates a token containing claims.

Claims are pieces of information stored inside JWT.

Example Claims:

```csharp
new Claim(ClaimTypes.Name,"Ravi")
new Claim(ClaimTypes.Role,"Manager")
```

Generated JWT:

```json
{
  "sub":"101",
  "name":"Ravi",
  "role":"Manager",
  "exp":1750000000
}
```

Actual JWT looks like:

```text
eyJhbGciOiJIUzI1NiIs...
```

The token is digitally signed by the server.


# Step 4: Send Token to Client

Response:

```json
{
   "token":"eyJhbGciOiJIUzI1NiIs..."
}
```

Client stores token.

```javascript
localStorage.setItem("token",token);
```


# Step 5: Client Calls Protected API

Example:

```http
GET /api/reports
```

Header:

```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

The token travels with every request.


# Step 6: JWT Middleware Validates Token

ASP.NET Core JWT Middleware performs:

### Check 1

Is token present?

```text
Yes
```

### Check 2

Is signature valid?

```text
Yes
```

### Check 3

Is token expired?

```text
No
```

### Check 4

Extract claims

```text
Name = Ravi
Role = Manager
```

User becomes authenticated.

# Step 7: Authorization Happens

Suppose controller:

```csharp
[Authorize(Roles="Manager")]
[HttpGet]
public IActionResult GetReports()
{
    return Ok("Confidential Reports");
}
```

Framework checks:

```text
Does token contain Role=Manager ?
```

Result:

```text
YES
```

Access Granted.

# Another Scenario

Customer token:

```json
{
   "name":"Amit",
   "role":"Customer"
}
```

Customer calls:

```http
GET /api/reports
```

Controller:

```csharp
[Authorize(Roles="Manager")]
```

Framework checks:

```text
Customer == Manager ?
```

Result:

```text
NO
```

Response:

```http
403 Forbidden
```

Authentication succeeded.

Authorization failed.


# Authentication vs Authorization

Students often confuse these concepts.

## Authentication

Question:

```text
Who are you?
```

Example:

```text
Username + Password
```

Result:

```text
You are Ravi
```

## Authorization

Question:

```text
What are you allowed to do?
```

Result:

```text
You are Manager
You can view reports
```

Authentication happens first.

Authorization happens second.

# Role-Based Authorization Example

```csharp
[Authorize(Roles="Admin")]
```

Only Admin can access.


```csharp
[Authorize(Roles="Manager,Admin")]
```

Manager OR Admin can access.



```csharp
[Authorize]
```

Any authenticated user can access.


```csharp
[AllowAnonymous]
```

Anyone can access.

No token required.


# Typical Claims Stored in JWT

```csharp
new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString())
new Claim(ClaimTypes.Name,user.Name)
new Claim(ClaimTypes.Email,user.Email)
new Claim(ClaimTypes.Role,user.Role)
```

JWT Payload:

```json
{
   "sub":"101",
   "name":"Ravi",
   "email":"ravi@test.com",
   "role":"Manager"
}
```

# Real World ASP.NET Core Flow

```text
User Login
    ↓
Verify Credentials
    ↓
Identify Role
    ↓
Generate JWT
    ↓
Return Token
    ↓
Client Stores Token
    ↓
API Request with Token
    ↓
JWT Middleware Validates
    ↓
Claims Extracted
    ↓
Authorize Attribute Checks Role
    ↓
Access Granted / Forbidden
```

## Role based authentication Tutorial

### 1. Create the project
- Create a new ASP.NET Core Web API project.
- Name it `SecureRolesWebApp` or similar.

### 2. Add configuration class
Create AppSettings.cs:

```csharp
namespace SecureWebApp.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
```

### 3. Define role constants
Create Role.cs:

```csharp
namespace SecureRolesWebApp.Entities
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
```

### 4. Create user model
Create User.cs:

```csharp
using System.Text.Json.Serialization;

namespace SecureWebApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }
    }
}
```

### 5. Create auth request/response models
Create AuthenticateRequest.cs:

```csharp
using System.ComponentModel.DataAnnotations;

namespace SecureWebApp.Models
{
    public class AuthenticateRequest
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}
```

Create AuthenticateResponse.cs:

```csharp
using SecureWebApp.Entities;

namespace SecureWebApp.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }
    }
}
```

### 6. Add service interface
Create IUserService.cs:

```csharp
using SecureWebApp.Entities;
using SecureWebApp.Models;
using System.Collections.Generic;

namespace SecureWebApp.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
```

### 7. Implement user service with JWT
Create UserService.cs:

- Hardcode users
- Generate JWT with `id` and `role` claims
- Use `AppSettings.Secret`

Example:
```csharp
public class UserService : IUserService
{
    private readonly AppSettings _appSettings;
    private List<User> _users = new()
    {
        new User { Id = 1, Username = "swarali", Password = "swarali", Role = Role.Admin, FirstName="Swarali", LastName="L" },
        new User { Id = 2, Username = "ganesh", Password = "ganesh", Role = Role.User, FirstName="Ganesh", LastName="S" }
    };

    public UserService(IOptions<AppSettings> appSettings) => _appSettings = appSettings.Value;

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
        if (user == null) return null;
        var token = generateJwtToken(user);
        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll() => _users;
    public User GetById(int id) => _users.FirstOrDefault(x => x.Id == id);

    private string generateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
```

### 8. Add JWT middleware
Create JwtMiddleware.cs:

```csharp
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
        if (token != null) attachUserToContext(context, userService, token);
        await _next(context);
    }

    private void attachUserToContext(HttpContext context, IUserService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            var role = jwtToken.Claims.First(x => x.Type == "role").Value;
            context.Items["User"] = userService.GetById(userId);
            context.Items["Role"] = role;
        }
        catch
        {
            // ignore invalid token
        }
    }
}
```

### 9. Create custom authorize attribute
Create `Filters/AuthorizeAttribute.cs`:

```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public string Role { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];
        var roleToken = (string)context.HttpContext.Items["Role"];

        if (user == null || Role != roleToken)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" })
            { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
```

### 10. Configure the app
Update Program.cs:

```csharp
builder.Services.AddControllers();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

### 11. Add secret to configuration
In appsettings.json:

```json
{
  "AppSettings": {
    "Secret": "YOUR_SECRET_KEY_HERE"
  }
}
```

### 12. Create controller with protected endpoint
Create or update UsersController.cs:

```csharp
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);
        if (response == null) return BadRequest(new { message = "Username or password is incorrect" });
        return Ok(response);
    }

    [Authorize(Role = Role.Admin)]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAll());
    }
}
```

### 13. Test it
1. Call `POST /users/authenticate`
   - Body: `{ "username": "swarali", "password": "swarali" }`
2. Copy the returned `token`
3. Call `GET /users`
   - Header: `Authorization: Bearer <token>`
4. Only users with `Role.Admin` can access `GET /users`

---

## Summary
- Use `JwtMiddleware` to validate tokens and set `HttpContext.Items`
- Use `[Authorize(Role = ...)]` for role-based access
- Use `IUserService` and `UserService` to manage users and issue JWTs


# Mentor Takeaway for Students

When building enterprise applications, think of JWT as a **Digital Identity Card**.

* Authentication = Issuing the Identity Card
* JWT = Digital Identity Card
* Claims = Information printed on the card
* Role = Department/Designation on the card
* Authorization = Security guard checking whether that designation allows entry into a particular room

This is why modern systems built with ASP.NET Core, Spring Boot, and Node.js commonly use JWT-based role authentication to secure APIs while keeping them stateless and scalable.
