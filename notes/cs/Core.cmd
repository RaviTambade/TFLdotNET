## .NET Core Concpets


.NET has the following design points:

- <b>Productivity is full-stack</b> with runtime, libraries, language, and tools all contributing to developer user experience.
- <b>Safe code</b> is the primary compute model, while unsafe code enables additional manual optimizations.
- <b>Static and dynamic code</b> are both supported, enabling a broad set of distinct scenarios.
- <b>Native code interop and hardware intrinsics</b> are low cost and high-fidelity (raw API and instruction access).
- <b>Code is portable across platforms</b> (OS and chip architecture), while platform targeting enables specialization and optimization.
- <b>Adaptability across programming domains</b> (cloud, client, gaming) is enabled with specialized implementations of the general-purpose programming model.
- <b>Industry standards like OpenTelemetry and gRPC </b>are favored over bespoke solutions.

.NET is maintained by Microsoft and the community. It is regularly updated to ensure users deploy secure and reliable applications to production.

## .NET Components
.NET includes the following components:

- <b>Runtime</b> -- executes application code.
- <b>Libraries</b> -- provides utility functionality like JSON parsing.
- <b>Compiler</b> -- compiles C# (and other languages) source code into (runtime) executable code.
- <b>SDK and other tools</b> -- enable building and monitoring apps with modern workflows.
- <b>App stacks</b> -- like ASP.NET Core and Windows Forms, that enable writing apps.


## .NET implementations
A .NET app is developed for one or more implementations of .NET. Implementations of .NET include .NET Framework, .NET 5+ (and .NET Core), and Mono.

Each implementation of .NET includes the following components:

- One or more runtimes—for example, .NET Framework CLR and .NET 8 CLR.
- A class library—for example, .NET Framework Base Class Library and .NET 8 Base Class Library.
- Optionally, one or more application frameworks—for example, ASP.NET, Windows Forms, and Windows Presentation Foundation (WPF) are included in .NET Framework and .NET 5+.
- Optionally, development tools. Some development tools are shared among multiple implementations.


## .NET ecosystem
There are multiple variants of .NET, each supporting a different type of app. The reason for multiple variants is part historical, part technical.

.NET implementations:

- .NET Framework -- The original .NET. It provides access to the broad capabilities of Windows and Windows Server. It is actively supported, in maintainenance.
- Mono -- The original community and open source .NET. A cross-platform implementation of .NET Framework. Actively supported for Android, iOS, and WebAssembly.
- .NET (Core) -- Modern .NET. A cross-platform and open source implementation of .NET, rethought for the cloud age while remaining significantly compatible with .NET Framework. Actively supported for Linux, macOS, and Windows.


There are four .NET implementations that Microsoft supports:

- .NET 6 and later versions
- .NET Framework
- Mono
- UWP
.NET, previously referred to as .NET Core, is currently the primary implementation. .NET (8) is built on a single code base that supports multiple platforms and many workloads, such as Windows desktop apps and cross-platform console apps, cloud services, and websites. Some workloads, such as .NET WebAssembly build tools, are available as optional installations.


## .NET Framework
.NET Framework is the original .NET implementation that has existed since 2002. Versions 4.5 and later implement .NET Standard, so code that targets .NET Standard can run on those versions of .NET Framework. It contains additional Windows-specific APIs, such as APIs for Windows desktop development with Windows Forms and WPF. .NET Framework is optimized for building Windows desktop applications.

## Mono
Mono is a .NET implementation that is mainly used when a small runtime is required. It is the runtime that powers Xamarin applications on Android, macOS, iOS, tvOS, and watchOS and is focused primarily on a small footprint. Mono also powers games built using the Unity engine.

It supports all of the currently published .NET Standard versions.

Historically, Mono implemented the larger API of .NET Framework and emulated some of the most popular capabilities on Unix. It is sometimes used to run .NET applications that rely on those capabilities on Unix.

Mono is typically used with a just-in-time compiler, but it also features a full static compiler (ahead-of-time compilation) that is used on platforms like iOS


## Universal Windows Platform (UWP)
UWP is an implementation of .NET that is used for building modern, touch-enabled Windows applications and software for the Internet of Things (IoT). It's designed to unify the different types of devices that you may want to target, including PCs, tablets, phones, and even the Xbox. UWP provides many services, such as a centralized app store, an execution environment (AppContainer), and a set of Windows APIs to use instead of Win32 (WinRT). Apps can be written in C++, C#, Visual Basic, and JavaScript.


### Common Language Runtime (CLR): 

An execution engine of .net is Common Language Runtime(CLR).
CLR does 4 primary important things:
1.	Garbage collection
2.	CAS (Code Access Security)
3.	CV (Code Verification)
4.	IL to Native translation.

The runtime provides the following benefits:

- Performance improvements.
- The ability to easily use components developed in other languages.
- Extensible types provided by a class library.
- Language features such as inheritance, interfaces, and overloading for object-oriented programming.
- Support for explicit free threading that allows creation of multithreaded and scalable applications.
- Support for structured exception handling.
- Support for custom attributes.
- Garbage collection.
- Use of delegates instead of function pointers for increased type safety and security.


### Common Type System (CTS): - 
CTS ensure that data types defined in two different languages get compiled to a common data type. This is useful because there may be situations when we want code in one language to be called in other language. We can see practical demonstration of CTS by creating same application in C# and VB.Net and then compare the IL code of both applications. Here the data type of both IL code is same.
 
### Common Language Specification (CLS):
CLS is a subset of CTS. CLS is a set of rules or guidelines. When any programming language adheres to these set of rules it can be consumed by any .Net language.
e.g. 	Lang must be object oriented, each object must be allocated on heap,
               Exception handling supported. 
Also each data type of the language should be converted into CLR understandable types by the Lang compiler. 

All types understandable by CLR forms CTS (common type system) which includes: 
```
System.Byte, System.Int16, System.UInt16, 
System.Int32, System.UInt32, System.Int64,         
System.UInt64, System.Char, System.Boolean, etc.
```

### Assembly Loader:

When a .NET application runs, CLR starts to bind with the version of an assembly that the application was built with. It uses the following steps to resolve an assembly reference:
1)   Examine the Configuration Files
2)   Check for Previously Referenced Assemblies
3)   Check the Global Assembly Cache
4)   Locate the Assembly through Codebases or Probing


### MSIL Code Verification and Just In Time compilation (JIT)

When .NET code is compiled, the output it produces is an intermediate language (MSIL) code that requires a runtime to execute the IL. So during assembly load at runtime, it first validates the metadata then examines the IL code against this metadata to see that the code is type safe. When MSIL code is executed, it is compiled Just-in-Time and converted into a platform-specific code that's called native code.
Thus any code that can be converted to native code is valid code. Code that can't be converted to native code due to unrecognized instructions is called Invalid code. During JIT compilation the code is verified to see if it is type-safe or not.

### Garbage Collection
 “Garbage” consists of objects created during a program’s execution on the managed heap that are no longer accessible by the program. Their memory can be reclaimed and reused with no adverse effects.
The garbage collector is a mechanism which identifies garbage on the managed heap and makes its memory available for reuse. This eliminates the need for the programmer to manually delete objects which are no longer required for program execution. This reuse of memory helps reduce the amount of total memory that a program needs to run.


### C# 
C# is a cross-platform general purpose language that makes developers productive while writing highly performant code.
