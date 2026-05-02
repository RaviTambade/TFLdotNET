namespace EGovernance;
public delegate  void TaxOperation(double amount);
public class CentralGov {
    public void DeductIncomeTax(double amount){
        Thread primaryThread=Thread.CurrentThread;
        Console.WriteLine("Thread ID=" +primaryThread.ManagedThreadId);
        Console.WriteLine("45% Income tax is deducted from your account");
    }
    public void DeductServiceTax(double amount){
        Thread primaryThread=Thread.CurrentThread;
        Console.WriteLine("Thread ID=" +primaryThread.ManagedThreadId);
        Console.WriteLine("18% Service tax is deducted from your account"); 
    }
    public void DeductProfessionalTax(double amount){
        Thread primaryThread=Thread.CurrentThread;
        Console.WriteLine("Thread ID=" +primaryThread.ManagedThreadId);
        Console.WriteLine("10% Professional tax is deducted from your account");
    }
}
