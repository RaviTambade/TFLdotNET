# Cookie-Based Authentication in ASP.NET Core MVC

*"Let’s take a short journey, friends… into how your favorite websites remember you."*

Imagine this: You walk into a library. The librarian asks for your ID. You show it, and she gives you a badge. For the rest of your time there, you just flash that badge to enter different sections. No more questioning, no more explanations.

That badge? It’s like a **cookie** — and this simple but powerful idea forms the backbone of **cookie-based authentication** in web applications.


## 🍪 What Is Cookie-Based Authentication?

In ASP.NET Core MVC, when a user logs in, the server issues a **cookie** that acts like an identity badge. This cookie is securely stored in the user’s browser and is automatically sent with every future request. That’s how the application "remembers" who you are — just like our librarian remembers you with your badge.


## 🧭 Let’s Understand How This Works – Step-by-Step

### 🧍 1. **User Login**

The user types their **username and password**. If correct, we say, “Welcome!” and issue an **authentication cookie** filled with claims like their username, role, etc.

### 📦 2. **Cookie Storage**

This cookie gets stored in the user's browser — encrypted and signed to prevent tampering. It can last a few minutes or several days based on how you set it up.

### 🔁 3. **Subsequent Requests**

On every new request, the browser automatically sends the cookie back. The server reads it and instantly knows who you are.

### ❌ 4. **User Logout**

When a user logs out, we destroy that cookie — just like returning the badge at the library exit.

## 🧩 Core Concepts You Must Know

### 🔐 **Authentication Cookie**

This is the “identity badge” we give to the user, holding key data like name, roles, or even permissions.

### 📜 **Claims-Based Identity**

Instead of just knowing your name, the server knows your **claims** — like “isAdmin: true”, “email: [user@example.com](mailto:user@example.com)”.

### ⏳ **Cookie Expiration + Sliding Expiration**

You can define how long the cookie lives. With **sliding expiration**, it gets extended if the user keeps using the app — like keeping the library badge as long as you stay active.

### 🔒 **Secure and HttpOnly Cookies**

Mark your cookies as `Secure` (only over HTTPS) and `HttpOnly` (JavaScript can’t access them) — this prevents snooping and scripting attacks.

## 🛠️ Let’s Build It – Step by Step

### 🧪 Step 1: Configure Authentication in `Program.cs`

```csharp
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Optional
    });
```

Add this to the pipeline:

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

### 🧑‍💻 Step 2: Create the `AccountController`

Let’s simulate a user store and handle login and logout.

```csharp
[HttpPost]
public async Task<IActionResult> Login(string username, string password)
{
    if (_users.ContainsKey(username) && _users[username] == password)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        return RedirectToAction("Index", "Home");
    }

    ModelState.AddModelError("", "Invalid username or password");
    return View();
}
```

And the logout:

```csharp
[HttpPost]
public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction("Index", "Home");
}
```

### 🧾 Step 3: Design the Login View (`Login.cshtml`)

Just a clean, simple form:

```html
<form asp-action="Login" method="post">
    <label>Username</label>
    <input type="text" name="username" required />
    
    <label>Password</label>
    <input type="password" name="password" required />
    
    <button type="submit">Login</button>
</form>
```

Show errors:

```html
@if (!ViewData.ModelState.IsValid)
{
    <div style="color:red">
        @foreach (var error in ViewData.ModelState.Values.SelectMany(v => v.Errors))
        {
            <p>@error.ErrorMessage</p>
        }
    </div>
}
```

### 🔐 Step 4: Protect Pages with `[Authorize]`

```csharp
[Authorize]
public IActionResult Index()
{
    return View(); // This can be viewed only by logged-in users
}
```

## ✅ Why Cookie Authentication Is Loved in MVC

* 🧠 **Simple & Intuitive**: Ideal for traditional, server-rendered applications.
* 🔒 **Secure by Design**: Can enforce HTTPS, prevent JavaScript access.
* 🛠️ **Built-in Support**: No need for external libraries or tools.
* ⏳ **Sliding Expiration**: Keeps active users logged in.


## 🚦 But Wait! Is This Always the Best Option?

> "Sir, can we use this for APIs too?"

Good question. While cookies are great for browser-based apps, APIs often use **JWT tokens** because they’re stateless and mobile-friendly. If you’re building SPAs or mobile backends, consider JWT instead.


## 🏁 Final Words from Your Mentor

Cookie-based authentication is like giving your user a **passport** to your application. It helps them move freely without checking their ID again and again. But it’s your job as the developer to make sure that passport is **valid**, **secure**, and **expires on time**.

🎯 Learn it. Build it. Protect it.

