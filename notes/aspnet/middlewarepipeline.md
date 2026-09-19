# ASP.NET Core Middleware Ordering

## The Hidden Bug in `Program.cs`

### 🎯 Learning Objective

By the end of this session, students should be able to:

* Understand the ASP.NET Core request pipeline.
* Explain middleware ordering.
* Differentiate Authentication and Authorization.
* Understand where CORS fits into the pipeline.
* Identify a middleware-ordering bug.
* Debug a broken `Program.cs`.
* Correct the pipeline based on request flow.


# 1. Ravi Sir Enters the Classroom

**Ravi Sir:**

> "Today I'm not going to give you the correct `Program.cs`."

Students look surprised.

**Student:**

> "Sir, then what are we going to do?"

**Ravi Sir:**

> "I'm going to give you a broken application."

**Student:**

> "But sir, if it's broken, won't it give compilation errors?"

**Ravi Sir:**

> "That's the interesting part."

He writes on the board:

```text
COMPILES
   ≠
CORRECT
```

Then:

> "The compiler checks whether your C# program is syntactically and semantically valid. It doesn't understand whether your HTTP request is travelling through the correct middleware pipeline."


# 2. The Real-World Scenario

Imagine we are building:

```text
TFL Insurance API
```

Our frontend is:

```text
React Application
```

Our backend is:

```text
ASP.NET Core Web API
```

Architecture:

```text
React
   │
   │ HTTP Request
   ▼
ASP.NET Core API
   │
   ├── CORS
   ├── Authentication
   ├── Authorization
   ├── Routing
   └── Controller
        │
        ▼
     Database
```

The API contains:

```text
GET    /api/policies
POST   /api/policies
GET    /api/claims
POST   /api/claims
```

Some endpoints require authentication.

# 3. The Broken `Program.cs`

Ravi Sir gives students this code:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("https://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer();

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.UseAuthorization();

app.UseAuthentication();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
```

Then Ravi Sir asks:

> "Find the bugs."


# 4. Student Investigation

### Student 1

> "Sir, the code compiles."

**Ravi Sir:**

> "Correct."

### Student 2

> "The application starts."

**Ravi Sir:**

> "Correct."

### Student 3

> "Then what is broken?"

**Ravi Sir:**

> "Excellent question. Let's send an HTTP request."


# 5. Request Journey

Suppose the browser sends:

```http
GET /api/claims
Authorization: Bearer eyJ...
```

The request enters the pipeline.

But look at our code:

```text
Authorization
      ↓
Authentication
      ↓
Routing
      ↓
CORS
      ↓
HTTPS
      ↓
Controller
```

Ravi Sir asks:

> "How can authorization decide whether the user is allowed to access something if authentication hasn't established who the user is?"

Student:

> "It can't, sir."

Exactly.


# 6. Bug #1 — Authorization Before Authentication

Broken:

```csharp
app.UseAuthorization();

app.UseAuthentication();
```

Correct:

```csharp
app.UseAuthentication();

app.UseAuthorization();
```

Classroom rule:

```text
Authentication
      ↓
Authorization
```

Or remember:

```text
WHO ARE YOU?
     ↓
WHAT CAN YOU DO?
```


# 7. Bug #2 — Routing Comes Too Late

Current code:

```csharp
app.UseAuthorization();
app.UseAuthentication();
app.UseRouting();
```

Routing identifies the endpoint that should handle the request.

Conceptually:

```text
Request
   │
   ▼
Routing
   │
   ▼
Which endpoint?
   │
   ▼
Authentication
   │
   ▼
Authorization
```

So we generally want routing established before middleware that relies on endpoint information.


# 8. Bug #3 — CORS Position

Current code:

```csharp
app.UseRouting();

app.UseCors("AllowFrontend");
```

This is a reasonable position for conventional endpoint routing.

The important idea is:

```text
Routing
   ↓
CORS
   ↓
Authentication
   ↓
Authorization
   ↓
Endpoint
```

Why?

Because browsers may send a CORS preflight request:

```http
OPTIONS /api/claims
```

The server needs to process CORS appropriately and return the required headers.


# 9. Bug #4 — HTTPS Redirection

Current code:

```csharp
app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();
```

Think about what HTTPS redirection means.

The client says:

```text
HTTP
 │
 ▼
ASP.NET Core
 │
 ▼
"Please use HTTPS."
```

It is generally better to perform HTTPS redirection early:

```csharp
app.UseHttpsRedirection();
```

before normal application processing.


# 10. Debugging Exercise

Ravi Sir now gives the students the following task.

### 🧪 Exercise

Fix this pipeline:

```csharp
app.UseAuthorization();

app.UseAuthentication();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.MapControllers();
```

### Student Task

Rearrange the middleware.

Students discuss.

Possible answer:

```csharp
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
```

# 11. But Ravi Sir Isn't Finished

He asks:

> "Where is exception handling?"

Student:

> "At the top, sir."

Ravi Sir:

> "Why?"

Student:

> "So that exceptions thrown by middleware later in the pipeline can be handled."

Exactly.

For production environments:

```csharp
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}
```

should be placed early enough to cover the downstream pipeline.


# 12. Corrected `Program.cs`

Now we produce the corrected version.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("https://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer();

builder.Services.AddAuthorization();

var app = builder.Build();


// Exception handling should be early
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}


// Redirect HTTP → HTTPS
app.UseHttpsRedirection();


// Serve static files if the application uses them
app.UseStaticFiles();


// Establish endpoint routing
app.UseRouting();


// Process CORS
app.UseCors("AllowFrontend");


// Establish user identity
app.UseAuthentication();


// Check permissions
app.UseAuthorization();


// Execute endpoint/controller
app.MapControllers();

app.Run();
```

