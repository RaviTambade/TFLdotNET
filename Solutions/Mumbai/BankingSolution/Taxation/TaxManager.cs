namespace Taxation;

public delegate void  TaxOperation();
public delegate void  TaxOperationWithParam(float tax);
public delegate float  TaxCalculation();
public class TaxManager
{
    
    //  Event Handlers
    public void PayIncomeTax( )
    {
        Console.WriteLine("Paying Income tax...   30%");
    }

    public void PayServiceTax()
    {
        Console.WriteLine("Paying Service tax...");
    }

    public void PayGSTTax()
    {
        Console.WriteLine("Paying GST tax...");
    }


    public float CalculateIncomeTax()
    {
        return 1000;
    }
}



// Under stand  Customers Business Operations and their requirements
// Analyze existing Systems and learn Automation needed
// Create Design using Design Principles
// Follow Object Oriented Approach
// Identify Business Entities
// Identify Business Rules
// Identify Delegates
// Identify Events
// Create reusable modules with  Entities, Classes, Interfaces, delegate and events
// Integrate all logi using Layered Architecture
// Build Solution using loosely Coupled Highly Cohesive Manner

//summary of above code 
// 1. TaxManager class has state and behaviour
// 2. TaxManager class has events and event handlers
// 3. TaxManager class has delegates
// 4. TaxManager class has methods: PayIncomeTax, PayServiceTax, PayGSTTax, CalculateIncomeTax
// 5. The methods are called when the respective events are raised
 ax
