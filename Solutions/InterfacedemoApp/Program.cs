namespace Demo;


public abstract class Person
{
    public string FirstName {get;set;}
    public string LastName{get;set;}

    public abstract void PayTax();

}
public interface IWorker
{
    void ShowDetails();
}

public interface IMentor
{
    void ShowDetails();
}

public class Employee:Person, IWorker,IMentor
{
    private string fullName;
    
    //Normal Property
    public string Name{
      
        set
        {
            if(value.Length != 0)
            {
            this.fullName=value;
            }
            else
            {
            throw new ArgumentException("Name can not be empty");
            }
        }

        get{

            return this.fullName;
        }
    }

    //Auto property

    public double Salary{get;set;}
    void  IWorker.ShowDetails()
    {
        Console.WriteLine("Method of Employee Class ShowDetials is invoked");
    }  

    void  IMentor.ShowDetails()
    {
        Console.WriteLine("Method of Mentor Class ShowDetials is invoked");
    }  


     public override void PayTax()
    {
        Console.WriteLine("Paying Incometax");
    }
}

public class Program{
    public static void Main(string [] args)
    {
       Console.WriteLine("Hello"); 

       Employee emp=new Employee();

       IWorker theMentor=emp;
       theMentor.ShowDetails();

    }
}