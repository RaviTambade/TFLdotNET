using System.Collections.Generic;
using Banking;
using Maths;

//Variables are declared using types
//CTS :Common Type System

//Value types:
            //primitive data types
            //int, float, double, bool, struct
            //enum
            //char
//Reference types
            //class
            //interface
            //delegate
            //event

//Collection Framework
            // namespaces
            // System.Collections.Generic
//bool status=false;
//Boolean flag=true;
//annonymous type

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

Account acc123=new Account(60000);
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