using System.Collections.Generic;
using WareHouse;
using TFLCollection;
List<Employee> employees=new List<Employee>();
employees.Add(new Employee(1, "Raghu",30000));
employees.Add(new Employee(2, "Rajendra",67000));
employees.Add(new Employee(4, "Sham",20000));
Console.WriteLine("List of Employees before Sort");
foreach(Employee emp in employees){
    Console.WriteLine(emp.id + "--"+ emp.name + "--"+ emp.salary);
}
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
