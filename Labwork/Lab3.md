# 🧪 Hands-On Lab (Part 3)




## Adding Minimal APIs for Machine & Factory Data (with OpenAPI)

Below is a **clean continuation lab**, written in a **step-by-step, mentor-guided style**, extending your **existing ECommerce solution** and **WebApp** from the previous lab.

This lab introduces **Minimal APIs + OpenAPI (Swagger)** and explains **why each line exists**, so students don’t just copy code—they **understand system behavior**.

> **Continuation of previous labs**
> Existing projects in solution:
>
> * `Cataog` → Class Library (Domain)
> * `Test` → Console App
> * `WebApp` → Razor Pages Web Application


## 🎯 Lab Objective

Students will learn how to:

* Add **Minimal APIs** inside an ASP.NET Core Web App
* Expose **JSON data endpoints**
* Enable **OpenAPI / Swagger**
* Understand **backend APIs powering UI & machines**
* Prepare foundation for **IoT / Factory / Industry 4.0 apps**

## 🧠 Concept Context for Students

> Razor Pages = **Human UI**
> Minimal APIs = **Machine / System UI**

Both can live **inside the same Web Application**.

## 📁 Step 1: Open WebApp Project

Navigate to solution root:

```cmd
D:
cd tryout\SeniorsTrg\ECommerce
```

Open **WebApp** folder:

```cmd
cd WebApp
```

---

## 🧩 Step 2: Open `Program.cs`

Locate:

```
WebApp
 └── Program.cs
```

📌 This file is the **heart of ASP.NET Core**
Everything starts and flows from here.

---

## 🧱 Step 3: Understand the Minimal Hosting Model

Replace (or modify) `Program.cs` to the following structure:

```csharp
var builder = WebApplication.CreateBuilder(args);
```

📌 **Meaning:**
Creates the application builder
→ loads config
→ prepares services
→ sets up dependency injection

---

## 📘 Step 4: Enable OpenAPI (Swagger)

Add this line **below builder creation**:

```csharp
builder.Services.AddOpenApi();
```

📌 **Why?**

* Generates API documentation
* Allows testing APIs via browser
* Essential for real-world APIs

---

## 🚀 Step 5: Build the Web Application

```csharp
var app = builder.Build();
```

📌 **Meaning:**
Application pipeline is finalized here.

---

## 🏭 Step 6: Create Machine Data API

Add **below `builder.Build()`**:

```csharp
// API to get machine data
app.MapGet("/api/machinedata", () =>
{
    var machinedata = new[]
    {
        new { MachineId = 1, Status = "Running", Temperature = 75.5, LastMaintenance = DateTime.Now.AddDays(-10) },
        new { MachineId = 2, Status = "Stopped", Temperature = 0.0, LastMaintenance = DateTime.Now.AddDays(-30) },
        new { MachineId = 3, Status = "Running", Temperature = 80.0, LastMaintenance = DateTime.Now.AddDays(-5) }
    };

    return machinedata;
});
```

### 🧠 Teaching Explanation

| Element              | Meaning                   |
| -------------------- | ------------------------- |
| `MapGet`             | HTTP GET endpoint         |
| `/api/machinedata`   | REST URL                  |
| `return machinedata` | Auto-converted to JSON    |
| Anonymous objects    | DTO-like lightweight data |

---

## 🌡 Step 7: Create Factory Environment API

Add next API:

```csharp
app.MapGet("/api/factoryenvironment", () =>
{
    var factoryenvironment = new[]
    {
        new { FactoryId = 1, Environment = "Normal", Humidity = 50.0, Pressure = 1013.25 },
        new { FactoryId = 2, Environment = "Critical", Humidity = 80.0, Pressure = 980.0 },
        new { FactoryId = 3, Environment = "Normal", Humidity = 45.0, Pressure = 1015.0 }
    };

    return factoryenvironment;
});
```

📌 **Industry Mapping**

* Machine Data → Sensors
* Factory Environment → Monitoring System
* JSON → Universal communication format

---

## ▶ Step 8: Run the Application

Add final line:

```csharp
app.Run();
```

📌 **Why?**
Starts the web server and listens for requests.

---

## 🏗 Step 9: Build the Solution

Go back to solution root:

```cmd
cd ..
dotnet build
```

Expected:

```
WebApp succeeded
Build succeeded
```

---

## ▶ Step 10: Run the WebApp

```cmd
dotnet run --project .\WebApp\WebApp.csproj
```

---

## 🌐 Step 11: Test APIs in Browser

### 🔹 Machine Data API

```
https://localhost:xxxx/api/machinedata
```

### 🔹 Factory Environment API

```
https://localhost:xxxx/api/factoryenvironment
```

- ✔ JSON output appears
- ✔ No controller required
- ✔ No model required

---

## 📘 Step 12: Enable OpenAPI UI

Add **before `app.Run()`**:

```csharp
app.MapOpenApi();
```

Re-run app.

Open:

```
https://localhost:xxxx/openapi
```

📌 **This is Swagger-style documentation** (minimal version).

---

## 🧠 Architecture After This Lab

```
WebApp
│
├── Razor Pages        → Human UI
├── Minimal APIs       → Machine / System UI
├── OpenAPI            → Documentation
└── Catalog Library    → Business Models
```

---

## 🧪 Student Exercises

1. Add `/api/alerts`
2. Filter running machines only
3. Add temperature threshold logic
4. Consume API from Razor page
5. Convert anonymous objects to models

---

## 🎓 Mentor Insight (Transflower Style)

> “Controllers are classrooms.
> Minimal APIs are **production floors**.
> Both are needed to build engineers.”


