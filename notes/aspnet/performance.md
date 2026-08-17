# “Performance Is Not a Feature — It Is a Foundation”

I would tell my ASP.NET Core learners: “Students, today I don't want to teach you another ASP.NET Core feature. I want to teach you a mindset.”**
 
> **Performance.**
 
> Because an application that works for 10 users is easy to build.  An application that continues to work when **10,000 or 1,00,000 users arrive** — that is engineering.

 

## 👨‍🏫 First, Let's Change the Question

A beginner asks:  “Sir, does my API work?”

A professional asks: “How fast does it respond?”

An experienced developer asks: “How does it behave when 10,000 users call it simultaneously?”

And an architect asks: **“How will it behave tomorrow when our traffic becomes 10 times larger?”**

That is the mindset I want you to develop.


# 🎯 Scenario: Our Insurance API

Imagine we have built:

```text
Angular / React
       ↓
ASP.NET Core Web API
       ↓
    Service
       ↓
 Repository
       ↓
    MySQL
```

Our endpoint is:

```http
GET /api/policies
```

It works.

Response time:

```text
50 ms
```

Wonderful!

But now imagine:

```text
10 users       → 50 ms
1,000 users    → 100 ms
10,000 users   → 2 seconds
50,000 users   → 10 seconds
```

Eventually:

```text
Request
   ↓
Timeout
   ↓
User frustrated
   ↓
Business frustrated
```

So I ask the classroom: **“Where did we go wrong?”**

The answer is important: We didn't design with **performance and concurrency** in mind.



# 1️⃣ Async/Await — Don't Block the Server

Suppose you write:

```csharp
var policies = service.GetPoliciesAsync().Result;
```

or:

```csharp
service.GetPoliciesAsync().Wait();
```

I ask:

> **“Why are we making an asynchronous operation synchronous?”**

Instead:

```csharp
var policies = await service.GetPoliciesAsync();
```

And don't stop there.

Think **end-to-end**:

```text
Controller
    ↓ async
Service
    ↓ async
Repository
    ↓ async
EF Core
    ↓ async
Database
```

### 🧠 Mentor lesson

`async/await` isn't about making one request magically faster. It helps the server **avoid unnecessarily blocking threads while waiting for I/O**, allowing better concurrency.

> **Don't just write async in the controller. Design the entire I/O path asynchronously.**

 
# 2️⃣ Caching — “Why Are We Asking the Database the Same Question 10,000 Times?”

Imagine this endpoint:

```http
GET /api/products
```

The product catalog changes once every hour.

But 20,000 users request it.

Are we going to ask the database 20,000 times?

```text
User 1 ──┐
User 2 ──┤
User 3 ──┤
User 4 ──┼──→ Database
...      │
User 20k ┘
```

I ask: **“If the answer hasn't changed, why calculate it again?”**

Introduce caching.

### Single-instance application

```text
IMemoryCache
```

### Multi-instance application

```text
IDistributedCache
       ↓
     Redis
```

And HTTP-level caching can also be useful for suitable responses. Architecture becomes:

```text
             ┌── Cache → Response
Request → API
             └── Database
```

### Mentor question

> **“What data in your application changes rarely but is requested frequently?”**

That's where you should start thinking about caching.
 

# 3️⃣ Database — The API May Not Be Slow. Your Query May Be Slow.

This is one of the most important lessons. A student says: “Sir, ASP.NET Core is slow.”

I ask:

> **“Show me your SQL.”**

😄

Your API might be doing:

```text
API
 ↓
Service
 ↓
EF Core
 ↓
Bad SQL
 ↓
Database scans 2 million rows
```

### Read-only queries

Consider:

```csharp
context.Products
       .AsNoTracking()
```

when tracking isn't required. Why? Because EF Core doesn't need to maintain change-tracking information for entities you aren't going to modify.

 

## 🚨 Watch for N+1 Queries

Imagine:

```csharp
foreach (var customer in customers)
{
    // Load policies
}
```

