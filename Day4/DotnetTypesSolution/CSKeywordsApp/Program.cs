using HR;
using catalog;
using Library;
using Training;
using TFLCollection;



const string institute="IACSD";
const int result=56;
/*Person prn=new Person();
prn.FirstName="Sachin";
prn.LastName="Nene";
//prn.Id=765;
//Console.WriteLine(prn.Id + " "+ prn.FirstName + " "+ prn.LastName);
*/

/*prn.Display("CPP");
prn.Display("CPP", "DSA", "Java", "Dotnet","SDM");
prn.Display("WPT", "OS");
prn.Display("Maths", "English", "Hindi");
*/


//caller--------- Main

//callee--------------------calculate
double area=0;
double circumference=0;
Console.WriteLine( "area ={0}, circumference ={1}", area,circumference);
//prn.Calculate(23,out area, out circumference);
Console.WriteLine( "area ={0}, circumference ={1}", area,circumference);



//Array

int [] numbers={34,65,76,23,45};
string [] names={"Manoj", "Raj", "Rahul", "Simran"};

Product [] products={
                      new Product {Title="Rose",UnitPrice=12},
                      new Product {Title="Jasmine",UnitPrice=2},
                      new Product {Title="Lotus",UnitPrice=32},
};

//Rectangular Array

/*int [2,2] matrix={
                    {12,34},            
                    {56,23}

};
*/

//int result=matrix[1,1];

//Array of Array
//Jagged Array
/*int [] [] studentMarks=new int[2];
studentMarks[0]=new int[] {67,45,34,65,45,34};
studentMarks[1]=new int[3];
int marksofAStudent=studentMarks[0][2];
*/

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
//Stack planes=flightsLanded;
//to Achieve deep copy

Stack flights=(Stack)flightsLanded.Clone();
//flights.sArr[3]=453;
//flightsLanded.sArr[3]=675;
//flights[3]=564

//

using(Product p1=new Product())
{
    p1.Title="Gerbera";
    p1.UnitPrice=12;
    //Do other stuff

}

using (Product p2= new Product())
{



}

Product p3= new Product();


