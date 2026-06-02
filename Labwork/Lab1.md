
# 🧪 Hands-On Lab: Building a Simple E-Commerce .NET Solution (CLI Based)
Below is a **student-friendly, step-by-step lab guide** explaining **how to build the given .NET solution from scratch using CLI commands on Windows**, exactly matching the output you shared.
## 🎯 Lab Objective

Students will learn how to:

* Create a **.NET Solution**
* Add multiple **projects** (Console App + Class Library)
* Set **project references**
* Build and run the solution using **dotnet CLI**


## 🛠 Prerequisites

* Windows 10 / 11
* .NET SDK installed (`dotnet --version`)
* Basic C# knowledge
* Command Prompt or PowerShell


## 📁 Step 1: Create Workspace Folder

Open **Command Prompt** and navigate to your working directory.

```cmd
D:
cd tryout
cd SeniorsTrg
```

Create a new folder for the solution:

```cmd
mkdir ECommerce
```

📌 **Why?**
This folder will contain the entire E-Commerce solution (all projects).


## 📦 Step 2: Create a .NET Solution File

Go inside the folder:

```cmd
cd ECommerce
```

Create a solution file:

```cmd
dotnet new sln
```

✅ Output:

```
The template "Solution File" was created successfully.
```

📌 **Why?**
A solution (`.sln`) is a **container** that groups multiple projects together.


## 🖥 Step 3: Create Console Application (Test Project)

Create a console app named **Test**:

```cmd
dotnet new console -o Test
```

📁 Resulting structure:

```
ECommerce
 ├── Test
 │   ├── Program.cs
 │   └── Test.csproj
 └── ECommerce.sln
```

📌 **Why?**
This will act as the **client / UI / runner** for our application.



## ➕ Step 4: Add Console Project to Solution

```cmd
dotnet sln add .\Test\Test.csproj
```

✅ Output:

```
Project `Test\Test.csproj` added to the solution.
```

📌 **Why?**
Projects must be added to the solution to build together.

---

## 📚 Step 5: Create Class Library (Catalog Layer)

Create a class library named **Cataog** (intentional spelling as per your output):

```cmd
dotnet new classlib -o Cataog
```

📁 Structure:

```
ECommerce
 ├── Cataog
 │   └── Cataog.csproj
 ├── Test
 └── ECommerce.sln
```

📌 **Why?**
This represents the **business/domain layer** (e.g., Product, Catalog logic).


## ➕ Step 6: Add Class Library to Solution

```cmd
dotnet sln add .\Cataog\Cataog.csproj
```

📌 **Why?**
Now both projects are part of the same solution.


## 🔗 Step 7: Add Project Reference

Navigate to the **Test** project:

```cmd
cd Test
```

Add reference to the Catalog project:

```cmd
dotnet add reference ..\Cataog\Cataog.csproj
```

📌 **Why?**
This allows the **Test** project to use classes defined in **Cataog**.

> 🔁 Think of it like:
>
> * **Test** → depends on → **Cataog**


## 🔙 Step 8: Return to Solution Folder

```cmd
cd ..
```


## 🏗 Step 9: Build the Entire Solution

```cmd
dotnet build
```

✅ Output highlights:

```
Cataog net10.0 succeeded with warning(s)
Test net10.0 succeeded
Build succeeded
```

📌 **Important Learning**

* Build succeeded even with warnings
* Warnings are about **non-nullable properties**
* This is common in modern C# and not an error


## ⚠ Understanding the Warnings (Conceptual)

```text
Non-nullable property 'Title' must contain a non-null value
```

📌 **Meaning:**
C# expects non-nullable properties to be initialized.

📘 **Teaching Tip:**
This is a great place to introduce:

* `required` keyword
* Constructors
* Nullable reference types


## ▶ Step 10: Run the Console Application

```cmd
dotnet run --project .\Test\Test.csproj
```

✅ Output:

```
Product Details: Id=1, Title=Gerbera, Description=Wedding flowers., Price=4.99
Hello, World!
```

📌 **What happened?**

* `Test` project executed
* It used `Product` from `Cataog`
* Console displayed product details


## 🧠 Architecture Understanding (For Students)

```
ECommerce Solution
│
├── Test (Console App)
│   └── UI / Runner / Client
│
└── Cataog (Class Library)
    └── Domain / Business Logic
```

## ✅ Key Learning Outcomes

Students now understand how to:

- ✔ Use **dotnet CLI** professionally
- ✔ Structure a **multi-project solution**
- ✔ Apply **project references**
- ✔ Build and run a solution
- ✔ Separate **UI** and **Business Logic**



## 🚀 Mentor Note

> “Real software is never a single project.
> This lab teaches how **real-world applications are organized**.”
