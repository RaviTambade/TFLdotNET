using System.Collections.Generic;
using WareHouse;
using TFLCollection;

/*
List<Object> allObjects=new List<Object>();
allObjects.Add(12);
allObjects.Add("Ravi Tambade");
allObjects.Add(true);
allObjects.Add(45.6);

List<Part> allParts=new List<Part>();
//allObjects.Add(new { Id=23, Title="Jasmine", Description="Smelling flower"});
allParts.Add(new Part() { PartName = "crank arm", PartId = 1234 });
allParts.Add(new Part() { PartName = "chain ring", PartId = 1334 });
allParts.Add(new Part() { PartName = "regular seat", PartId = 1434 });
allParts.Add(new Part() { PartName = "banana seat", PartId = 1444 });
allParts.Add(new Part() { PartName = "cassette", PartId = 1534 });
allParts.Add(new Part() { PartName = "shift lever", PartId = 1634 });

foreach ( object  o in allObjects){
    Console.WriteLine(o);
}

foreach ( Part  part in allParts){
    Console.WriteLine(part);
}

Stack<Part>  assembly=new Stack<Part>();
Queue<Part> container=new Queue<Part>;
container.Enqueue(new Part() { PartName = "regular seat", PartId = 1434 });

Dictionary<string, Part> todaysSpareParts=new Dictionary<string, Part>();
todaysSpareParts.Add("main",new Part() { PartName = "crank arm", PartId = 1234 } );
todaysSpareParts.Add("top",new Part() { PartName = "shift lever", PartId = 1634 } );


Part thePart=todaysSpareParts["main"];
Console.WriteLine(thePart);

*/


List<Employee> employees=new List<Employee>();
employees.Add(new Employee(1, "Raghu",30000));
employees.Add(new Employee(2, "Rajendra",67000));
employees.Add(new Employee(4, "Sham",20000));

Console.WriteLine("List of Employees before Sort");
foreach(Employee emp in employees){
    Console.WriteLine(emp.id + "--"+ emp.name + "--"+ emp.salary);
}


//Implement logic for Sorting
EmpComparer ec = new EmpComparer();

employees.Sort(ec);
Console.WriteLine("List of Employees after Sort");
foreach(Employee emp in employees){
    Console.WriteLine(emp.id + "--"+ emp.name + "--"+ emp.salary);
}


Player p1= new Player("Sourav Ganguly",50, 300, 50000);
Player p2= new Player("Sachin Tendulkar",49, 350, 100000);
Player p3= new Player("Rahul Dravid",48, 250, 70000);

List<Player> indianPlayers=new List<Player>();
indianPlayers.Add(p1);
indianPlayers.Add(p2);
indianPlayers.Add(p3);

indianPlayers.Sort();

foreach( Player currentPlayer in indianPlayers){
  Console.WriteLine(currentPlayer);
}
