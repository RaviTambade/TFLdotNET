using HR;
using catalog;
using Library;
using Training;
using TFLCollection;
const string institute="Transflower";
const int result=56;
double area=0;
double circumference=0;
Console.WriteLine( "area ={0}, circumference ={1}", area,circumference);
Console.WriteLine( "area ={0}, circumference ={1}", area,circumference);
int [] numbers={34,65,76,23,45};
string [] names={"Manoj", "Raj", "Rahul", "Simran"};
Product [] products={
                      new Product {Title="Rose",UnitPrice=12},
                      new Product {Title="Jasmine",UnitPrice=2},
                      new Product {Title="Lotus",UnitPrice=32},
};
Books myColletion=new Books();
string title=myColletion[0];
Console.WriteLine("Title= "+ title);
myColletion[3]="Who moved my Cheese";
title=myColletion[3];
Console.WriteLine("Title= "+ title);
Student std1=new Student();
Employee emp=new Employee();
emp.CalculateSalary();
SalesEmployee salesEmp= new SalesEmployee();
salesEmp.CalculateSalary();
Employee e3=salesEmp;
e3.CalculateSalary();
Stack flightsLanded=new Stack(3);
Stack flights=(Stack)flightsLanded.Clone();
Product pp=new Product();
using(Product p1=new Product())
{
    p1.Title="Gerbera";
    p1.UnitPrice=12;
}
using (Product p2= new Product())
{
}
Product p3= new Product();
