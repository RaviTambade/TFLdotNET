 
using Banking;


 Account acct543=new Account(67000);
 Handler theHandler=new Handler();
 acct543.trigger+=theHandler.onDeposit;
 
 acct543.Deposit(7000);
