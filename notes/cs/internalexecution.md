## .NET Runtime Execution Process

When you write a C# program, the C# source code does **not** directly execute on the CPU. It goes through compilation, **IL (Intermediate Language)** generation, loading by the **CLR/CoreCLR**, and finally **JIT compilation** into native machine code.

### ASCII Diagram

```text
                C# SOURCE CODE
                     │
                     │  Program.cs
                     ▼
              ┌─────────────────┐
              │   C# Compiler   │
              │     Roslyn      │
              │     (csc)       │
              └────────┬────────┘
                       │
                       │ Compile
                       ▼
              ┌─────────────────┐
              │   IL / CIL      │
              │ Intermediate    │
              │ Language        │
              └────────┬────────┘
                       │
                       │ Stored in
                       ▼
              ┌─────────────────┐
              │ .NET Assembly   │
              │                 │
              │ .dll / .exe     │
              │                 │
              │ IL + Metadata   │
              └────────┬────────┘
                       │
                       │ Application starts
                       ▼
              ┌─────────────────┐
              │     .NET        │
              │     Runtime     │
              │                 │
              │    CoreCLR      │
              └────────┬────────┘
                       │
             ┌─────────┴─────────┐
             │                   │
             ▼                   ▼
      ┌──────────────┐    ┌──────────────┐
      │ Assembly     │    │ Type /       │
      │ Loader       │    │ Metadata     │
      └──────┬───────┘    │ Processing   │
             │            └──────┬───────┘
             └──────────┬────────┘
                        ▼
                 ┌─────────────┐
                 │     JIT     │
                 │  Compiler   │
                 └──────┬──────┘
                        │
                        │ IL → Native Code
                        ▼
                 ┌─────────────┐
                 │ Native CPU  │
                 │    Code     │
                 └──────┬──────┘
                        │
                        ▼
                 ┌─────────────┐
                 │     CPU     │
                 │  Executes   │
                 │ instructions│
                 └─────────────┘
```

# 1. Step-by-Step Execution Process

Suppose we write:

```csharp
class Program
{
    static void Main()
    {
        int a = 10;
        int b = 20;
        int c = a + b;

        Console.WriteLine(c);
    }
}
```

### Step 1 — C# Source Code

The developer writes:

```text
Program.cs
```

This is **human-readable C# code**.

```text
C# Source
   ↓
Program.cs
```

# 2. C# Compiler — Roslyn

The .NET SDK invokes the C# compiler, commonly called **Roslyn**.

```text
Program.cs
    │
    ▼
 C# Compiler
  (Roslyn)
    │
    ▼
   IL
```

The compiler performs several tasks:

* Syntax analysis
* Semantic analysis
* Type checking
* Code generation
* Metadata generation

The important point is:

> **The C# compiler generally does not generate CPU-specific machine code.**

It generates **IL/CIL**.

# 3. What is IL?

**IL = Intermediate Language**

It is also commonly called:

* CIL — Common Intermediate Language
* MSIL — Microsoft Intermediate Language

For example, conceptually:

```text
C#:

int c = a + b;
```

may become IL instructions resembling:

```text
ldloc.0
ldloc.1
add
stloc.2
```

These are **not Intel/AMD CPU instructions**.

They are instructions understood by the .NET execution environment and later compiled to native code.

# 4. IL + Metadata → Assembly

The compiler packages the generated IL along with metadata into an assembly.

```text
                 Assembly
              ┌──────────────┐
              │              │
              │     IL       │
              │              │
              ├──────────────┤
              │   Metadata   │
              │              │
              ├──────────────┤
              │    Other     │
              │   Assembly   │
              │   Information│
              └──────────────┘
```

Typical output:

```text
MyApp.dll
```

or an executable application such as:

```text
MyApp.exe
```

The important conceptual structure is:

```text
.dll / .exe
   │
   ├── IL
   ├── Type Metadata
   ├── Method Metadata
   ├── Assembly Metadata
   └── References
```

# 5. Application Starts — CoreCLR

When a .NET application runs, the **.NET runtime** is responsible for executing the application.

For modern .NET applications, an important runtime implementation is:

**CoreCLR**

