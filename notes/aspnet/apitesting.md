# 🧪 Testing an ASP.NET Core Web API

> **Mentor says:**
> "Writing an API is like building a car.
> Testing is not checking whether the car exists.
> Testing is checking whether the car behaves correctly under different situations."

Imagine we have built an **Insurance Web API**.

```text
                 ASP.NET Core Web API
                         │
          ┌──────────────┼──────────────┐
          │              │              │
     Controllers      Services      Repository
          │              │              │
          └──────────────┼──────────────┘
                         │
                      Database
```

Suppose we have:

```http
POST /api/policies
GET  /api/policies/101
PUT  /api/policies/101
DELETE /api/policies/101
```

Now the question is:

> **How do we know this API really works?**

This is where different levels of testing come into the picture.

## 1. Unit Testing — "Test the Worker"

### Mentor Story

Imagine a factory. There is a worker whose only responsibility is:

> "Calculate insurance premium."

You don't need to start the entire factory to test this worker. You simply give:

```text
Age = 30
Policy Amount = ₹10,00,000
Term = 20 years
```

and check whether the worker calculates the expected premium. That is **Unit Testing**.

### Definition

> **Unit testing tests one small unit of application logic in isolation.**

Usually the unit is:

* a method
* a class
* a service
* a business rule

For example:

```csharp
public class PremiumCalculator
{
    public decimal Calculate(decimal amount, int age)
    {
        if (age < 30)
            return amount * 0.02m;

        return amount * 0.03m;
    }
}
```

We can test it without:

* Web server
* HTTP
* Database
* Controller
* Network

### Example

```csharp
[Fact]
public void Calculate_ShouldReturnCorrectPremium()
{
    // Arrange
    var calculator = new PremiumCalculator();

    // Act
    var result = calculator.Calculate(1000000, 30);

    // Assert
    Assert.Equal(30000, result);
}
```

The famous testing pattern is:

```text
AAA

Arrange
   ↓
Act
   ↓
Assert
```

### Mentor's Question

> "Why don't we test the database here?"

Because we are testing **PremiumCalculator**, not the database.

That is the beauty of unit testing.

```text
             UNIT TEST
                 │
                 ▼
        ┌─────────────────┐
        │ PremiumCalculator│
        └─────────────────┘
                 │
                 ▼
             Expected
              Result
```

## 2. Unit Testing a Service

Suppose our API has:

```text
PoliciesController
        │
        ▼
PolicyService
        │
        ▼
PolicyRepository
```

We want to test:

```csharp
PolicyService.PurchasePolicy()
```

But the service depends on the repository.

So we can replace the real repository with a **mock**.

```text
                 Unit Test
                     │
                     ▼
              PolicyService
                     │
                  Mock
                     │
                     ▼
             Fake Repository
```

For example, using Moq:

```csharp
var repository = new Mock<IPolicyRepository>();

repository
    .Setup(r => r.GetById(101))
    .Returns(new Policy
    {
        Id = 101,
        Amount = 1000000
    });
```

Then:

```csharp
var service = new PolicyService(repository.Object);

var result = service.PurchasePolicy(101);
```

And finally:

```csharp
Assert.NotNull(result);
```

### Mentor's rule

> **Unit Test = isolate the unit.**

If your test requires:

```text
SQL Server
MongoDB
RabbitMQ
HTTP
File System
External API
```

you are probably moving away from a pure unit test.

## 3. Integration Testing — "Test the Team"

Now imagine the same factory. The premium worker works perfectly. But what happens when:

```text
Controller
      ↓
Service
      ↓
Repository
      ↓
Database
```

work together?

That's where **Integration Testing** comes in.

> **Integration testing verifies that multiple components work correctly together.**

For example:

```text
HTTP Request
     │
     ▼
Controller
     │
     ▼
Service
     │
     ▼
Repository
     │
     ▼
Database
```

Now we are no longer testing one class. We are testing **integration between components**.

## 4. Example Integration Test

Suppose we have:

```http
GET /api/policies/101
```

The integration test can send an actual HTTP request to the application.

