# Understanding .NET Runtime Execution Process

## 1. Let's begin with a question!

**Mentor**: Students, when you write a C# program, does your computer's CPU understand C#?

**Student**: No, Sir. CPU understands machine instructions.

**Mentor**: Excellent! Then how does this C# statement execute?

```
int c = a + b;
```

Does the CPU directly execute this statement?

**Student**: No, Sir. Something must convert it into machine instructions.

**Mentor**: Exactly! That conversion and execution process is what we are going to understand today.

Remember:

> A programmer writes source code, a compiler translates it, a runtime prepares it, and the CPU executes machine instructions.

## 2. Big Picture: .NET Execution Architecture

Let's understand the complete journey of a C# program.

```
             DEVELOPER
                 |
                 | Writes
                 v
        +-------------------+
        |   C# Source Code  |
        |     Program.cs   |
        +-------------------+
                 |
                 | Roslyn Compiler
                 v
        +-------------------+
        |    IL / CIL       |
        | Intermediate      |
        | Language          |
        +-------------------+
                 |
                 | Packaged with
                 | Metadata
                 v
        +-------------------+
        |  .NET Assembly    |
        |    MyApp.dll      |
        +-------------------+
                 |
                 | Application Startup
                 v
        +-------------------+
        |   .NET Runtime    |
        |     CoreCLR       |
        +-------------------+
                 |
          +------+------+
          |             |
          v             v
     Assembly       Type System
     Loading        & Metadata
          |             |
          +------+------+
                 |
                 v
        +-------------------+
        |   JIT Compiler    |
        |                   |
        |    IL -> Native   |
        +-------------------+
                 |
                 v
        +-------------------+
        | Native Machine    |
        | Code              |
        +-------------------+
                 |
                 v
        +-------------------+
        |       CPU         |
        | Executes Code     |
        +-------------------+
```

**Mentor's** observation: Notice that compilation and execution are two different phases.

- Compilation: C# → IL.
- Execution: IL → Native Machine Code → CPU.

## 3. Understand the individual components

### Step 1: C# Source Code

Consider this simple application:

```
using System;

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

The developer creates:

```
Program.cs
```

This is human-readable source code.

Ask yourself: Can the processor understand `Console.WriteLine()` directly?

No. It needs translation into instructions that the processor can execute.

### Step 2: Roslyn — C# Compiler
 
The .NET SDK invokes the C# compiler, commonly known as Roslyn.

Its responsibilities include:

1. Lexical and syntax analysis.
2. Semantic analysis.
3. Type checking.
4. Code generation.
5. Metadata generation.

```
       Program.cs
           |
           v
    +--------------+
    |   Roslyn     |
    | C# Compiler  |
    +--------------+
           |
           v
        IL Code
```

**Mentor**: What does Roslyn generate?

**Student**: Intermediate Language, Sir!

**Mentor**: Correct. It generally does not generate processor-specific native code during ordinary .NET compilation.

### Step 3: What is IL?

IL stands for Intermediate Language.

It is also known as:

- CIL – Common Intermediate Language.
- MSIL – Microsoft Intermediate Language (historical terminology).

For example:

C# statement:

```
int c = a + b;
```

Conceptually, its IL might resemble:

```
ldloc.0
ldloc.1
add
stloc.2
```

Let's understand the instructions:

| IL instruction | Meaning                          |
| -------------- | -------------------------------- |
| `ldloc.0`      | Load local variable 0            |
| `ldloc.1`      | Load local variable 1            |
| `add`          | Add values                       |
| `stloc.2`      | Store result in local variable 2 |

These are not native Intel or ARM instructions.

They are instructions in the .NET intermediate representation.

### Step 4: Assembly — IL + Metadata

The compiler packages IL and metadata into an assembly.


```
         MyApp.dll
       +------------------+
       |                  |
       |    IL Code       |
       |------------------|
       |                  |
       |  Type Metadata   |
       |------------------|
       |                  |
       | Method Metadata  |
       |------------------|
       |                  |
       | Assembly Details |
       |------------------|
       |                  |
       |   References     |
       |                  |
       +------------------+
