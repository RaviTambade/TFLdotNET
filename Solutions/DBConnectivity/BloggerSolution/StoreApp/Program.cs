using AuthLib;

//Entry Point: Main
Console.WriteLine("Welcome to Vijay Sales Store");

string message = "";
AuthManager mgr = new AuthManager();
bool status = mgr.Login("sachin", "ravi");
    if (status)
    {
        message = "User is  trusted User";
    }
    else
    {
        message = "Invalid User";
    }

Console.WriteLine(message);
Console.ReadLine();