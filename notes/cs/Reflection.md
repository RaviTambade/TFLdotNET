# Reflection

Reflection in .NET is a powerful mechanism that allows you to inspect and manipulate metadata of types, methods, properties, and other program elements at runtime. While it's not typically the first tool you reach for during refactoring, it can be incredibly useful in certain scenarios:

1. **Dependency Injection (DI)**: During refactoring, you might be restructuring your codebase to use dependency injection. Reflection can help in scanning assemblies for types and automatically registering them with the DI container without explicitly specifying each dependency.

2. **Automated Code Analysis and Refactoring Tools**: Reflection can be used by automated code analysis and refactoring tools to examine code and suggest or perform refactorings. For example, tools can analyze method signatures, find method invocations, and suggest changes to improve code quality.

3. **Code Generation**: Reflection can be used to generate code dynamically during refactoring. For instance, if you're splitting a monolithic class into smaller classes, reflection can help automate the generation of new classes and members based on specific criteria.

4. **Dynamic Loading and Unloading of Assemblies**: If you're refactoring a large application into smaller modules or microservices, reflection can aid in dynamically loading and unloading assemblies at runtime based on configuration or user input.

5. **Migration to New APIs**: Reflection can help identify and update code that uses deprecated APIs during refactoring. By inspecting assemblies for types and methods that are marked obsolete, you can automatically refactor the code to use the recommended alternatives.

6. **Aspect-Oriented Programming (AOP)**: During refactoring, you might introduce aspects such as logging, caching, or error handling to existing code. Reflection can facilitate the application of these aspects by dynamically intercepting method calls and injecting the required behavior.

7. **Reflection-based Testing**: Reflection can be used to automate testing during refactoring. For example, you can dynamically discover and execute test methods based on naming conventions or attributes, reducing the manual effort required to maintain test suites.

8. **Configuration and Settings Management**: Reflection can assist in refactoring code related to configuration and settings management. You can use reflection to dynamically read and write configuration values from configuration files or other sources, making the code more flexible and maintainable.

While reflection provides powerful capabilities, it's essential to use it judiciously during refactoring, as it can introduce complexity and performance overhead. Additionally, reflection-based code may be less statically typed and more prone to runtime errors, so thorough testing is crucial.

Reflection is a feature of .NET to write code that can examine and modify other pieces of code dynamically is quite a useful power. Using reflection, you can, for instance, load a class dynamically from an Assembly, test whether a given type has a specific member, and even create code dynamically.

A program reflects on itself when it extracts metadata from its assemblies, then uses it to modify its own behavior or inform the user. You can compare Reflection to C++ RTTI (Runtime Type Information), except that it has a much wider swath of capabilities. When you write a C# program that uses reflection, you can use either the TypeOf operator or the GetType() method to get the object’s type.

Several important tools make use of reflection to enable their working. One example is unit test frameworks, which use reflection to identify test classes and methods marked with the necessary attributes.

Reflection can be used to create applications called type browsers which allow users to select types and then read the data provided about them

```
    // Using GetType to obtain type information:

    int i = 42;
    System.Type type = i.GetType();
    System.Console.WriteLine(type);
```

Implementing reflection in C# requires a two-step process. You first get the “type” object, then use the type to browse members such as “methods” and “properties.”


To access the sample class Calculator from Test.dll assembly, the Calculator class should be defined as the following:

```
namespace Test
{
    public class Calculator
    {
        public Calculator() { ... }
        private double _number;
        public double Number { get { ... } set { ... } }
        public void Clear() { ... }
        private void DoClear() { ... }
        public double Add(double number) { ... }
        public static double Pi { ... }
        public static double GetPi() { ... }
    }
}
```

Then, you can use reflection to load the Test.dll assembly:

```
// dynamically load assembly from file Test.dll
Assembly testAssembly = Assembly.LoadFile(@"c:\Test.dll");
```

To create an instance of the calculator class:

```
// get type of class Calculator from just loaded assembly
Type calcType = testAssembly.GetType("Test.Calculator");
 

// create instance of class Calculator
object calcInstance = Activator.CreateInstance(calcType);
And access its members (the following examples illustrate getting values for the public double Number property):

// get info about property: public double Number
PropertyInfo numberPropertyInfo = calcType.GetProperty("Number");

// get value of property: public double Number
double value = (double)numberPropertyInfo.GetValue(calcInstance, null);

// set value of property: public double Number
numberPropertyInfo.SetValue(calcInstance, 10.0, null);

```

### How Reflection in C# Works

