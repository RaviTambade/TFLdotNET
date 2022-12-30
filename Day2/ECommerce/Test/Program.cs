using Membership;

//Entry Point code for ECommerce Solution

Console.WriteLine( "TFL Store ");
Console.WriteLine("******************************");

Console.WriteLine("Enter your Email ID");
string email=Console.ReadLine();

Console.WriteLine("Enter your password");
string password=Console.ReadLine();

if(AuthManager.Validate(email,password))
{
    Console.WriteLine("Welcome to Transflower Store");
}
else{
     Console.WriteLine("Invalid User");
  
}

Console.WriteLine("Thank you for visiting Transflower");