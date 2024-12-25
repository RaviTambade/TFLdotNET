 
 using CRM;
using LMS;
using Sports;

using DAO;
using System.Collections;
UtilityManager.ViewNames("Alice", "Bob", "Charlie");
 Console.WriteLine("\n");
 UtilityManager.ViewNames(12, 45, 90, 98);
 Console.WriteLine("\n");
 UtilityManager.ViewNames(true, false);
 var person1= new {
        Name="Alice",
        Age=25
 };
 var person2= new {
        Name="Bob",
        Age=30
 }; 

  UtilityManager.ViewNames(person1, person2);
  Console.WriteLine("\n");


  UtilityManager.ViewNames(45, false, new { Name="Charlie", Age=35 }, "Transflower");

    string  first = "Sameer Patil";
    string second = "Rajan Nene";

    UtilityManager mgr= new UtilityManager();
    mgr.Swap(ref first, ref second);
    Console.WriteLine($"First: {first}, Second: {second}");



    int radius = 5;
    double area, circumference;
    //Console.WriteLine($"Area: {area}, Circumference: {circumference}");

    mgr.Calculate(radius, out area, out circumference);
    Console.WriteLine($"Area: {area}, Circumference: {circumference}");



    Customer customer = new Customer();
    customer.Name = "Alice";
    customer.Address = "123, Baker Street";
    customer.Email = "alice.brown@transflower.in";


    Object obj = customer;

    Customer customer2 = (Customer)obj;  // epxlicit casting

    Customer customer3= obj as Customer; // safe casting

    Console.WriteLine($"Name: {customer3.Name}, Address: {customer3.Address}, Email: {customer3.Email}");
    
    if(customer3 is Customer){
        Console.WriteLine("customer3 is a Customer");
    }

    // RTTI (Run Time Type Information)
    // C #, Java : Reflection
    //Reflection: gettting metadata of a type at runtime
    //System.Reflection namespace
    //Type class, MethodInfo, PropertyInfo, FieldInfo, 
    //ConstructorInfo, EventInfo, Assembly, Module, ParameterInfo, MemberInfo
    //Using Reflecdtion we can perform task such as reverse  engineering
    Type t= customer3.GetType();
    Console.WriteLine(t.FullName);
    if(t.Name=="Customer"){
        Console.WriteLine("t is a Customer");
    }
    Library library = new Library();
    library.AddBook(new Book{Title="C# Programming", Author="Ravi Tambade",
                              Publisher="Transflower", Pages=450});
    library.AddBook(new Book{Title="Java Programming", Author="Sandip Kulange",
                              Publisher="Sunbeam", Pages=500});

    library.AddBook(new Book{Title="databade Programming", Author="Sameer D",
                              Publisher="IBM", Pages=350});
    //your object is behaving like an array
    //Indexer is called as smart array
    Book book=library[0];
    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Publisher: {book.Publisher}, Pages: {book.Pages}");
    Console.WriteLine("Hello, World!");

    //Property Initializer syntax
    //Object Initializer syntax

    library[0]=new Book{Title="Python Programming",
                        Author="Nilesh Ghule",
                        Publisher="Sunbeam",
                        Pages=450};

    book=library[0];
    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, Publisher: {book.Publisher}, Pages: {book.Pages}");
    

    CustomerProfile customerProfile = new CustomerProfile();
    //customerProfile.ShowDetails();   //not allowed


    //Explicit Interface Inheritance
    ICustomerDetails customerDetails = customerProfile;
    customerDetails.ShowDetails();

    IOrderDetails orderDetails = customerProfile;
    orderDetails.ShowDetails();

   // Comparion logic 

   List<Player> players = new List<Player>{
       new Player{ Name="Sachin", Score=100000 , Age=52, Rank=1},
       new Player{ Name="Virat", Score=6700, Age=35, Rank=6},
       new Player{ Name="Rahul", Score=79990, Age=45, Rank=8},
       new Player{ Name="Dhoni", Score=780000, Age=39, Rank=2},
       new Player{ Name="Rohit", Score=987777, Age=37, Rank=4}
   };

    //players.Sort();
    players.Sort(new SelectionCommityManager());
    foreach(Player player in players){
        Console.WriteLine($"Name: {player.Name}, Score: {player.Score}, Age: {player.Age}, Rank: {player.Rank}");
    }

    //Nullable types
    int ? i;
    i=null;

    Nullable<int> j=null;
    j=null;
    j=67000;


   using( DBManager dbManager = new DBManager()){

    dbManager.GetData("SELECT * FROM EMPLOYEES");
    dbManager.GetData("SELECT * FROM DEPARTMENTS");
    dbManager.SaveData("INSERT INTO EMPLOYEES VALUES(101, 'Alice', 45000)");
  
    Dictionary<string, string> data = new Dictionary<string, string>{
        {"Name", "Bob"},
        {"Age", "35"},
        {"Salary", "67000"}
    };


    /* ArrayList list = new ArrayList();
     * */
    //namespace System.Collections.Generic

    List<int> list = new List<int>();
    list.Add(45);  
    list.Add(67);  
    list.Add(89);  
    int number1=list[0]; 
}
    