# 13. Draw the Pipeline

Ravi Sir draws this on the board:

```text
                 HTTP REQUEST
                       │
                       ▼
             ┌──────────────────┐
             │ Exception Handler│
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ HTTPS Redirect   │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Static Files     │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Routing          │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ CORS             │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Authentication   │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Authorization    │
             └────────┬─────────┘
                      │
                      ▼
             ┌──────────────────┐
             │ Controller       │
             │ / Endpoint       │
             └────────┬─────────┘
                      │
                      ▼
                  RESPONSE
```

# 14. Student Question: Why Can't the Compiler Find This?

**Student:**

> "Sir, why doesn't Visual Studio show an error?"

Ravi Sir:

> "Because every individual statement is valid C#."

For example:

```csharp
app.UseAuthentication();
```

is valid.

And:

```csharp
app.UseAuthorization();
```

is valid.

But:

```csharp
app.UseAuthorization();
app.UseAuthentication();
```

may be **architecturally wrong** for the intended authentication/authorization flow even though both statements compile.

This is the difference between:

```text
Syntax correctness
        vs
Runtime behavior
        vs
Architectural correctness
```

# 15. Student Question: Is Middleware Like a Stack?

**Student:**

> "Sir, is middleware like a stack?"

Ravi Sir:

> "Good observation."

Conceptually, middleware behaves like nested request/response processing.

For example:

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("A - Before");

    await next();

    Console.WriteLine("A - After");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("B - Before");

    await next();

    Console.WriteLine("B - After");
});
```

Request flow:

```text
A Before
   ↓
B Before
   ↓
Endpoint
   ↓
B After
   ↓
A After
```

So middleware isn't simply a list of configuration statements.

It creates a **request/response execution pipeline**.

# 16. Hands-On Debugging Challenge

Now students receive a slightly more difficult example:

```csharp
app.UseAuthorization();

app.UseCors("AllowFrontend");

app.UseExceptionHandler("/error");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();
```

### Your task

Identify the problems.

Students should identify:

```text
❌ Authorization before Authentication

❌ Exception handling is too late

❌ Routing is after several middleware components that may depend on it

❌ CORS is not in the intended position

❌ HTTPS redirection should generally happen earlier
```

Then rearrange them.

Expected conceptual ordering:

```text
Exception Handling
       ↓
HTTPS Redirection
       ↓
Static Files
       ↓
Routing
       ↓
CORS
       ↓
Authentication
       ↓
Authorization
       ↓
Endpoint
```

# 17. Important Nuance for Students

Ravi Sir adds an important warning:

> "Don't memorize one universal `Program.cs`."

Different applications have different middleware.

For example:

```text
Static Files?
    Maybe.

CORS?
    If cross-origin access is required.

Authentication?
    If authentication is used.

Authorization?
    If authorization is used.

Session?
    If session is used.

Rate Limiting?
    If rate limiting is configured.

Swagger?
    Often development/API documentation.

SignalR?
    If hubs are used.
```

Therefore:

> **Don't memorize the order. Understand the dependency between middleware components.**

# 18. The Golden Questions

Whenever you see `Program.cs`, ask:

### Question 1

**Does this middleware need routing information?**

If yes, routing must already have established the endpoint context.

### Question 2

**Does this middleware need the authenticated user?**

If yes, authentication must happen before it.

### Question 3

**Does this middleware depend on authorization?**

Authorization must happen after authentication.

### Question 4

**Can this middleware throw an exception that should be globally handled?**

Then exception handling should be positioned to cover it.

### Question 5

**Is the browser making a cross-origin request?**

Then understand where CORS processing occurs and how preflight requests are handled.

# 19. Final Student Challenge

Ravi Sir writes:

```text
Client
  │
  ▼
   ?
  │
  ▼
Routing
  │
  ▼
   ?
  │
  ▼
Authentication
  │
  ▼
   ?
  │
  ▼
Controller
```

He asks:

> "Fill the missing boxes."

Students answer:

```text
Exception Handling
       ↓
Routing
       ↓
CORS
       ↓
Authentication
       ↓
Authorization
       ↓
Controller
```

Ravi Sir:

> "Excellent."

Then he writes the final principle:

```text
Compiler checks:
    "Is the code valid?"

Runtime checks:
    "Does the code behave correctly?"

Architect thinks:
    "Does the request flow make sense?"
```

# 🎯 Takeaway

**Middleware ordering is application architecture.**

A request doesn't magically jump from:

```text
Browser → Controller
```

It travels through a pipeline:

```text
Client
  ↓
Middleware
  ↓
Middleware
  ↓
Middleware
  ↓
Endpoint
  ↓
Response
```

A small change in that pipeline can change application behavior.

So when debugging ASP.NET Core:

> **Don't only debug the controller. Debug the road that takes the request to the controller.**

**The compiler can verify your code.
The mentor teaches you to understand the flow.**