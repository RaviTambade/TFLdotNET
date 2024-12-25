using  Taxation;
using AcctMgmt;
using  Notification;

 // create the objects
Account account = new Account();
account.Transfer("123", 1000);
bool status = false;

account.Transfer("876", 7000, out status);
Console.WriteLine("Transfer status: " + status);


TaxManager taxManager = new TaxManager();
NotificationManager notificationManager = new NotificationManager();

//subscribe to the events
account.AccountOpened += notificationManager.SendEmailNotification;
account.AccountClosed += notificationManager.SendSMSNotification;
account.OverBalance += taxManager.PayServiceTax;
//account.OverBalance+=taxManager.PayGSTTax;
//account.OverBalance += taxManager.PayIncomeTax;

account.UnderBalance += notificationManager.SendWhatsAppNotification;

//open the account
account.OpenAccount("Raj", 100000);
account.Deposit(200000);

//summary of above code
// 1. Create the objects of Account, TaxManager and NotificationManager
// 2. Subscribe to the events of Account class
// 3. Open the account and deposit some amount
// 4. The events will be raised and the respective event handlers will be called
// 5. The output will be displayed on the console
