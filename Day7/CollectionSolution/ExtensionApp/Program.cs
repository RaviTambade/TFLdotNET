using Mathematics;
using Training;
using System.Collections.Generic;


MathEngine engine=new MathEngine();
int result=engine.Addition(45,2);
result=engine.Subtraction(45,2);
result=engine.Multiply(45,2);
Console.WriteLine( "Result ="+ result);

 int[] factorsOf300 = { 2, 2, 3, 5, 5 };
 int factorsCount=factorsOf300.Distinct().Count();
 Console.WriteLine( "Total numbers ="+ factorsCount);
 
 string [] names={"Ravi", "Sameer", "Rahul", "Manoj","Ravi", "Sarang", "Sameer"};
 int namesCount=names.Distinct().Count();
 Console.WriteLine( "Total names ="+ namesCount);





    int[] marks = { 56, 54, 34, 76, 87, 98, 56, 89 };

    var firstFourNumbers = marks.Skip(4);
    foreach( int number in firstFourNumbers)
    {
        Console.WriteLine(number);
    }

    string[] students = { "Raj", "Simran", "Amit", "Seeta", "Amarjeet", "Rojer", "Smith" };
    var selectedStudents= from student in students where student.Contains('a')  select student;

    foreach ( string student in selectedStudents)
    {
        Console.WriteLine(student);
    }



    //**************************************************

    List<Student> dacStudents = new List<Student>();
    dacStudents.Add(new Student { StudentID = 1, Name = "Manisha Shinde", Age = 65});
    dacStudents.Add(new Student { StudentID = 2, Name = "Chitra Patil", Age = 35 });
    dacStudents.Add(new Student { StudentID = 3, Name = "Raveena Sharma", Age = 21 });
    dacStudents.Add(new Student { StudentID = 4, Name = "Sameera Deo", Age = 40 });
    dacStudents.Add(new Student { StudentID = 5, Name = "Raj Kale", Age = 38 });
    dacStudents.Add(new Student { StudentID = 6, Name = "Ganesh Mogarkar", Age = 25 });
    dacStudents.Add(new Student { StudentID = 7, Name = "Sameer Kulkarni", Age = 18 });

    //Business Rule
    Console.WriteLine("After Filter applying , Results shown");
    var energeticStudents = from student in dacStudents 
                            where student.Age> 20  && student.Age <45
                            select student;

    foreach( Student student in energeticStudents )
    {
        Console.WriteLine(student.Name);
    }
