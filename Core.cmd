# .NET Core Concpets


### Common Language Runtime (CLR): 

An execution engine of .net is Common Language Runtime(CLR).
CLR does 4 primary important things:
1.	Garbage collection
2.	CAS (Code Access Security)
3.	CV (Code Verification)
4.	IL to Native translation.

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

