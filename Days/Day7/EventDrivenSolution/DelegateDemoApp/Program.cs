using EGovernance;
using Banking;
using System.Threading;

Thread primaryThread=Thread.CurrentThread;
Console.WriteLine("Thread ID=" +primaryThread.ManagedThreadId);

CentralGov bjpGovt=new CentralGov();
TaxOperation itOperation=new TaxOperation(bjpGovt.DeductIncomeTax);
TaxOperation proOperation=new TaxOperation(bjpGovt.DeductProfessionalTax);
//itOperation(5000);
//proOperation(1000);

/*itOperation.Invoke(5000);
proOperation.Invoke(1000);
*/

//Not supported.

/*IAsyncResult iResult=itOperation.BeginInvoke(5000,null,null);
if(iResult.IsCompleted)
{
    Console.WriteLine(" Task is completed....");
}
//proOperation.BeginInvoke(1000,null, null);
*/

/*Account  acct123=new Account(6000);
acct123.overBalance+=itOperation;
acct123.overBalance+=proOperation;

Console.WriteLine("Enter Amount to be deposited");
double amount=double.Parse(Console.ReadLine());
acct123.Deposit(amount);
Console.WriteLine("Before Tax processing");
Console.WriteLine(acct123);
//performing action
acct123.ProcessTax();
Console.WriteLine("After Tax processing");
Console.WriteLine(acct123);


*/
