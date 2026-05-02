namespace HR;
public class Employee{
    public virtual void CalculateSalary(){
            Console.WriteLine("Employee : CalculateSalary");
    }
    public virtual void DoWork(){
    }
}
public class SalesEmployee:Employee{
    public new virtual void CalculateSalary()
    {
        Console.WriteLine("Sales Employee : CalculateSalary"); 
    }
    public override void DoWork()
    {
        base.DoWork();
    }
}
public class SalesManager:SalesEmployee{
    public override void CalculateSalary()
    {
        base.CalculateSalary();
    }
}
