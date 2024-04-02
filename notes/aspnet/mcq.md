
# Test your .NET Programming knowledge with following  MCQs

Assessment of any type of objective question is normally known as MCQ Test. It is a way of examining in which questions asked to have a single correct answer.MCQ Test includes true/false, multiple-choice, and matching questions. 

Knowledge is tested through hands-on as well as through project work. .NET programming skill get stronger as we do more Project based learning. It matters how we apply concepts to sovle a problem through project.
These MCQ's are just samples for revising .NET and testing our understanding about .NET.


<hr/>

## Practice Test for .NET Learning

<hr/>
1.Which is the following .net cli command used to create and build Single Page Applicaion?

```
A. dotnet new console -o TestApp
B. dotnet new webapp -o TestApp
C. dotnet new react -o TestApp
D. dotnet new ng -o TestApp
``` 

2.Which of the following term define rules for .Net Languages?

```
A. Common Language Specification
B. Common Language Infrastructure 
C. Common Type System 
D. Common Language Runtime
```


3.Which component of .net Architecture defines Events and Delegate?

```
A.CLR
B.CTS
C.CLS
D.DLR
```


4.What is the Assembly scope of an .net type defined in Code ?

```
A. public 
B. protected
C. internal
D. private
``` 

5.Which is the incorrect syntax of array in C#
```
A.int [ , ]  mtrx = new int [2, 3];
B.int [ , ] mtrx = new int [2, 3] { {10, 20, 30}, {40, 50, 60} }
C.int [ ]  [ ]  mtrxj = new  int [2] [];
D.int [ ]  [ ]  mtrxj = new  int [2] [3];
```

6.Which access specifier will you use to make base class members accessible in the derived class and 
not accessible for the rest of the program?
```
A. public 
B. private 
C. protected 
D. static 
``` 

7.What is Nullable type?

```
A.It allows assignment of null to reference type.
B.It allows assignment of null to value type.
C.It allows assignment of null to static class.
D.None of the above.
``` 

8.Which type of inheritance is not supported in C# Programming?

```
A.Multiple Interface Inheritance
B.Mulitple Implementation Inheritance
C.Multiple Level Inheritance.
D.Single base class Inheritance
``` 

9.What is the type of structure (struct) in C# Programming Language?
```
A.Reference type
B.Value type
C.Class type
D.String type
```
 

10.Missing Explicit Typecasting in code leads to following type of error.

```
A. Runtime Error
B. Linking Error
C. Compile Time Error
D. Cyclic Dependency
```

11.Which namespace  provide a specialized form of reflection that enables you to build types at runtime

```
A.System.Reflection
B.System.Type
C.System.Reflection.Emit
D.System.RTTI
``` 

12.What will be the output of given code snippet?

```
class Program
 {
     static void Main(string[] args)
     {
         int[] marks= { 79, 78,36, 84, 23 };
         var result = from n in marks
                       where n >= 0
                       select n;
        foreach (int i in result) 
        Console.Write(i + " ");
        Console.WriteLine();
        Console.ReadLine();
    }
}

```

```
A. 36  23
B. 79 36 84 23
C. 79 78 36 84 23 
D. Run time error
``` 

13.Which type of method calling represent following C# code segment?
```
static void Main(string[] args)
 {
 int num1 =89, num2 = 72;
     swap(ref num1,  ref num2);
     Console.WriteLine(num1 + "  " + num2);
 }
 static void swap(ref int value1,  ref int value2)
 	{  
     int temp;
     temp = value1;
     value1 = value2;
     value2 = temp;
    }

```


```
A. Pass by Value
B. Pass by Pointer
C. Pass by Reference  
D. Parameter Arrays
``` 


14.Which line is wrong in following Code ?

```
1.string query = "SELECT * FROM users";
2.MySqlCommand queryCommand = new MySqlCommand(query, connection);
3.try
4.{
5.    connection.Open();
6.    using (MySqlDataReader reader = queryCommand.ExecuteScalar())
7.    {
8.        while (reader.Read())
9.        {
10.            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Email: {reader["email"]}");
11.        }
12.    }
13.}
14.catch (Exception ex)
15.{
16.    Console.WriteLine($"Error: {ex.Message}");
17.}
18.finally
19.{
20.    connection.Close();
21.}
```

