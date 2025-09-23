
### 🔐 Making the App Secure

So, how do we secure our modern apps?
We apply **Security Policies**:

* Authentication (Verify the user’s identity).
* Authorization (Grant specific roles and permissions).

For example:

* A **student** logs in and sees only their marks.
* A **teacher** logs in and sees marks of all students.
* An **admin** logs in and manages courses, teachers, and students.

Same application, but behavior changes by role.

### 🧪 Demo Flow (JWT Authentication Example)

Imagine we run a simple ASP.NET Core application with JWT authentication.

* Step 1: A user hits **/login** with username/password.
* Step 2: Server checks credentials and issues a **JWT token** (like a digital ID card).
* Step 3: For every subsequent request, the client attaches the token in the header.
* Step 4: The server verifies the token before giving access to APIs.

This ensures:
✅ Only valid users can access.
✅ Only allowed roles can perform certain operations.
✅ APIs remain safe from anonymous misuse.


### 🎯 Mentor’s Takeaway

So next time you design a web application, don’t stop at MVC or database CRUD.
Ask yourself:

* Does my app **remember the user**? (Session Management)
* Does my app **protect the user’s data**? (Authentication & Authorization)
* Does my app **adapt to the user’s preferences**? (Personalization/Profile Management)
* Does my app **support different cultures and languages**? (Localization/Globalization)

That’s when your app grows from *just working* → to a **mature, professional-grade web application**.



👨‍🏫 **“See, our application is now running on port 4000.”**
That’s like saying *our bank branch has opened for business*.

* When you access it in the **browser** (`http://localhost:4000/users`) — you get **Unauthorized**.
* Why? Because the browser is like a random customer walking in **without showing an ID card**. The bank won’t hand over money just because someone asked.

But when you access it using **Postman**, and include the right **authorization token** in the request header → suddenly you’re allowed in.
Now the bank staff recognizes you and says, *“Yes, this person is valid. Please give them the data.”*



### 🏦 The Bank Analogy

Imagine you walk into a bank to withdraw cash:

1. You fill in a **withdrawal slip** (like your login credentials: username + password).
2. You hand it to the cashier with your passbook (server checks your credentials).
3. The bank doesn’t immediately give you money. Instead, they issue you a **token** — a small numbered disc.

   * This token is proof that you’ve already been verified.
   * Now you can sit and wait until your turn comes.
4. When your number is called, you show the token → and get your money.

🔑 The important part:
That **token** is valid only for a certain period of time. If you lose it, or try to use it after the branch is closed, it’s useless.



### 🌐 How this maps to Web Applications

* **Login request** → You give credentials.
* **Server validates** → Issues a **JWT token** (the bank token).
* **Token is stored on the client side** (browser, Postman, mobile app).
* **Subsequent requests** → You don’t send your username/password again.

  * Instead, you attach the **token** in the request header:

    ```
    Authorization: Bearer <your-token-value>
    ```
* Server checks the token, verifies it, and if it’s valid → grants access.

If the token is missing or invalid → the server responds **401 Unauthorized**.



### 🧪 Why did Browser Fail but Postman Worked?

* In the browser, you just typed the URL. No headers were included.
* Postman allowed you to **add headers** → including the `Authorization: Bearer <token>`.
* That’s why Postman succeeded where the browser failed.

This teaches us:
👉 **Secure APIs require more than just a URL. They require valid authorization tokens.**



### 🎯 Mentor’s Key Takeaway

Students, remember:

* A **URL** is like the *door to a bank*.
* A **token** is like your *token slip* from the cashier.
* Without the token, the bank won’t recognize you.
* With the token, you can complete your work smoothly.

This is why we say:

> “In modern web applications, tokens are the currency of trust between the client and the server.”



### 🔐 What Happened in the Story?

1. **Bank Waiting Hall (Browser without token)**

   * You typed `http://localhost:4000/users`.
   * Server said **“Unauthorized”** because you came *without identity proof*.

2. **Submitting Slip to Bank Staff (Authenticate API)**

   * You sent a **POST request** to `/authenticate`.
   * Inside the **request body**, you included:

     ```json
     {
       "username": "ankur",
       "password": "password123"
     }
     ```
   * Server validated the credentials and issued a **token** (like the bank staff giving you a small plastic token disc).

3. **Watching the LED Screen (Token in Hand)**

   * Now you hold the token.
   * Whenever your number comes, the cashier doesn’t recheck your documents — the **token itself is enough**.

