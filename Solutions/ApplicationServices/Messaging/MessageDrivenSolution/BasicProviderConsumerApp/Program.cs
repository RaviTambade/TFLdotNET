
using BasicProviderConsumerApp;

List<Account> accounts = new List<Account>();
accounts.Add(new Account(10000));
accounts.Add(new Account(20000));
accounts.Add(new Account(30000));
accounts.Add(new Account(57000));
//more than 56000

 
BankEventReciver bankEventReciver = new BankEventReciver();

foreach (var acc in accounts)
{
    acc.lowBalance += bankEventReciver.ApplyTax; //subscribe to the event
}




//Bulk operation
//simulating withdrawal against all accounts

foreach (var account in accounts)
{
    account.withdraw(49000);
}

