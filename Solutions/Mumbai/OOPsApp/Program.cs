using HR;
using Mathematics; 
 int count=78;
 count ++;

Complex c1 = new Complex(2, 3);
Complex c2 = new Complex(12, 73);
Complex c3 = c1 + c2;
Console.WriteLine(c3);
Console.WriteLine(Complex.GetCount());

Employee e1 = new SalesEmployee("Ravi", 23, "Bangalore", 101, "IT", 45000, 100000, 5000);
Console.WriteLine(e1);
//Polymorphism
double package=e1.CalculateSalary();
Console.WriteLine(package);
Console.WriteLine("Hello, World!");