```

Think of a .NET assembly as a book:
- **IL** = Actual content of the book.
- **Metadata** = Index and description of the content.
- **References** = References to other books.
- **Assembly information** = Book title, edition and publisher details.

The C# compiler (**Roslyn**) packages the generated Intermediate Language (IL), metadata, and assembly information into a .NET assembly (.dll or .exe).

```
                  C# COMPILER (ROSLYN)
                          |
                          |
                    Generated IL
                          |
                    Generated Metadata
                          |
                          v
              +---------------------------+
              |       .NET ASSEMBLY       |
              |                           |
              |       MyApp.dll           |
              |                           |
              +---------------------------+
              |                           |
              |   1. IL CODE              |
              |      - Method bodies      |
              |      - CIL instructions   |
              |                           |
              +---------------------------+
              |                           |
              |   2. TYPE METADATA        |
              |      - Classes            |
              |      - Interfaces         |
              |      - Structures         |
              |      - Enums              |
              |                           |
              +---------------------------+
              |                           |
              |   3. METHOD METADATA      |
              |      - Method names       |
              |      - Parameters         |
              |      - Return types       |
              |                           |
              +---------------------------+
              |                           |
              |   4. ASSEMBLY METADATA    |
              |      - Assembly name      |
              |      - Version            |
              |      - Culture            |
              |      - Public key info    |
              |                           |
              +---------------------------+
              |                           |
              |   5. REFERENCES           |
              |      - System.Runtime     |
              |      - Other assemblies   |
              |      - Dependencies       |
              |                           |
              +---------------------------+
                          |
                          |
                          v
                 .NET RUNTIME (CoreCLR)
                          |
                          v
                 Assembly Loading
                          |
                          v
                    JIT Compiler

```


Typical output:

```
bin/
  Debug/
    net10.0/
      MyApp.dll
      MyApp.deps.json
      MyApp.runtimeconfig.json
```

The `.dll` contains the managed assembly. The accompanying files provide dependency and runtime configuration information.

**Mentor**'s question: Why do we need metadata?

Because the runtime needs information about types, methods, fields, references and other program structures.

## 4. Runtime execution — CoreCLR

The .NET Core execution architecture explains how a C# application moves from source code through compilation, runtime services and JIT compilation before executing on the CPU.


```
                 DEVELOPER
                     |
                     v
           +--------------------+
           |   C# SOURCE CODE   |
           |     Program.cs     |
           +--------------------+
                     |
                     v
           +--------------------+
           |   C# COMPILER      |
           |     ROSLYN         |
           +--------------------+
                     |
                     v
           +--------------------+
           | INTERMEDIATE       |
           | LANGUAGE (IL/CIL)  |
           +--------------------+
                     |
                     v
           +--------------------+
           |   .NET ASSEMBLY    |
           |    MyApp.dll       |
           |                    |
           |  IL + Metadata     |
           |  References        |
           +--------------------+
                     |
                     v
       +--------------------------------+
       |        .NET HOST               |
       |                                |
       |  Application Startup           |
       |  Runtime Selection             |
       |  Runtime Initialization        |
       +--------------------------------+
                     |
                     v
       +--------------------------------+
       |          CORECLR               |
       |      .NET Runtime Engine       |
       |                                |
       |  +--------------------------+  |
       |  |     Assembly Loader      |  |
       |  +--------------------------+  |
       |                                |
       |  +--------------------------+  |
       |  |   Type System & Metadata |  |
       |  +--------------------------+  |
       |                                |
       |  +--------------------------+  |
       |  |    Garbage Collector     |  |
       |  +--------------------------+  |
       |                                |
       |  +--------------------------+  |
       |  |    Exception Handling    |  |
       |  +--------------------------+  |
       |                                |
       |  +--------------------------+  |
       |  |       Threading          |  |
       |  +--------------------------+  |
       |                                |
       |  +--------------------------+  |
       |  |     JIT Compiler         |  |
       |  +--------------------------+  |
       +--------------------------------+
                     |
                     v
           +--------------------+
           |  NATIVE MACHINE    |
           |       CODE         |
           +--------------------+
                     |
                     v
           +--------------------+
           |        CPU         |
           |  Executes Native   |
           |  Instructions      |
           +--------------------+