The main class for reflection is the System.Type class, which is a partial abstract class representing a type in the Common Type System (CTS). When you use this class, you can find the types used in a module and namespace and also determine if a given type is a reference or value type. You can parse the corresponding metadata tables to look through these items:

- <b>Fields</b>
- <b>Properties</b>
- <b>Methods</b>
- <b>Events</b>

The System.Type class also comes with several instance methods you can use to get information from a specific type. Here’s a list of some of the most important ones:

- <b>GetConstructors()</b> – gets the constructors for the type as an array of System.Reflection.ConstructorInfo.
- <b>GetMethods()</b> – gets the methods for the type as an array of System.Reflection.MethodInfo.
- <b>GetMembers()</b> – gets the members for the type as an array of System.Reflection.MemberInfo.

The System.Reflection namespace, as the name suggests, holds several useful classes if you want to work with reflection. Some of those are the three ones you’ve just read about. Here are some more important ones:

- <b>ParameterInfo</b>
- <b>Assembly</b>
- <b>AssemblyName</b>
- <b>PropertyInfo</b>

Late bindings can also be achieved through reflection. To illustrate, you might not know which assembly to load during compile time. In this instance, you can ask the user to enter the assembly name and type during run time so the application can load the appropriate assembly. With the System.Reflection.Assembly type, you can get three static types which allow you to load an assembly directly:

- <b>LoadFrom</b>
- <b>LoadWithPartialName</b>

When you consider that an assembly is a logical DLL or EXE and a manifest is a detailed overview of an assembly, then it makes sense that a PE (portable executable) file for CTS would have the extension of .dll or .exe. Within the PE file is mainly metadata, which contains a variety of different tables such as a:

- <b>Filed definition table</b>
- <b>Type definition table</b>
- <b>Method definition table</b>

When you parse these tables, you can retrieve an assembly’s types and attributes.

#### Uses for Reflection C#
There are several uses including:

1. Use Module to get all global and non-global methods defined in the module.
2. Use MethodInfo to look at information such as parameters, name, return type, access modifiers and implementation details.
3. Use EventInfo to find out the event-handler data type, the name, declaring type and custom attributes.
4. Use ConstructorInfo to get data on the parameters, access modifiers, and implementation details of a constructor.
5. Use Assembly to load modules listed in the assembly manifest.
6. Use PropertyInfo to get the declaring type, reflected type, data type, name and writable status of a property or to get and set property values.
7. Use CustomAttributeData to find out information on custom attributes or to review attributes without having to create more instances.

## System.Reflection.Emit Namespace

We can Create a dynamic library using `System.Reflection.Emit`. The dynamic library could add IL (Intermediate Language) code dynamically at runtime and emitting it as a new assembly. Here's a simple example demonstrating how to create a dynamic library with a single class containing a method using `System.Reflection.Emit`:

```csharp
//Program.cs file content

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;

// Create a dynamic assembly
AssemblyName assemblyName = new AssemblyName("DynamicLibrary");
AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

// Define a dynamic module
ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");

// Define a dynamic type
TypeBuilder typeBuilder = moduleBuilder.DefineType("DynamicClass", TypeAttributes.Public);

// Define a dynamic method
MethodBuilder methodBuilder = typeBuilder.DefineMethod("DynamicMethod", MethodAttributes.Public | MethodAttributes.Static, typeof(void), null);
ILGenerator ilGenerator = methodBuilder.GetILGenerator();

// Emit IL instructions
ilGenerator.Emit(OpCodes.Ldstr, "Welcome to Transflower Dynamic Code generated using Refletion!"); // Load string onto the stack
ilGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) })); 
// Call Console.WriteLine method
ilGenerator.Emit(OpCodes.Ret);
// Return from method


// Create the type
Type dynamicType = typeBuilder.CreateType();


// Instantiate the dynamic type and invoke the method
object   instance = Activator.CreateInstance(dynamicType);
Type tt = instance.GetType();
BindingFlags flags = BindingFlags.InvokeMethod | BindingFlags.Instance |
          BindingFlags.Public | BindingFlags.Static;

tt.InvokeMember("DynamicMethod", flags, null,null,null);


```

This example dynamically creates a library named "DynamicLibrary.dll" containing a single class named "DynamicClass" with a method named "DynamicMethod". Inside the method, it emits IL instructions to load a string onto the stack, call `Console.WriteLine`, and return. Finally, it saves the dynamic assembly to a file and then loads and invokes the method dynamically.

Please note that working with `System.Reflection.Emit` can be quite complex, and this example provides just a basic illustration. It's essential to thoroughly understand IL and the workings of the Reflection.Emit namespace before using it extensively in production code.