4. **Going to Cashier (Authorized API Call)**

   * You copied the token.
   * In Postman, you added it to the **Authorization header**:

     ```
     Authorization: Bearer <your-token-here>
     ```
   * Now when you request `/users`, the server checks the token and says ✅ “Yes, valid. Here’s your data.”

5. **Security Guarantee**

   * Without a token → **401 Unauthorized**.
   * With a valid token → **Access Granted**.
   * Even if 500 people request data, no one can misuse it, because each one must present their own valid token.


### 💡 The Big Insight

Just like in a bank:

* The **manager** checks your documents once (username/password).
* Then gives you a **token**.
* The **cashier** never checks your documents again — only your token.
* Whoever holds the token can access the service → that’s why JWT must be **securely stored** by the client.


### 🧩 Mapping to Web Application

* **Authenticate API** → Login desk (validate credentials).
* **JWT Token** → Bank token disc.
* **Authorization header** → Showing the token to cashier.
* **Users API** → Secure counter, only accessible with a valid token.


⚡ So the answer to your question:
👉 **Yes, exactly the same thing happens in your web application.**
The token system ensures **secure, efficient, and scalable authentication** — without rechecking credentials every time.



## 🔑 Big Picture: JWT Authentication Flow

1. **Client → Authenticate API**
   Sends `username + password`.
2. **Server → Token Issued**
   Middleware + Service validate credentials → generate JWT token.
3. **Client → Secure API Call**
   Attaches `Authorization: Bearer <token>` in request header.
4. **Server → Middleware Validates Token**
   If valid → Controller Action executes.
   If invalid/expired → Respond with **401 Unauthorized**.



## 🏗️ What We Need in ASP.NET Core

### 1. **Model**

Represents user data.

```csharp
public class User
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Username  { get; set; }
    public string Password  { get; set; }  // (in real apps, use hashed passwords)
}
```


### 2. **Service (Business Logic)**

Define contract + implementation for user authentication.

```csharp
public interface IUserService
{
    User Authenticate(string username, string password);
    IEnumerable<User> GetUsers();
}
```

```csharp
public class UserService : IUserService
{
    private readonly List<User> _users = new List<User>
    {
        new User { FirstName="Ankur", LastName="Prasad", Username="ankur", Password="12345" }
    };

    public User Authenticate(string username, string password)
    {
        return _users.SingleOrDefault(x => x.Username == username && x.Password == password);
    }

    public IEnumerable<User> GetUsers() => _users;
}
```


### 3. **Controller**

Exposes APIs:

* `/authenticate` → Public (login).
* `/users` → Protected (only with valid token).

```csharp
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtUtils _jwtUtils;  // helper for token creation

    public UsersController(IUserService userService, IJwtUtils jwtUtils)
    {
        _userService = userService;
        _jwtUtils = jwtUtils;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] LoginRequest model)
    {
        var user = _userService.Authenticate(model.Username, model.Password);
        if (user == null) return Unauthorized(new { message = "Invalid credentials" });

        var token = _jwtUtils.GenerateToken(user);
        return Ok(new { user.FirstName, user.LastName, user.Username, Token = token });
    }

    [Authorize]
    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        return Ok(_userService.GetUsers());
    }
}
```


### 4. **Middleware / JWT Helper**

This part intercepts every request *before* the controller is hit.

```csharp
public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateToken(token);

        if (userId != null)
        {
            // attach user to context
            context.Items["User"] = userService.GetUsers().FirstOrDefault(x => x.Username == userId);
        }

        await _next(context);
    }
}
```


### 5. **Program.cs Setup**

Register dependencies and middleware.

```csharp
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

var app = builder.Build();

// custom middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();
app.Run();
```


## 📦 Dependencies Needed

* `Microsoft.AspNetCore.Authentication.JwtBearer`
* `System.IdentityModel.Tokens.Jwt`

## 🔐 Token Types Recap

* **JWT (JSON Web Token)** → Used for most web apps, APIs, SPAs, mobile apps.
* **OAuth (Open Authorization)** → Enterprise, delegated access (e.g., Google, Facebook login, banking apps).



✅ With this structure in mind, you can now:

* Write your **User model**,
* Define **IUserService + UserService**,
* Create **UsersController** with `Authenticate` and `GetUsers`,
* Add **JWT helper + middleware**,
* Plug everything into **Program.cs**.
 


