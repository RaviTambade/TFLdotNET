//Reflection is a technique used for fetching information 
// at runtime

using System.Reflection;  //importing the namespace
using SOA;


string path=@"D:\Ravi\CDAC\PGDAC\IACSD\ECommerce\BLL\bin\Debug\net7.0\bll.dll";


Assembly asm =Assembly.LoadFrom(path);
Console.WriteLine(asm.FullName);
Type [] alltypes=asm.GetTypes();

foreach( Type t in alltypes){
    Console.WriteLine($"Type: {t.FullName}");
    Console.WriteLine("Methods:");
    foreach (MethodInfo method in t.GetMethods())
    {
        Console.WriteLine($" - {method.Name}");
    }

    Console.WriteLine("Properties:");
    foreach (PropertyInfo prop in t.GetProperties())
    {
        Console.WriteLine($" - {prop.Name}");
    }

    Console.WriteLine("Fields:");
    foreach (FieldInfo field in t.GetFields())
    {
        Console.WriteLine($" - {field.Name}");
    }

    Console.WriteLine("Constructors:");
    foreach (ConstructorInfo ctor in t.GetConstructors())
    {
        Console.WriteLine($" - {ctor.Name}");
    }

    //creating instance of a class

    ProductService  svc=new ProductService(); //creating object of ProductService
    svc.AddProduct();

    Console.WriteLine();
}