```text
.NET Runtime
     │
     ▼
  CoreCLR
```

CoreCLR provides the execution environment for managed .NET code.

It is responsible for important runtime services such as:

```text
CoreCLR
   │
   ├── Assembly Loading
   ├── Type System
   ├── JIT Compilation
   ├── Garbage Collection
   ├── Exception Handling
   ├── Threading
   └── Runtime Execution
```

# 6. Assembly Loading

CoreCLR loads the required assemblies.

```text
MyApp.dll
   │
   ▼
Assembly Loader
   │
   ├── Load assembly
   ├── Read metadata
   ├── Resolve dependencies
   └── Prepare types/methods
```

For example:

```text
MyApp.dll
   │
   ├── Program
   │     └── Main()
   │
   └── Dependencies
```

# 7. Method Execution and JIT

Now suppose the runtime needs to execute:

```csharp
Main()
```

The method contains IL.

```text
Main()
   │
   ▼
 IL instructions
   │
   ▼
 JIT Compiler
   │
   ▼
 Native Machine Code
```

**JIT = Just-In-Time compiler**

The JIT compiler converts IL into machine code suitable for the current processor/platform.

For example:

```text
             IL
              │
              ▼
        ┌─────────────┐
        │     JIT     │
        │  Compiler   │
        └──────┬──────┘
               │
               ▼
       Native Machine Code
               │
               ▼
              CPU
```

# 8. Why IL Instead of Direct Machine Code?

This is one of the important ideas behind .NET.

Suppose the same C# application runs on:

```text
Windows x64
Linux x64
Linux ARM64
macOS ARM64
```

The C# source can remain essentially the same.

```text
             C# Code
                │
                ▼
             Compiler
                │
                ▼
                IL
                │
        ┌───────┼────────┐
        ▼       ▼        ▼
      x64     ARM64     ...
       │        │
       ▼        ▼
      JIT      JIT
       │        │
       ▼        ▼
 Native x64  Native ARM64
```

Therefore:

> **IL provides an architecture-neutral intermediate representation, while the runtime/JIT produces native code for the target environment.**

# 9. Complete .NET Execution Pipeline

A useful way to remember the entire process is:

```text
┌───────────────────────┐
│    C# Source Code     │
│      Program.cs       │
└───────────┬───────────┘
            │
            │ Roslyn Compiler
            ▼
┌───────────────────────┐
│     IL / CIL Code     │
│                       │
│   Intermediate Code   │
└───────────┬───────────┘
            │
            │ Packaged with
            │ metadata
            ▼
┌───────────────────────┐
│       Assembly        │
│      .dll / .exe      │
└───────────┬───────────┘
            │
            │ Application starts
            ▼
┌───────────────────────┐
│       CoreCLR         │
│                       │
│  .NET Runtime Engine  │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│   Assembly Loading    │
│   Metadata / Types    │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│      JIT Compiler     │
│                       │
│       IL → Native     │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│   Native Machine Code │
└───────────┬───────────┘
            │
            ▼
┌───────────────────────┐
│          CPU          │
│       Executes        │
└───────────────────────┘
```

## 10. Where Does the CLR Fit?

Historically:

```text
.NET Framework
      │
      ▼
     CLR
```

Modern .NET:

```text
.NET
 │
 └── Runtime
      │
      └── CoreCLR
```

So for teaching purposes:

```text
CLR
 │
 ├── Concept of .NET execution environment
 │
 └── CoreCLR
       └── Modern runtime implementation
```

A useful distinction is:

| Component                | Responsibility                       |
| ------------------------ | ------------------------------------ |
| **C# Compiler / Roslyn** | C# → IL                              |
| **IL/CIL**               | Intermediate instructions            |
| **Assembly**             | IL + metadata + assembly information |
| **CoreCLR**              | Runtime execution environment        |
| **JIT**                  | IL → native machine code             |
| **GC**                   | Automatic memory management          |
| **CPU**                  | Executes native machine instructions |

### One-line interview answer

> **In .NET, C# source code is compiled by Roslyn into IL and metadata inside an assembly; when the application runs, CoreCLR loads the assembly and the JIT compiler converts the required IL methods into native machine code, which is then executed by the CPU.**