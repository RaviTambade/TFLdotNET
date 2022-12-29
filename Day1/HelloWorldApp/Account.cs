namespace Banking;

public class Account {

    //data member
    private float balance=5000;

    //getter setter methods
    public void SetBalance(float amount){
        this.balance=amount;
    }

    public float GetBalance(){
        return this.balance;   
    }

    //Constructor overloading
    public Account(){
        this.balance=10000;
    }

    public Account(float amount){
        this.balance=amount;
    }
    
    /*destructor is always called by Garbage Collector 
      before object is going to be removed from Heap*/

    public void Withdraw(float amount){
        //business logic
        //data validation logic
        if( amount ==0){
            throw new Exception("Amount can not be zero");
        }
        this.balance-=amount;
    }

    public void Deposit(float amount){
        this.balance+=amount;
    }
    ~Account()
    {
        //DeInitialization


    }
}