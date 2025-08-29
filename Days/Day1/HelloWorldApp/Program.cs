using System.Collections.Generic;
using Banking;
using Maths;

//.NET:  Ecosystem  =  CLR + CTS + C# language(garmmer) + Reusable Library (vocablury)
// Reusable Library  :  ado.net, asp.net MVC, web api, etc. 

// we would be able build application
// host application
// Exeucte application



// Execution Engine: CLR (Common Lanaguge Runtime)

//Varible declaration:
// Variables are declared using types
//CTS :Common Type System
// Two ways you can define variables:
// 1.Value types:    2.Refernce Types


//1. primitive data types (Value Types)
//int, float, double, bool, struct
//enum
//char


//2.Reference types
//class , interface, delegate ,event



//Collection Framework
// namespaces
// System.Collections.Generic
//bool status=false;
//Boolean flag=true;
//annonymous type


int result = 56;
char ch = 'T';
boolean status = false;


//Annonyous type

var p=new {
                FirstName="Satish",
                LastName="Dhavan"
};

Console.WriteLine(p.FirstName  + "  "+ p.LastName);
int count=12;
string company="Transflower";
//var  trainer="Ravi Tambade";

count ++;
Console.WriteLine("Count = "+ count);
Console.WriteLine(company);
Console.WriteLine("Hello, World!");

Account acc123=new Account(60000);  //User defiend type
acc123.Deposit(15000);
float  currentBalance123=acc123.GetBalance();
 
Account acc124=new Account(20000);
acc124.Deposit(8000);
float  currentBalance124=acc124.GetBalance();
 
//Collection 

List<Account> accounts=new List<Account>();
accounts.Add(acc123);
accounts.Add(acc124);

foreach( Account theAccount in accounts){
  float result=theAccount.GetBalance();
  Console.WriteLine("Current Balance={0}",result);
}

Complex  c1=new Complex(34,56);
Complex c2=new Complex(11,78);
Complex c3=c1 + c2;