## 🏁 Step-by-Step Roadmap to Implement JWT Authentication in ASP.NET Core Web API

### **Step 1: Create a New Project**

```bash
dotnet new webapi -n JwtAuthDemo
cd JwtAuthDemo
```

👉 This scaffolds a simple ASP.NET Core Web API project with a default `Controllers` folder and `Program.cs`.



### **Step 2: Add Dependencies**

Edit your **.csproj** file and add JWT-related packages:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="7.0.0" />
  <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.0.0" />
</ItemGroup>
```

Then run:

```bash
dotnet restore
```


### **Step 3: Create Folder Structure**

* **Models** → `User.cs`, `AuthenticateRequest.cs`, `AuthenticateResponse.cs`
* **Services** → `IUserService.cs`, `UserService.cs`
* **Helpers** → `JwtUtils.cs` (helper for token creation/validation), `AppSettings.cs`
* **Controllers** → `UsersController.cs`



### **Step 4: Define Models**

**User.cs**

```csharp
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }  // ⚠️ For demo only. Use hashed password in real apps.
}
```

**AuthenticateRequest.cs**

```csharp
public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
```

**AuthenticateResponse.cs**

```csharp
public class AuthenticateResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }

    public AuthenticateResponse(User user, string token)
    {
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Token = token;
    }
}
```


### **Step 5: Define Service Layer**

**IUserService.cs**

```csharp
public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(int id);
}
```

**UserService.cs**

```csharp
public class UserService : IUserService
{
    private List<User> _users = new()
    {
        new User { Id=1, FirstName="Ankur", LastName="Prasad", Username="ankur", Password="12345" }
    };

    private readonly IJwtUtils _jwtUtils;

    public UserService(IJwtUtils jwtUtils)
    {
        _jwtUtils = jwtUtils;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
        if (user == null) return null;

        var token = _jwtUtils.GenerateToken(user);
        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll() => _users;

    public User GetById(int id) => _users.FirstOrDefault(x => x.Id == id);
}
```


### **Step 6: Implement JWT Helper**

**JwtUtils.cs**

```csharp
public interface IJwtUtils
{
    string GenerateToken(User user);
    int? ValidateToken(string token);
}

public class JwtUtils : IJwtUtils
{
    private readonly IConfiguration _config;

    public JwtUtils(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                        SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateToken(string token)
    {
        if (token == null) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);

        try
        {
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

            return userId;
        }
        catch
        {
            return null;
        }
    }
}
```

### **Step 7: Configure appsettings.json**

```json
"Jwt": {
  "Secret": "THIS IS MY VERY SECURE SECRET KEY"
}
```


### **Step 8: Register Services in Program.cs**

```csharp
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
```

And configure authentication middleware:

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
```


### **Step 9: Create Controller**

**UsersController.cs**

```csharp
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);
        if (response == null)
            return Unauthorized(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAll());
    }
}
```

## 🚀 Testing Flow

1. Run the app → `dotnet run`.
2. In Postman:

   * Call **POST /users/authenticate** with body:

     ```json
     { "username": "ankur", "password": "12345" }
     ```

     → Get back JWT token.
   * Call **GET /users** with `Authorization: Bearer <token>` header.
     → See protected data.


## 🔑 Step-by-Step JWT Authentication Flow in ASP.NET Core

### 1. **AppSettings.json → Secret Key**

* In `appsettings.json`, you keep your **Secret** (like the bank’s stamp/seal).

  ```json
  {
    "AppSettings": {
      "Secret": "THIS_IS_MY_SUPER_SECRET_KEY"
    }
  }
  ```
* This **Secret** will be used to generate the **JWT signature**.

👉 Analogy: This is like the **bank manager’s seal** — without it, no token is valid.


### 2. **AppSettings Class**

* A **C# class** is created to map the values from `appsettings.json`.

  ```csharp
  public class AppSettings
  {
      public string Secret { get; set; }
  }
  ```
* Registered in **Startup.cs → ConfigureServices** using:

  ```csharp
  services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
  ```

👉 Analogy: This class is the **official register** that stores the secret so other components can read it.


### 3. **UserService (Business Logic)**

* Depends on `AppSettings` to fetch the secret.
* Validates user credentials and, if valid, generates a **JWT token**.
* Uses:

  * `SymmetricSecurityKey`
  * `SigningCredentials`
  * `JwtSecurityTokenHandler`

👉 Analogy: **Bank Staff** → You show your credentials (username/password). If correct, they **generate a token** using the bank’s seal (secret).


### 4. **UsersController**

* Two endpoints:

  1. **POST /authenticate** → Public, takes username/password, calls `UserService.Authenticate()`, returns token.
  2. **GET /users** → Protected, requires `[Authorize]`, only works if a valid token is in the request header.

👉 Analogy:

* **Authenticate** → Login desk → “Show me ID, I’ll give you a token.”
* **Get Users** → Cashier counter → “Show me your token, I’ll serve you.”


### 5. **Authorize Attribute**

* `[Authorize]` is added on secure APIs.
* Ensures only **authenticated users with valid JWT** can access.

👉 Analogy: A **guard at the cashier** who only lets you through if you have a valid bank token.


### 6. **Middleware (JWT Interceptor)**

* Configured in the **HTTP pipeline** (`Startup.cs → Configure`).
* Middleware checks **every request**:

  1. Does it have an `Authorization` header?
  2. Does it start with `Bearer <token>`?
  3. If yes → validate token using secret.
  4. If valid → attach user info to `HttpContext`.
  5. If not → return `401 Unauthorized`.

👉 Analogy: Middleware = **gatekeeper before the cashier**.

* If token is missing/invalid → ❌ rejected at the gate.
* If valid → ✅ forwarded to controller.

### 7. **Token Expiration**

* Token has an expiry (e.g., 7 days).
* After expiry, client must re-authenticate.

👉 Analogy: **Bank tokens are valid only for that day’s working hours**. Tomorrow → you need a fresh token.


## 📌 Big Picture Flow

1. Client sends **/authenticate** with username/password.
2. `UserService` validates → generates **JWT token** using secret.
3. Token returned to client.
4. Client sends **/users** with header:

   ```
   Authorization: Bearer <jwt-token>
   ```
5. Middleware checks validity → if valid → controller executes → data returned.


⚡ **In short:**

* **appsettings.json → Secret key**
* **AppSettings class → Reads secret**
* **UserService → Generates JWT**
* **UsersController → Exposes APIs (Authenticate + Protected)**
* **Authorize attribute → Marks protected endpoints**
* **Middleware → Intercepts & validates every request before hitting controller**

When middleware intercepts a request and finds a token, it must **validate and attach the user to the context**. Let me frame this in a **mentor-style crisp story** for your students:



## 🔐 What Happens After Middleware Finds a Token?

### 1. Interceptor (Middleware) in Action

* Every incoming HTTP request goes through middleware.
* Middleware checks:

  * Does request have an **Authorization header**?
  * Does it start with `Bearer <token>`?
  * If not → return **401 Unauthorized**.

👉 Analogy: Gatekeeper at the bank entrance → “Show me your token.”


### 2. Validate the Token

* If a token exists, middleware uses **JWT SecurityTokenHandler** + **AppSettings.Secret** to validate:

  * Is the **signature** correct?
  * Is the **expiry time** still valid?
  * Does it contain the expected **claims** (like UserId, Username)?

👉 Analogy: The guard checks the token against the **bank’s master list** (secret stamp + expiry date).


### 3. Attach User to Context

* If token is valid, middleware extracts **claims** (like `UserId`, `Username`).
* Middleware attaches this data to **`HttpContext.User`**.
* From here, **controllers and services can know who the current user is** without re-verifying credentials.

👉 Analogy: Once the guard verifies your token, he **writes your name & ID in the visitor register**. Now every counter knows who you are — you don’t need to keep explaining.


### 4. If Invalid → Block Immediately

* If token fails validation (wrong signature, expired, tampered):

  * Middleware **stops the request right there**.
  * Returns **401 Unauthorized** to client.

👉 Analogy: If your bank token is fake or expired → gatekeeper won’t let you step inside.


## 📌 Why This Matters

* **Decoupling**: Controllers don’t worry about checking passwords or validating tokens.
* **Centralized security**: Middleware ensures *every request* is validated before reaching controllers.
* **Contextual access**: Once user is attached to context, any controller or service can read:

  ```csharp
  var userId = HttpContext.User.FindFirst("id")?.Value;
  ```

👉 This makes the app both **secure** and **scalable**.



⚡ Mentor’s Closing Line:
“Think of middleware as the **security desk at the bank’s entrance**.
It doesn’t let fake tokens enter the building. And if you’re valid, it registers you so everyone inside knows your identity.”

