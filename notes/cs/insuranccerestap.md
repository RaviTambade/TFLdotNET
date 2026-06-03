# ASP.NET Core Minimal REST API Assignment

## Insurance Policy Management System

### Problem Statement

You are working as a Software Engineer in an insurance company that provides various insurance policies to customers. The company wants to modernize its system by exposing business operations through REST APIs so that web applications, mobile applications, and external partners can interact with the system.

The system should be developed using **ASP.NET Core Minimal APIs** and should demonstrate how **Events and Event Handlers** can be used to notify multiple departments whenever important business activities occur.

Instead of tightly coupling departments with business operations, implement the **Publisher-Subscriber (Event-Driven) Architecture** using C# Events.


# Business Scenario

Whenever a policy-related operation is performed, different departments should be automatically notified.

| Business Operation | Event Raised    | Departments Notified             |
| ------------------ | --------------- | -------------------------------- |
| Purchase Policy    | PolicyPurchased | Sales Team, Notification Service |
| Pay Premium        | PremiumPaid     | Accounts Team, SMS Service       |
| Register Claim     | ClaimRegistered | Claim Department, Surveyor Team  |
| Renew Policy       | PolicyRenewed   | Renewal Team, Email Service      |



# Technology Requirements

Develop the solution using:

* ASP.NET Core 9/10 Minimal API
* C#
* Dependency Injection
* Events and Delegates
* RESTful API Design
* Swagger/OpenAPI
* In-Memory Data Storage (List<T>)



# Functional Requirements

## 1. Purchase Insurance Policy

### API Endpoint

```http
POST /api/policies/purchase
```

### Request

```json
{
  "policyNumber": "POL1001",
  "customerName": "Ravi Tambade",
  "policyType": "Health",
  "premiumAmount": 5000
}
```

### Business Logic

* Create policy record
* Store in memory
* Raise PolicyPurchased event

### Subscribers

* Sales Department
* Customer Notification Service

### Expected Response

```json
{
  "message": "Policy Purchased Successfully"
}
```

### Console Logs

```text
[Sales Team]
New policy sold.

[Notification Service]
Welcome email sent to customer.
```


## 2. Pay Premium

### API Endpoint

```http
POST /api/policies/paypremium
```

### Request

```json
{
  "policyNumber": "POL1001",
  "amount": 5000
}
```

### Business Logic

* Update premium payment status
* Raise PremiumPaid event

### Subscribers

* Accounts Department
* SMS Notification Service

### Expected Response

```json
{
  "message": "Premium Received"
}
```

### Console Logs

```text
[Accounts Department]
Payment recorded successfully.

[SMS Service]
Premium payment confirmation sent.
```



## 3. Register Claim

### API Endpoint

```http
POST /api/policies/registerclaim
```

### Request

```json
{
  "policyNumber": "POL1001",
  "claimAmount": 25000,
  "reason": "Accident"
}
```

### Business Logic

* Create claim record
* Raise ClaimRegistered event

### Subscribers

* Claim Department
* Surveyor Team

### Expected Response

```json
{
  "message": "Claim Registered"
}
```

### Console Logs

```text
[Claim Department]
Claim case created.

[Surveyor Team]
Survey request initiated.
```

## 4. Renew Policy

### API Endpoint

```http
POST /api/policies/renew
```

### Request

```json
{
  "policyNumber": "POL1001"
}
```

### Business Logic

* Extend policy validity
* Raise PolicyRenewed event

### Subscribers

* Renewal Department
* Email Notification Service

### Expected Response

```json
{
  "message": "Policy Renewed Successfully"
}
```

### Console Logs

```text
[Renewal Team]
Policy renewal processed.

[Email Service]
Renewal confirmation email sent.
```


## 5. View All Policies

### API Endpoint

```http
GET /api/policies
```

### Expected Response

```json
[
  {
    "policyNumber": "POL1001",
    "customerName": "Ravi Tambade",
    "policyType": "Health",
    "premiumAmount": 5000
  }
]
```


# Suggested Project Structure

