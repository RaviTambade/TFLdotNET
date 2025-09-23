
# Securing ASP.NET Web API Using JWT — A Festival Wristband Story

> “Students, let me take you to a music festival... 🎶🎉”

Imagine you're at a giant music festival. As you walk in, the staff gives you a special **wristband** — colorful, coded, and uniquely yours. No matter how many stages you visit or food stalls you explore, you don’t need to show your ticket again. Just flash your wristband — it tells everyone, “Yes, this person is authorized.”

In the world of web apps, **JWT tokens** are just like that wristband.



## 🎫 What is JWT (JSON Web Token)?

When users log in, they get a **digital wristband** — a JWT — that proves who they are. It's compact, secure, and easy to carry (over HTTP headers). No need to check the login list each time. That would be like going back to the ticket counter every time you want to change the stage — **too slow and messy**.

### 📦 JWT Structure – Just Like a Sealed Parcel:

JWT has 3 parts — all packed in one string separated by dots:

1. **Header**: What kind of token is this? What algorithm?
2. **Payload**: The info (claims) — like your user ID, email, role, etc.
3. **Signature**: Sealed and signed to ensure nobody tampers with it.

---

## 🧠 Why JWT Authentication?

Let’s understand why JWT is *the real deal* in modern APIs:

- ✅ **Stateless**: No session storage required. Token contains all info.
- ✅ **Cross-Platform**: Use it with web, mobile, desktop — anything!
- ✅ **Scalable**: Great for microservices; tokens travel with the request.

## 🛠️ Step-by-Step Implementation in ASP.NET Core Web API

Let me guide you like a mentor in a hands-on lab:

### 🧪 Step 1: Create Your Web API Project

```bash
dotnet new webapi -n SecureWebApp
```

Let’s start with a simple controller to test the API:

```csharp
[Route("api/[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("Hello World");
}
```

Test it in Postman — ✅ "Hello World".

### 📦 Step 2: Add NuGet Packages

Install these two packages:

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
```
### 🔑 Step 3: Configure JWT in `appsettings.json`

```json
"Jwt": {
  "Key": "TFLNonDisclouserKey",
  "Issuer": "transflower.in"
}
```

### ⚙️ Step 4: Configure JWT in `Program.cs`

```csharp
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
```

And don’t forget:

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

### 🔐 Step 5: Create Login Endpoint to Issue JWT

```csharp
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _config;

    public LoginController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost]
    public IActionResult Post([FromBody] LoginRequest loginRequest)
    {
        // Normally you'd check username/password from DB
        if (loginRequest.Username == "admin" && loginRequest.Password == "123")
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        return Unauthorized();
    }
}
```

Create a simple request model:

```csharp
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
```

### 🔒 Step 6: Secure API with `[Authorize]`

Now decorate your API controller:

```csharp
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("Hello from secured API");
}
```

Now try calling it without a token — ❌ 401 Unauthorized.

Call `/api/login` to get a token, add it in the request header like:

```
Authorization: Bearer <your_token_here>
```

- ✅ Now you get your “Hello from secured API”.


##  Mentor’s Advice to You

> “Security is **not an afterthought**. It’s the foundation of trust between your application and its users. JWT is not just a token — it’s a promise, digitally signed, that says ‘You can trust me’.”

🧠 Don’t just implement JWT — understand why it matters.

📘 Want to build a full project with login, role-based access, and refresh tokens? Let’s do it together.

Just say **"Let's go further"** — and I’ll mentor you step by step.
