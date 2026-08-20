# Mock Dotnet Full Stack Interview

This mock interview closely resembles a **45–60 minute .NET Full Stack Developer interview** conducted by companies hiring freshers or interns, combining project discussion, technical concepts, coding knowledge, and behavioral questions.

## Interviewer: Ravi Tambade

**Good morning, Pranita. Please introduce yourself.**

## Candidate: Pranita Mane

Good morning, Sir. My name is Pranita Mane. I have been working as a .NET Full Stack Developer Intern at Transflower since June 2025.

During my internship, I have worked with C#, ASP.NET Core, REST APIs, SQL Server, React, GitHub, and Entity Framework Core. I have also worked on enterprise application development using layered architecture.

The major project I have contributed to is **TFLComentor**, a mentor-driven skill evaluation platform. Through this project I learned backend development using ASP.NET Core, frontend development using React, database design, REST API development, team collaboration, and Git version control.

I am looking for an opportunity where I can continue learning while contributing as a .NET Full Stack Developer.

## Project Discussion

### Ravi

Can you explain your project?

### Pranita

Our project is called **TFLComentor**.

It is an AI-assisted mentor-driven learning platform that evaluates students based on their skill gaps and helps them follow personalized learning paths.

The application supports four users:

* Student
* Mentor
* Admin
* Employer

The application currently contains seven major modules.

* Skill Taxonomy
* Evaluation Content Management
* Evaluation Services
* Assessment Orchestrator
* Insight Core
* Growth Engine
* Membership & User Management

Each module has a separate responsibility.

### Ravi

Which modules did you work on?

### Pranita

I worked mainly on

* Skill Taxonomy
* Evaluation Content Management

I developed REST APIs, business logic, repository classes and database tables for managing runtimes, concepts, question banks and MCQ options.

I also developed around 15 React components for filtering questions based on question type, status and IDs.

### Ravi

Which technologies did you use?

### Pranita

Backend

* C#
* ASP.NET Core Web API
* Entity Framework Core
* REST APIs

Frontend

* React
* JavaScript
* HTML
* CSS

Database

* SQL Server

Tools

* GitHub
* .NET CLI / NuGet
* Postman

## Architecture Discussion

### Ravi

What architecture did you follow?

### Pranita

Initially we planned Microservices.

But since the project was in its initial stage, we adopted a **Modular Monolithic Architecture**.

Inside every module we followed Layered Architecture.

```
Controller
↓
Service
↓
Repository
↓
Database

```

Each layer has a specific responsibility which makes the application easy to maintain and test.

### Ravi

Why didn't you continue with Microservices?

### Pranita

Microservices introduced deployment complexity, API communication, service discovery and DevOps overhead.

Since our project was only around 20% complete, a Modular Monolith was easier to develop and maintain.

Later we can extract modules into independent microservices.

## Database

### Ravi

Tell me about your database.

### Pranita

Initially our ER diagram contained around 108 tables.

During database optimization we merged similar entities.

Currently we have around 45 production tables.

I mainly worked on

* Runtime
* Concept
* Question Bank
* MCQ Options

tables.

### Ravi

What challenge did you face?

### Pranita

Initially the Question Bank required joining many tables.

```
Question
↓
Concept
↓
Runtime
↓
Framework
↓
Technology

```

The LINQ queries and SQL joins became expensive.

To improve performance, we denormalized some independent tables by storing required information directly in the Question Bank table.

This reduced query complexity and improved API response time.

## ASP.NET Core

### Ravi

What is ASP.NET Core?

### Pranita

ASP.NET Core is a cross-platform, high-performance, open-source framework for building modern, cloud-enabled applications.

It simplifies enterprise application development by providing

* Built-in Dependency Injection (DI)
* Kestrel cross-platform web server
* A unified programming model for MVC and Web APIs
* A robust, configurable Middleware pipeline

### Ravi

Which attributes and patterns have you used?

### Pranita

I frequently used

