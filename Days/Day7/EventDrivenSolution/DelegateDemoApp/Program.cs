using EGovernance;
using Banking;
using System.Threading;
Thread primaryThread=Thread.CurrentThread;
Console.WriteLine("Thread ID=" +primaryThread.ManagedThreadId);
CentralGov bjpGovt=new CentralGov();
TaxOperation itOperation=new TaxOperation(bjpGovt.DeductIncomeTax);
TaxOperation proOperation=new TaxOperation(bjpGovt.DeductProfessionalTax);