You might accidentally produce:

```text
1 query → Customers

+ 100 queries → Policies
```

That's the famous:

> **N+1 problem**

Now investigate whether appropriate eager loading, projection, or query redesign can solve it.

Also consider:

```text
Indexes
Proper projections
Pagination
Compiled queries where justified
Raw SQL / Dapper for specific scenarios
```

### Mentor message

> **“Before optimizing C# code, inspect the database query.”**

Many performance problems live below the controller.

# 4️⃣ Compression — “Why Are We Sending So Much Data?”

Imagine your API returns:

```json
{
   "id": 1,
   "name": "...",
   "description": "...",
   ...
}
```

Thousands of records can create a large response. Ask:  **“Do we really need to send all those bytes over the network?”** Response compression can reduce payload size. ASP.NET Core supports:

```text
Brotli
Gzip
```

But remember: Compression itself consumes CPU. So performance engineering is always about **trade-offs**. Don't memorize:  “Brotli is better.”

Ask:  **“For this workload, is the CPU cost worth the network reduction?”**

 

# 5️⃣ Middleware — “Every Request Goes Through Here”

Look at your ASP.NET Core pipeline:

```text
Request
  ↓
Middleware 1
  ↓
Middleware 2
  ↓
Middleware 3
  ↓
Middleware 4
  ↓
Controller
```

Now imagine you have 20 middleware components. I ask:  **“Does every request really need every piece of work?”**

- Keep your pipeline intentional. 
- Avoid expensive work that isn't needed.
- Use short-circuiting where appropriate.
- And don't misunderstand this:  **Don't remove useful middleware simply because "fewer middleware = faster."**
- Measure first.

 

# 6️⃣ Output Caching — “Why Execute the Same Endpoint Again?”

Suppose:

```http
GET /api/products
```

returns the same data for many users.

Instead of:

```text
Request
 ↓
Controller
 ↓
Service
 ↓
Database
 ↓
Response
```

we can sometimes have:

```text
Request
 ↓
Output Cache
 ↓
Response
```

For appropriate endpoints, ASP.NET Core's **Output Caching** can significantly reduce repeated server-side work.

### Mentor question

> **“Which APIs in your project produce the same response repeatedly?”**

That's the question an engineer asks.

 

# 7️⃣ Minimal APIs — Don't Use a Hammer for Every Nail

ASP.NET Core gives us different programming models. For some very high-throughput, simple endpoints, Minimal APIs can be appropriate.

```csharp
app.MapGet("/products", ...);
```

But don't tell students:

> “Minimal APIs are always faster, therefore MVC is bad.”

That's not engineering. 

The right question is:

> **“Does this endpoint actually benefit from a simpler request pipeline, and have we measured it?”**
 

# 8️⃣ Memory — “Where Did All That RAM Go?”

Now imagine your API receives thousands of requests.

Every request creates objects.

```text
Request
 ↓
Objects
 ↓
Strings
 ↓
Collections
 ↓
JSON
 ↓
Garbage Collector
```

Too many unnecessary allocations can create:

```text
More allocations
     ↓
More GC work
     ↓
CPU pressure
     ↓
Latency
```

Tools such as:

* BenchmarkDotNet
* dotMemory
* profilers

can help you investigate.

Advanced techniques such as:

```text
Span<T>
ArrayPool<T>
StringBuilder
```

can be useful in appropriate hot paths.

But here's my warning:

> **Don't use Span<T> just because you learned Span<T>.**

First find the bottleneck.

 

# 9️⃣ Kestrel & ThreadPool — “The Server Has Limits Too”

Your ASP.NET Core application eventually runs on a server.

That server has:

```text
CPU
Memory
Threads
Connections
Network
```

You should understand how Kestrel and the .NET ThreadPool behave. Also understand modern protocols:

```text
HTTP/1.1
HTTP/2
HTTP/3
```

But don't randomly tune:

```csharp
ThreadPool.SetMinThreads(...)
```

because someone on the internet recommended it.

### Mentor rule:

> **Configuration tuning without measurement is guessing.**

Understand the default behavior first. Change settings only when evidence tells you there is a problem.

 

# 🔟 Logging — “I Want to Know Everything!”

A student says:  “Sir, I'll log everything.” I say: **“Excellent. Now imagine 50,000 requests per second.”**

Suddenly:

```text
Request
 ↓
Logs
 ↓
Logs
 ↓
Logs
 ↓
Logs
 ↓
Storage
 ↓
Cost
```

Logging is valuable. But excessive logging can create:

* CPU overhead
* I/O overhead
* storage consumption
* higher observability costs

Use:

### Structured logging

```text
PolicyId = 1234
CustomerId = 5678
Operation = PurchasePolicy
Duration = 83ms
```

And use appropriate telemetry sampling for high-volume systems.

 

# 🧰 Bonus: IHttpClientFactory

Never casually create HTTP clients everywhere:

```csharp
new HttpClient()
```

For production applications, understand and use:

```text
IHttpClientFactory
```

It helps manage HTTP client lifetimes and integrates well with resilient HTTP communication patterns.

 

# 🏗️ Now Let's Put Everything Together

Our production architecture might evolve like this:

```text
                    USERS
                      ↓
              API Management
                      ↓
              ASP.NET Core API
                      ↓
        ┌─────────────┼─────────────┐
        ↓             ↓             ↓
      Cache         Service       Logging
        ↓             ↓             ↓
      Redis        Repository   App Insights
                      ↓
                   Database
```

And around it:

```text
              ┌── Autoscaling
              ├── Monitoring
              ├── Authentication
              ├── Caching
              ├── Compression
              ├── CI/CD
              └── Resilience
```

Now you are no longer thinking only:  **“How do I write this controller?”**

You're thinking: **“How will this system behave under load?”**

# 🧪 Mentor Challenge: Let's Test Your Application

I would give students this assignment:

### Scenario

> **“Your insurance API currently handles 100 requests/sec. Tomorrow the business expects 10,000 requests/sec.”**

Don't immediately start coding. Create a performance investigation:

```text
1. Where is the bottleneck?
        ↓
2. Is it CPU?
        ↓
3. Is it memory?
        ↓
4. Is it database?
        ↓
5. Is it network?
        ↓
6. Is it locking?
        ↓
7. Is it external API latency?
        ↓
8. Are we doing unnecessary work?
        ↓
9. Can we cache?
        ↓
10. Can we scale horizontally?
```

Then measure. Then optimize. Then measure again.

 

# 🔥 The Golden Rule of Performance

I want every ASP.NET Core learner to remember these five words:  **Measure → Identify → Optimize → Test → Measure Again**

Not: 

> “I heard Redis is faster.”

Not:

> “Minimal APIs are faster.”

Not:

> “Dapper is faster.”

Not:

> “Async is faster.”

Those statements without context are incomplete.

Instead ask:

> **“What is the bottleneck in MY application?”**

 

# 🌱 From Developer to Engineer

A beginner thinks:

```text
Feature → Code → Done
```

A professional thinks:

```text
Feature
   ↓
Correctness
   ↓
Performance
   ↓
Security
   ↓
Scalability
   ↓
Observability
   ↓
Reliability
   ↓
Cost
```

That's the transformation I want to create in the classroom.

> **Students, performance is not something you add at the end of the project.**
>
> You don't first build a slow application and then say:
>
> *“Now let's make it fast.”*
>
> From the beginning, learn to ask:
>
> **How much data?
> How many users?
> How many requests?
> How much latency?
> How much memory?
> How many database calls?
> What happens under load?**
>
> **Performance isn't a feature.
> Performance is a foundation.**
>
> And when you start asking these questions naturally, you are no longer learning only **ASP.NET Core**.
>
> **You are learning to engineer software.** 