```



Now comes the interesting part. We have an assembly. But how does it execute? The .NET runtime provides the execution environment.

For modern .NET applications using the standard CoreCLR runtime, CoreCLR provides services such as:

```
                 CoreCLR
                    |
      +-------------+-------------+
      |             |             |
      v             v             v
 Assembly       Type System      JIT
 Loading        & Metadata     Compiler
      |             |             |
      +-------------+-------------+
                    |
      +-------------+-------------+
      |             |             |
      v             v             v
 Garbage       Exception      Threading
 Collection    Handling
```

It also provides runtime infrastructure for managed code execution.

### Step 5: Assembly Loading

The runtime loads the application assembly and resolves the dependencies required for execution.

```
             MyApp.dll
                 |
                 v
          Assembly Loading
                 |
        +--------+--------+
        |        |        |
        v        v        v
      Read    Resolve   Load
    Metadata  References Types
        |        |        |
        +--------+--------+
                 |
                 v
          Runtime Preparation
```

For example:

```
MyApp.dll
   |
   +-- Program
   |      |
   |      +-- Main()
   |
   +-- Employee
   |
   +-- Department
   |
   +-- Dependencies
```

The runtime uses assembly and type information to prepare code for execution.

### Step 6: JIT Compiler

Important Concept

JIT stands for Just-In-Time compiler.

Its job is to convert IL into native machine code when required.

```
          IL Instructions
                 |
                 v
          +-------------+
          |     JIT     |
          |  Compiler   |
          +-------------+
                 |
                 v
         Native Machine Code
                 |
                 v
                CPU
```


**Mentor**: Why do we call it Just-In-Time?

**Student**: Because the IL is compiled into native code when the method needs to execute.

**Mentor**: Exactly!

An important detail: JIT compilation is generally performed method by method, as methods are needed, rather than compiling the entire application into native code at startup.

## 5. Why does .NET use IL?

Let's imagine that we develop an application on Windows x64.

Later, we want to run it on Linux ARM64.

Should we rewrite the entire C# application?

Generally, no.

```
             C# Source Code
                    |
                    v
             Roslyn Compiler
                    |
                    v
                IL Code
                    |
          +---------+---------+
          |         |         |
          v         v         v
       Windows    Linux     macOS
         x64       x64       ARM64
          |         |         |
          v         v         v
         JIT       JIT       JIT
          |         |         |
          v         v         v
       Native    Native    Native
       x64       x64       ARM64
```

The same IL can be compiled into native instructions for different supported processor architectures.

This is one of the important benefits of the .NET execution model.

> Write once, compile to an intermediate representation, and execute on supported environments with the appropriate runtime.

Note: Platform-specific dependencies, native libraries, runtime availability and application configuration can still affect portability.


#### Understand the architecture through three layers

| Layer       | Responsibility                                | Examples           |
| ----------- | --------------------------------------------- | ------------------ |
| Compilation | Converts source code into IL and metadata.    | Roslyn             |
| Runtime     | Loads and manages application execution.      | .NET Host, CoreCLR |
| Execution   | Converts IL into native code and executes it. | JIT, CPU           |

#### Ravi Sir's classroom takeaway:

Think of it like a manufacturing process:
- C# source code is the design.
- Roslyn is the translator.
- IL is the standardized instruction format.
- Assembly is the packaged instruction set.
- CoreCLR is the factory environment.
- JIT is the machine that converts instructions into processor-specific o
perations.
- CPU is the machine that performs the actual work.


Remember: The compiler prepares the program; the runtime manages its execution; the CPU executes native instructions.

## 6. CLR vs CoreCLR — Understand the difference

**Student**s often get confused between CLR and CoreCLR.

Let's clarify.

| Term               | Meaning                                                            |
| ------------------ | ------------------------------------------------------------------ |
| CLR                | Common Language Runtime; the .NET runtime concept and architecture |
| .NET Framework CLR | Original Windows .NET Framework runtime implementation             |
| CoreCLR            | Runtime implementation used by modern .NET applications by default |
| Roslyn             | C# compiler                                                        |
| JIT                | Converts IL into native machine code                               |
| GC                 | Garbage Collector; manages managed memory                          |

Historically:

```
       .NET Framework
              |
              v
             CLR
```

Modern .NET:

```
           .NET
             |
             v
          Runtime
             |
             v
          CoreCLR
             |
      +------+------+
      |      |      |
      v      v      v
     JIT     GC   Runtime
                  Services