* `[ApiController]`
* `[Route]`
* `[HttpGet]`
* `[HttpPost]`
* Constructor Injection (for DI)
* `[Table]`
* `[Key]`
* `[DatabaseGenerated]`

## REST APIs

### Ravi

Explain the flow of a REST request.

### Pranita

```
React UI
↓
HTTP Request
↓
Controller
↓
Service
↓
Repository
↓
SQL Server
↓
JSON Response
↓
React UI

```

## React

### Ravi

What React work did you do?

### Pranita

I developed approximately 15 reusable components.

Some examples are

* Question List
* Question Filter
* Search Bar
* Runtime Dropdown
* Status Filter
* Question Details

The components communicate with ASP.NET Core REST APIs using the Fetch API.

## C# Fundamentals

### Ravi

Which OOP concepts did you implement?

### Pranita

All four.
Encapsulation
Private fields with public properties (getters and setters).
Abstraction
Repository interfaces (e.g., `IRepository`).
Inheritance
Common base entity classes.
Polymorphism
Method overriding and interface implementation.

### Ravi

How do you handle unmanaged resources in C#?

### Pranita

While C# has a garbage collector, unmanaged resources require manual cleanup.

Modern C# uses

* The `IDisposable` interface and its `Dispose()` method.
* The `using` statement (or `using` declarations)

for automatic, deterministic resource management to ensure `Dispose()` is called as soon as the object goes out of scope.

## Data Access

### Ravi

How do you prevent SQL Injection, and how do you call stored procedures?

### Pranita

To prevent SQL Injection using raw SQL in ADO.NET, we use parameterized queries with `SqlParameter` instead of string concatenation. In Entity Framework Core, standard LINQ queries are automatically parameterized.

To execute Stored Procedures, we set the `CommandType` to `CommandType.StoredProcedure` and pass the required parameters.

## C# Asynchronous Programming

### Ravi

How does C# perform asynchronous programming?

### Pranita

Using the `async` and `await` keywords along with the `Task` and `Task<T>` classes.

Internally, it uses a state machine generated by the compiler and relies on the .NET Thread Pool to assign I/O-bound or CPU-bound tasks to worker threads without blocking the main application thread.

## Git

### Ravi

How did your team use Git?

### Pranita

Our workflow was

```
Pull latest code
↓
Create feature branch
↓
Commit
↓
Push
↓
Create Pull Request
↓
Code Review
↓
Merge

```

Git helped us collaborate and resolve conflicts efficiently.

## CI/CD

### Ravi

What is CI/CD?

### Pranita

CI means Continuous Integration.
Whenever developers push code to GitHub, automated builds and tests are triggered.
CD means Continuous Deployment or Delivery.
Once all checks pass, the latest version is deployed automatically.

## HR Round

### Ravi

What was the biggest lesson you learned?

### Pranita

Working in a team.
I learned

* Code reviews
* Git collaboration
* Requirement discussions
* Meeting deadlines
* Debugging production issues

I also understood that software engineering is much more than writing code.

### Ravi

Why should we hire you?

### Pranita

I have practical experience building enterprise applications using C#, ASP.NET Core, React, REST APIs, SQL Server, and Git.

I enjoy learning new technologies, solving problems, and working collaboratively. I believe I can contribute effectively while continuously improving my skills.

### Ravi

Do you have any questions for us?

### Pranita

Yes, Sir.

1. What technologies does your team currently use?
2. What kind of training is provided to freshers?
3. What projects would I initially work on?
4. What growth opportunities are available for .NET Full Stack Developers?

## Interviewer's Assessment

**Strengths**

* Clear communication
* Good project understanding
* Strong C# and ASP.NET Core fundamentals
* Good knowledge of layered architecture
* Practical React experience
* Team collaboration using Git
* Sound database concepts
* Understanding of CI/CD and enterprise development

**Areas to Improve**

* Gain deeper knowledge of ASP.NET Core Identity and JWT Authentication
* Learn Microservices in depth
* Explore Docker and Kubernetes
* Improve knowledge of Design Patterns
* Strengthen Data Structures and Algorithms (DSA)
* Practice system design for scalable applications