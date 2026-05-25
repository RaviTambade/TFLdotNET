using Banking.AccountManagement.Publishers;
using AccountLowBalanceLogging.Subscribers;


//Main Program
//role of Main program in Event Delegate Model
//Main program is the orchestrator of the event


//Business entity instance
// Before performing any actdion against  Business Entity
// Registger subscribers to events of the business entity (publisher)

var acc = new Account("Abhay Navale", 20000);
// subscribers
var email = new EmailNotificationService();
var logger = new LogService();

// Subscribing to the event (Suscription)

// Delegate objects are created internally and
// address of subriber functions are registered
// to the event

acc.LowBalance += email.SendLowBalanceAlert;  //Attach
acc.LowBalance += logger.LogLowBalance;     //Attach

//  Ready to  Perform business Operations
// Logic for business operations

acc.Withdraw(500);   // Balance = 1500
acc.Withdraw(600);   // Balance = 900 → triggers event
acc.Withdraw(200);   // Balance = 700 → triggers again