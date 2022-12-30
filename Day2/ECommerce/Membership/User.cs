namespace Membership;

public class User{
    //Normal Property
    private string firstName;
    public string FirstName {
        get{ return firstName;}
        set{firstName=value;}
    }
    //auto property
    public string LastName{ get;set;}
    public string Email{ get;set;}
    public string ContactNumber{ get;set;}
    public string Location{ get;set;}

}