```text
InsuranceApi
│
├── Models
│   ├── Policy.cs
│   ├── Claim.cs
│
├── Events
│   ├── PolicyEventArgs.cs
│
├── Services
│   ├── InsurancePolicyManager.cs
│
├── Subscribers
│   ├── SalesDepartment.cs
│   ├── AccountsDepartment.cs
│   ├── ClaimDepartment.cs
│   ├── RenewalDepartment.cs
│   ├── NotificationService.cs
│   ├── SmsService.cs
│
├── Program.cs
│
└── appsettings.json
```


# Domain Model

```csharp
public class Policy
{
    public string PolicyNumber { get; set; }
    public string CustomerName { get; set; }
    public string PolicyType { get; set; }
    public decimal PremiumAmount { get; set; }
}
```

# Event Publisher

```csharp
public class InsurancePolicyManager
{
    public event EventHandler<PolicyEventArgs> PolicyPurchased;
    public event EventHandler<PolicyEventArgs> PremiumPaid;
    public event EventHandler<PolicyEventArgs> ClaimRegistered;
    public event EventHandler<PolicyEventArgs> PolicyRenewed;
}
```


# Sample Minimal API Endpoints

```csharp
app.MapPost("/api/policies/purchase",
    (Policy policy, InsurancePolicyManager manager) =>
{
    manager.PurchasePolicy(policy);

    return Results.Ok(
        new
        {
            Message = "Policy Purchased Successfully"
        });
});
```


# Learning Objectives

After completing this assignment, students should understand:

### ASP.NET Core Minimal APIs

```csharp
app.MapGet(...)
app.MapPost(...)
app.MapPut(...)
app.MapDelete(...)
```

### Dependency Injection

```csharp
builder.Services.AddSingleton<
    InsurancePolicyManager>();
```

### C# Events

```csharp
public event EventHandler<PolicyEventArgs>
    PolicyPurchased;
```

### Event Subscription

```csharp
manager.PolicyPurchased +=
    salesDepartment.OnPolicyPurchased;
```

### Event Invocation

```csharp
PolicyPurchased?.Invoke(
    this,
    eventArgs);
```

### REST API Development

* GET
* POST
* PUT
* DELETE

### Swagger Testing

```csharp
app.UseSwagger();
app.UseSwaggerUI();
```

# Architecture Diagram

```text
                   REST API
                        |
                        v
           InsurancePolicyManager
                        |
          --------------------------------
          |       |       |       |
          v       v       v       v

   PolicyPurchased
   PremiumPaid
   ClaimRegistered
   PolicyRenewed

          |       |       |       |
          --------------------------------
          |       |       |       |
          v       v       v       v

      Sales Team
      Accounts Team
      Claim Department
      Renewal Team
      SMS Service
      Email Service
      Surveyor Team
      Notification Service
```

# Bonus Challenges

### Level 1

* Implement Custom EventArgs
* Pass Customer Name
* Pass Policy Number
* Pass Premium Amount

### Level 2

* Add Repository Pattern
* Store data using EF Core
* Use SQL Server database

### Level 3

* Implement Logging using ILogger

```csharp
logger.LogInformation(
    "Policy Purchased");
```

### Level 4

* Implement Background Services

```csharp
IHostedService
```

### Level 5

* Publish Events to RabbitMQ

```text
Policy Purchased
        |
        v
     RabbitMQ
        |
        v
 Notification Service
```

### Level 6 (Industry Ready)

Convert the solution into a Microservice Architecture:

* Policy Service
* Claim Service
* Payment Service
* Notification Service

Communicate using:

* REST APIs
* RabbitMQ
* Event-Driven Messaging


## Deliverables

Students should submit:

1. ASP.NET Core Minimal API Project
2. Swagger Screenshots
3. Event Demonstration Output
4. Source Code
5. README Documentation
6. API Testing Collection (Postman)

This assignment introduces students to real-world enterprise concepts such as **REST APIs, Minimal APIs, Dependency Injection, Event-Driven Architecture, Publisher-Subscriber Pattern, Notifications, Logging, and Microservice Communication**, which are widely used in modern insurance, banking, e-commerce, and cloud-native applications.