```
A.2
B.6
C.10
D.20
```

15.Which file extention define .net external dependencies needed for buliding .net core application?

```
A.sln
B.csproj
C.xml
D.appsettings.json
```

16.Which of the following statement is correct MySQL database connection string ?

```
A.Server=localhost;Port=3306;Database=mydatabase;User=myuser;Password=mypassword;
B.Server=localhost;Port=3308;Database=mydatabase;User=myuser;Password=mypassword;
C.Server=localhost;Port=3306;Initial Catalog=mydatabase;User=myuser;Password=mypassword;
D.Server=localhost;Port=3306;Database=mydatabase;User=myuser;PWD=mypassword;
```


17. Which of the following class is used to send HTTP requires in .NET?

```
A.  MessageClient 
B.  HttpWebRequest
C.  HttpClient    
D.  WebClient
```

18.What is the name of Default Application Pool in IIS?

```
A. Assmblies are loaded into an application domain before executing the code it contains
B. Application domain consist of domain specific logic of .net application.
C. Web application consist of pool of Application domain.   
D. Application domain maintains Thread pool for concurrency.
```

19.Which of the following object is used along with application object in order to ensure that only one process accesses a variable at a time?

```
A.Synchronize
B.Synchronize()
C.ThreadLock
D.Lock()
``` 

20.Which Class is used to make a thread instance explicitly?
```
A.Thread 
B.ThreadStart
C.ThreadPool   
D.Runnalble
```
 

21.What is the role of HTML helper in ASP.NET MVC?

```
A.Generates html elements   
B.Generates html view
C.Generates html help file
D.Generates model data
```

22.Which ASP.NET MVC filter will be executed at last during request processing in ASP.NET MVC Pipeline?

```
A.Exception filters 
B.Action filters 
C.Authorization filters 
D.Response filters
``` 

23.Which server , we can not deploy asp.net core Application?

```
A. Kestral   
B. IIS
C. Tomcat
D. MySql
``` 

24.ECommerce Application is built using ASP.NET object Model. 
Which ASP.NET object would be used to maintain Shopping Cart across number of requests being received from users?

```
A.Application object 
B.Session object
C.Response object
D.Server object
``` 

25.Which namespace is used ASP.NET MVC for JSON serialization?

```
A.System.Text.Json
B.JsonFormatter.NET
C.GetJson.NET
D.None of the above
```


26.Which of the following statement is TRUE?

```
A.Action method can be static method in a controller class.
B.Action method can be private method in a controller class.
C.Action method can be protected method in a controller class.
D.Action method must be public method in a controller class.
```

27.Visual Studio .NET tool is used to build WCF Service Application project. 
The project contains web.config file. Which is the root element in configuration file for WCF Service declaration?

```
A.System.Service
B.System.ServiceModel  
C.Service.Contract
D.None
```


26.What do you mean by bundling process in ASP.NET MVC application?

```
A.Loading of multiple images in single request.
B.Loading of multiple view files in single request.
C.Loading of caching of multiple script files.
D.Loading of multiple script files in single request.
```


27.Which status code is sent for successful execution of Web API in ASP.NET Core?

```
A.201
B.404
C.500
D.200
```


28.Which of the following is used to check the validity of the model in ASP.NET Web API?

```
A.Mode.Valid
B.Model.IsValid
C.ModelState.IsValid
D.ModelState.Valid
``` 

29.Which component is essential for collecting data using Connected Data Access Mode?

```
A.DataSet
B.DataReader
C.DataRow
D.DataAdapter
```


30.Which configuration file is used to change configuration setting that will affect only the current Web application?

```
A.web.xml
B.appsettings.json
C.Machine.config
D.web.config
```


31.Customer is demanding to buid web solution using decoupled , reusable , stateless Application logic. 
Which type of attribute would help to define reusable, corss platfrom, stateless application logic.

```
A.Controller
B.APIController		
C.Service
D.WebAPI
```


32.Which is not the data object used for data transfer in ASP.NET MVC?
```
A.ViewBag
B.ViewData
C.TempData
D.MetaData			
``` 

34.Which of these is an attribute that you can apply to a controller action or an entire controller that modifies the way in which the action is executed??

```
A.Action filter
B.Result filter
C.Exception filter
D.Authorization filter   
``` 

