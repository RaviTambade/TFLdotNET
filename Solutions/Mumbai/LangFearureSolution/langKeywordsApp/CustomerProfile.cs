namespace CRM{
    //in c# you can use multiple interfaces in a class
    //Explicit Interface Inheritnace (Implementation)
    public class CustomerProfile : ICustomerDetails, IOrderDetails{
        
        void ICustomerDetails.ShowDetails(){
            Console.WriteLine("Customer Details");
        }
        void IOrderDetails.ShowDetails(){
            Console.WriteLine("Order Details");
        }
    }
}