```

A clarification: CLR is the general runtime concept; CoreCLR is not simply another name for every CLR implementation. Modern .NET also supports alternative runtime execution models, including Native AOT.

## 7. What happens when we execute `dotnet run`?

Let's connect theory with practical development. Suppose we create a console application:

```
dotnet new console -n RuntimeDemo

cd RuntimeDemo

dotnet run
```

What happens internally?

```
             Developer
                 |
                 v
           dotnet run
                 |
                 v
          .NET SDK / CLI
                 |
                 v
          Build Application
                 |
                 v
          Roslyn Compiler
                 |
                 v
             MyApp.dll
                 |
                 v
          .NET Host Startup
                 |
                 v
          Load CoreCLR
                 |
                 v
          Load Application
                 |
                 v
            Execute Main()
                 |
                 v
          JIT Compilation
                 |
                 v
           Native Code
                 |
                 v
                CPU
```

In practice, `dotnet run` builds the project when necessary and starts the application through the .NET host. The runtime then handles loading and execution.

Try these commands:

```
dotnet build
```

```
dotnet run
```

```
dotnet --info
```

```
dotnet --list-sdks
```

Lab observation: After building, explore the `bin/Debug` directory and identify the generated assembly and runtime configuration files.

## 8. Advanced Concept: Is JIT the only way?

No!

Modern .NET also supports Ahead-of-Time compilation.

| JIT                                            | Native AOT                                                               |
| ---------------------------------------------- | ------------------------------------------------------------------------ |
| IL compiled into native code at runtime        | Application compiled into native code ahead of execution                 |
| Requires a compatible runtime environment      | Can produce a native executable with required runtime components         |
| Runtime compilation overhead                   | Avoids ordinary JIT compilation at startup                               |
| Dynamic runtime capabilities are more flexible | Some dynamic features require additional configuration or are restricted |

```
              C# Source
                  |
                  v
             Roslyn
                  |
                  v
              IL Code
                  |
          +-------+-------+
          |               |
          v               v
      JIT Model       Native AOT
          |               |
          v               v
      Runtime JIT     AOT Compiler
          |               |
          v               v
      Native Code     Native Executable
          |               |
          v               v
         CPU             CPU
```

This is an important topic when discussing startup performance, deployment and application architecture.
 
## 9. Mentors Assignment — Explore .NET Internals

Build a small console application and investigate its compilation and execution.

Problem statement: Understand what happens to a C# program between source code and CPU execution.

Tasks:

1. Create a console application using `dotnet new console`.
2. Write a program containing arithmetic operations and method calls.
3. Execute `dotnet build`.
4. Locate the generated `.dll`.
5. Explore the assembly using ILSpy or `ildasm` where supported.
6. Identify IL instructions generated for arithmetic operations.
7. Investigate JIT compilation using available .NET diagnostics tools.
8. Compare JIT deployment with Native AOT.

Expected learning outcome: **Student**s should be able to explain the journey from C# source code to native machine instructions without memorizing definitions.

## 10. Connect this knowledge to ASP.NET Core

Now think about a real-world application developed at Transflower. Suppose we build an ASP.NET Core Web API. Does the same execution process apply? Absolutely!

```
       React Frontend
              |
              | HTTP Request
              v
      ASP.NET Core Web API
              |
              v
        C# Controllers
              |
              v
       Business Services
              |
              v
        Data Access Layer
              |
              v
          MySQL DB
```

All the managed C# code in the backend follows the .NET compilation and runtime execution model. This is why understanding CLR, CoreCLR, JIT, GC and assemblies is important even when developing modern ASP.NET Core applications.

## Final takeaway from Mentor

> Students, don't become programmers who only know how to write C# syntax. Become engineers who understand what happens behind that syntax. When you understand the compiler, runtime, memory management and CPU execution, you start thinking beyond frameworks. Remember this execution pipeline:

```
C# → Roslyn → IL → Assembly
                       |
                       v
                    CoreCLR
                       |
                       v
                  Assembly Loader
                       |
                       v
                      JIT
                       |
                       v
                Native Machine Code
                       |
                       v
                      CPU
```

Next **Mentor** Session: .NET Memory Management — Stack, Heap, Garbage Collection, Generations, IDisposable and the difference between managed and unmanaged resources.