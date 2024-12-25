
namespace  Utility{

public sealed class HRManager{
    public void AddEmployee(){
        Console.WriteLine("Employee added successfully");
    }
    public void UpdateEmployee(){
        Console.WriteLine("Employee updated successfully");
    }
    public void DeleteEmployee(){
        Console.WriteLine("Employee deleted successfully");
    }
}
public sealed class MathEngine{
    public int Add(int a, int b){
        return a + b;
    }
    public int Subtract(int a, int b){
        return a - b;
    }
   
}




public static class UtilityManager{
    //extension Methods
    public static int Multiply( this MathEngine  m,int a, int b){
        return a * b;
    }
   
    public static void ProcessSalary(this HRManager hr){
        Console.WriteLine("Salary processed successfully");
    }   
}

}