36.Which line is wrong written in Program.cs file of asp.net core MVC Application?

```
1.var builder = WebApplication.CreateBuilder(args);
2.builder.Services.AddControllersWithViews();
3.var app = builder.Build();
4.if (!app.Environment.IsDevelopment())
5.{
6.    app.UseExceptionHandler("/Home/Error");
7.}
8.app.UseDynamicFiles();
9.app.UseRouting();
10.app.UseAuthorization();
11.app.MapControllerRoute(
12.    name: "default",
13.    pattern: "{controller=Home}/{action=Index}/{id?}");
14.app.Run();

```

```
A.6
B.8
C.9
D.13
```


37.Which is the incorrect syntax  of Razor file in ASP.NET Core?

```
A.@Html.ActionLink("Click", "Controller1", "CheckData", { @class="my-css-classname", data_my_attr="my-attribute"}) 
B.@Html.ActionLink("Click", "Controller1", "CheckData", { @class="my-css-classname", data_my_attr="my-attribute"}) 
C.<a asp-controller="Controller1" asp-method="CheckData" class="my-css-classname" my-attr="my-attribute">Click</a>
D.<a asp-controller="Controller1" asp-action="CheckData" class="my-css-classname" my-attr="my-attribute">Click</a>
``` 

38.Which statement is correct about Response.Output.Write()?

```
A.HttpContext.Response.Output.Write() allows you to buffer output
B.HttpContext.Response.Output.Write() allows you to write formatted output
C.HttpContext.Response.Output.Write() allows you to flush output
D.HttpContext.Response.Output.Write() allows you to stream output
``` 

39.Which of the following tool is used to manage the GAC in .NET Framework?
```
A.RegSvr.exe
B.GacUtil.exe
C.GacSvr32.exe
D.GacMgr.exe
```

40.How could we add data into session in asp.net core?

```
A.HttpContext.Session.SetString("MyKey", "MyValue");
B.HttpContext.Session["MyKey"]= "MyValue";
C.HttpContext.Request.Session["MyKey"]="MyValue";
C.HttpContext.Response.Session["MyKey"]="MyValue";
``` 

41.Which method of DBContext class reflects changes to database for CRUD Operation in .net while implementing Entity Framework?

```
A.Fill 
B.Update
C.SaveChanges
D.Commit
``` 

42.Which File extension is used for defining views in ASP.NET Core MVC?

```
A..html
B..cs
C..cshtml
D.None of the above
```


43.Which of the following is not a member of ADO.NET Command object?

```
A.ExecuteScalar()
B.ExecuteStream()
C.Open()
D.ExecuteReader()
``` 

44.What is the best practice to isolate connection string while using asp.net core application?

```
A.appsettings.json
B.web.config
C.package.json
D.web.xml
```


45.How do you get information from a form that is submitted using the "post" method?

```
A.HttpContext.Request.QueryString
B.HttpContext.Request.Form
C.HttpContext.Response.Method
D.HttpContext.Response.Query
``` 

46.Which degault pages technology is used default for presentation logic implementation in ASP.NET MVC based Applications

```
A.html
B.razor pages
C.web forms
D.None of the above
``` 

47. Which Request/Response  data fromat  supported by Web API by default?
A.JSON
B.XML
C.BSON
D.All of the Above

48. Which of the following types of routing supported in Web API?

```
A. Attribute Routing
B.Convention-based Routing
C.All of the above
D.None of the above
```

49. Which interface must be implemented  to provide querying facility in LINQ?

```
A.IEnumerator or IQueryable
B.IEnumberable or Queryable
C.Enumberatble or Qurable
D.None of the above
```

50. What is the default injection type of Unit Container used in ASP.NET Core

```
A.Constructor Injection
B.Prperty Injection
C.Method Injection
D.All of the Above
```


<hr/>
Answers:
1. C
2. A
3. B
4. C
5. D
6. C
7. B
8. B
9. B
10. C
11. C
12. C
13. C
14. B
15. B
16. A
17. C
18. A
19. A
20. A
21. A
22. A
23. D
24. B
25. A
26. D
27. B
26. D
27. D
28. C
29. B
30. B
31. B
32. D
34. A
36. B
37. C
38. B
39. B
40. A
41. C
42. C
43. C
44. A
45. B
46. B  A
47. C
48. C
49. B
50. A