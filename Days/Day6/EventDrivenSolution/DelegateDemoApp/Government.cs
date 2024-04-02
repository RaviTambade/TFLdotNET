namespace EGovernance;


//delegate type declaration
//it is a wrapper for function pointer

//Tax Services
public delegate  void TaxOperation(double amount);


public class CentralGov {

    //Tax business Logic
    //isolated, decouple
    //automic
    //durable
    //acid
    public void DeductIncomeTax(double amount){
        Console.WriteLine("45% income tax is deducted from your account");

    }
    public void DeductServiceTax(double amount){
        Console.WriteLine("18% income tax is deducted from your account"); 
    }
    public void DeductProfessionalTax(double amount){
        Console.WriteLine("10% income tax is deducted from your account");
  
    }
}