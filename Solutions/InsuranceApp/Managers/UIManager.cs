using System.Net;

namespace MaxNewYorkInsurance.Managers;

public class UIManager
{
    
    public void ShowMenu()
    {
        
        /*
        ====================================
         Management System
        ====================================
        1. Purchase Policy
        2. Pay Premium
        3. Register Claim
        4. Renew Policy
        5. Exit
        Enter Choice:

        */
    }

    public int GetChoice()
    {
       int choice= int.Parse(Console.ReadLine());
       return choice;
    }
}