
# Reflection in .NET

*"Imagine you're a software detective. The program is your case file, and your mission is to examine what classes, properties, and methods it has — *without ever opening the source code*. That’s exactly what Reflection in .NET lets you do."*

### 🧠 What Is Reflection?

Reflection is the ability of a program to inspect and interact with its own metadata — like looking into a mirror at runtime and asking:

* What am I made of?
* What methods do I have?
* What properties or constructors do I expose?

In .NET, **Reflection is enabled by the `System.Reflection` namespace**. It gives you tools to read and manipulate the structure of assemblies, types, and objects dynamically.

### 🔍 Common Use Cases During Refactoring

Reflection becomes your secret tool in many advanced refactoring and real-world scenarios:

1. ✅ **Dependency Injection (DI)**
   Automatically scan and register types in an IoC container without manual wiring.

2. 🛠️ **Code Analysis Tools**
   Tools like ReSharper or Roslyn-based analyzers use reflection to inspect method signatures or attributes.

3. 🧬 **Code Generation & Dynamic Proxies**
   Generate classes or behaviors on the fly when splitting large monoliths or enabling plug-ins.

4. 📦 **Dynamic Module Loading**
   Useful in microservice or plugin architectures where modules load/unload at runtime.

5. ⚠️ **API Migration & Obsolete Detection**
   Identify types/methods marked with `[Obsolete]` and auto-suggest alternatives.

6. 🌀 **AOP (Aspect-Oriented Programming)**
   Add behaviors like logging or security without altering the core logic.

7. 🧪 **Reflection-Based Testing**
   Auto-discover and run test methods using conventions or attributes.

8. 🧾 **Dynamic Configuration Binding**
   Map configuration values to strongly typed objects at runtime.

### 🔧 How It Works in C\#

Let's take a small practical example:

```csharp
int num = 42;
Type type = num.GetType();
Console.WriteLine(type); // Output: System.Int32
```

Here, `GetType()` returns the metadata about the variable `num`.

### 💡 Deep Dive: Accessing Members Using Reflection

Given a class inside a compiled DLL:

```csharp
namespace Test
{
    public class Calculator
    {
        public double Number { get; set; }
        public double Add(double value) => Number + value;
    }
}
```

You can do the following:

```csharp
Assembly assembly = Assembly.LoadFile(@"c:\Test.dll");
Type calcType = assembly.GetType("Test.Calculator");
object calc = Activator.CreateInstance(calcType);

// Set Property
PropertyInfo prop = calcType.GetProperty("Number");
prop.SetValue(calc, 10.0);

// Call Method
MethodInfo addMethod = calcType.GetMethod("Add");
double result = (double)addMethod.Invoke(calc, new object[] { 5.0 });
Console.WriteLine(result); // Output: 15
```

You didn’t even have to reference `Test.dll` at compile time!


### 🔍 Key Classes in System.Reflection

* `Assembly`: Loads and reflects assemblies.
* `Type`: Central to accessing metadata like methods, properties, and fields.
* `MethodInfo`, `ConstructorInfo`, `PropertyInfo`, `FieldInfo`: Let you dig into and manipulate each part of a type.
* `Activator`: Creates instances dynamically.

### 🧬 Bonus: Dynamic Code with Reflection.Emit

*"Reflection tells you about types... but what if you could *create* new types and methods during runtime?"*

Yes, that's possible using `System.Reflection.Emit`.

```csharp
AssemblyName name = new("DynamicLib");
AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
ModuleBuilder mb = ab.DefineDynamicModule("MainModule");
TypeBuilder tb = mb.DefineType("MyType", TypeAttributes.Public);

// Method
MethodBuilder method = tb.DefineMethod("Greet", MethodAttributes.Public | MethodAttributes.Static, typeof(void), null);
ILGenerator il = method.GetILGenerator();
il.Emit(OpCodes.Ldstr, "Hello from emitted IL!");
il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
il.Emit(OpCodes.Ret);

// Finalize and run
Type t = tb.CreateType();
t.GetMethod("Greet").Invoke(null, null); // Output: Hello from emitted IL!
```

🧠 **Reflection.Emit** is for expert use — dynamic compilers, DSLs, or code generators.


### ⚠️ Reflection Comes with Warnings

* **Performance**: It's slower than direct access.
* **Maintainability**: Code becomes harder to understand and debug.
* **Security**: Can access private members — handle with care.

### 🧩 Real-Life Tools Using Reflection

* **xUnit/NUnit/MSTest**: Auto-discover `[Test]` methods.
* **ASP.NET Core**: Registers services using reflection.
* **Entity Framework**: Maps models dynamically.
* **Swagger**: Builds documentation by scanning API metadata.

### 🧠 Reflection vs Traditional Access

| Feature          | Traditional Code | Reflection             |
| ---------------- | ---------------- | ---------------------- |
| Compile-time     | Strongly typed   | Dynamic and late-bound |
| Introspection    | Not possible     | Fully supported        |
| Access modifiers | Enforced         | Can access private too |
| Performance      | Fast             | Slower                 |


## 👨‍🏫 Final Thought from Your Mentor

*"Reflection is like having X-ray vision into your code. Use it to inspect, extend, or bend your system — but use it wisely. Just like a surgeon, precision and purpose are key!"*
 