```csharp
var client = factory.CreateClient();

var response =
    await client.GetAsync("/api/policies/101");
```

Then:

```csharp
response.EnsureSuccessStatusCode();
```

And:

```csharp
var policy =
    await response.Content
                  .ReadFromJsonAsync<Policy>();

Assert.Equal(101, policy.Id);
```

Now something interesting happened. We didn't directly call:

```csharp
PoliciesController.GetById()
```

Instead we called:

```http
GET /api/policies/101
```

The request went through the application pipeline.

```text
Test
 │
 │ HTTP GET
 ▼
ASP.NET Core
 │
 ▼
Middleware
 │
 ▼
Routing
 │
 ▼
Controller
 │
 ▼
Service
 │
 ▼
Repository
 │
 ▼
Database
```

That is much closer to real application behavior.


## 5. Integration Testing with WebApplicationFactory

ASP.NET Core provides:

```csharp
WebApplicationFactory<TEntryPoint>
```

This allows us to start the application in a test environment.

Conceptually:

```text
Integration Test
       │
       ▼
WebApplicationFactory
       │
       ▼
ASP.NET Core Application
       │
       ├── Middleware
       ├── Routing
       ├── Controllers
       ├── Services
       └── Repositories
```

A typical test might look like:

```csharp
public class PolicyApiTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PolicyApiTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPolicy_ShouldReturn200()
    {
        var response =
            await _client.GetAsync("/api/policies/101");

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }
}
```

Now we are testing the **real API pipeline**.

## 6. Unit vs Integration

This is an important interview question.

| Unit Testing              | Integration Testing           |
| ------------------------- | ----------------------------- |
| Tests one unit            | Tests multiple components     |
| Usually isolated          | Components work together      |
| Dependencies mocked       | Real dependencies may be used |
| Very fast                 | Slower                        |
| No real database normally | Database may be involved      |
| Tests business logic      | Tests integration             |
| `PolicyService`           | API + Service + Repository    |

Think:

```text
UNIT

     🧑
     │
     ▼
  ONE WORKER
```

versus:

```text
INTEGRATION

🧑 → 🧑 → 🧑 → 🗄️
 │     │     │
Team works together
```

## 7. End-to-End Testing — "Test the Customer Journey"

Now let's go one level higher. Imagine a customer purchasing an insurance policy. The customer doesn't care about:

```text
Controller
Service
Repository
Entity Framework
Dependency Injection
```

The customer cares about:

> "Can I successfully purchase my policy?"

This is **End-to-End Testing**.

## 8. Real Customer Journey

Imagine this flow:

```text
Customer
   │
   ▼
Login
   │
   ▼
Receive JWT Token
   │
   ▼
Browse Policies
   │
   ▼
Select Policy
   │
   ▼
Purchase Policy
   │
   ▼
Make Payment
   │
   ▼
Policy Activated
```

An E2E test validates this **complete business journey**.


## 9. E2E Test Example

Imagine our frontend is:

```text
Angular / React
       │
       ▼
ASP.NET Core Web API
       │
       ▼
SQL Server
```

The E2E test behaves like a real user.

```text
Open Application
       ↓
Enter username
       ↓
Enter password
       ↓
Click Login
       ↓
Select Policy
       ↓
Click Purchase
       ↓
Make Payment
       ↓
Verify Policy Status
```

Tools commonly used for browser-based E2E testing include:

* Playwright
* Selenium
* Cypress

For example, conceptually:

```text
Playwright
    │
    ▼
Browser
    │
    ▼
React / Angular UI
    │
    ▼
ASP.NET Core API
    │
    ▼
Database
```

The test is asking:

> **"Can a real user successfully complete the business operation?"**

## 10. Three Levels — One Picture

This is the picture I would draw on the classroom board:

```text
                    END-TO-END TEST
                 "Can the customer
                  complete the journey?"
                         │
                         ▼
              ┌─────────────────────┐
              │     Frontend        │
              │  React / Angular    │
              └──────────┬──────────┘
                         │
                         ▼
              ┌─────────────────────┐
              │   ASP.NET Core API  │
              └──────────┬──────────┘
                         │
              ┌──────────┴──────────┐
              ▼                     ▼
         Controller              Service
                                  │
                                  ▼
                             Repository
                                  │
                                  ▼
                              Database


              INTEGRATION TEST
          "Do these components work
               together?"
                         │
                         ▼
              API → Service → DB


                 UNIT TEST
            "Does this worker
               work correctly?"
                         │
                         ▼
                    Service
                       OR
                   Calculator
```
 
## 11. Testing Pyramid

Now tell the students:

> "Don't write 1,000 E2E tests for everything."

Why? Because E2E tests are generally:

* slower
* more expensive to maintain
* more dependent on infrastructure

Instead, we follow the **Testing Pyramid**.

```text
                    /\
                   /  \
                  / E2E\
                 /------\
                /        \
               /Integrate \
              /------------\
             /              \
            /   Unit Tests   \
           /__________________\
```

A healthy application usually has:

```text
        Few
        E2E Tests
           ▲
           │
      More Integration
           ▲
           │
       Many Unit Tests
```

 

## 12. What Should We Test at Each Level?

Imagine our API has:

```csharp
POST /api/policies/purchase
```

### Unit Test

Test:

```text
Premium calculation
Eligibility rules
Policy validation
Business rules
```

Example:

```text
Age < 18
   ↓
Purchase rejected
```

### Integration Test

Test:

```text
HTTP Request
      ↓
Controller
      ↓
Service
      ↓
Repository
      ↓
Database
```

Example:

```http
POST /api/policies/purchase
```

Verify:

```http
HTTP 201 Created
```

and database contains the purchased policy.

 

### E2E Test

Test:

```text
Login
  ↓
Browse policy
  ↓
Select policy
  ↓
Purchase
  ↓
Payment
  ↓
Confirmation
```

The question is:

> **"Can the customer complete the entire journey?"**

 
## 13. Mentor's Simple Analogy

I would explain it to students using a **restaurant**.

### Unit Test

Test the chef.

```text
Chef
 ↓
Can he cook the dish correctly?
```

### Integration Test

Test the restaurant.

```text
Waiter
   ↓
Chef
   ↓
Kitchen
   ↓
Billing
```

Do all departments work together?

### End-to-End Test

Test the customer experience.

```text
Customer
   ↓
Enter restaurant
   ↓
Order food
   ↓
Food arrives
   ↓
Eat
   ↓
Pay bill
   ↓
Leave
```

The customer doesn't care whether the chef uses:

```text
Gas stove
Electric stove
Induction
```

The customer asks:

> **"Did I get what I ordered?"**

That's E2E testing.


## 14. ASP.NET Core Testing Strategy

For a real project, I would teach students this structure:

```text
Solution
│
├── src
│   ├── API
│   ├── Application
│   ├── Domain
│   └── Infrastructure
│
└── tests
    │
    ├── UnitTests
    │   ├── Services
    │   ├── Validators
    │   └── BusinessRules
    │
    ├── IntegrationTests
    │   ├── Controllers
    │   ├── API Pipeline
    │   └── Database
    │
    └── E2ETests
        ├── Login
        ├── PurchasePolicy
        ├── Payment
        └── Renewal
```

## 15. Mentor's Final Mantra

Write this on the board:

```text
UNIT
"Is my logic correct?"
        ↓
INTEGRATION
"Do my components work together?"
        ↓

END-TO-END
"Does the complete business journey work?"
```

Or even simpler:

```text
           CODE
            │
            ▼
       ┌─────────┐
       │  UNIT   │
       └────┬────┘
            │
     "Does it work?"
            │
            ▼
    ┌────────────────┐
    │  INTEGRATION   │
    └───────┬────────┘
            │
     "Do they work?"
            │
            ▼
      ┌───────────┐
      │   E2E     │
      └─────┬─────┘
            │
      "Does the
    customer succeed?"
```

### The Transflower Mentor way

> **Unit testing builds confidence in the developer.**
> **Integration testing builds confidence in the application.**
> **End-to-end testing builds confidence in the business.**

And a professional ASP.NET Core developer should understand **all three**, not merely know how to write a controller.