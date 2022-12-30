namespace Membership;
public static  class AuthManager
{
    public static bool Validate(string email, string password){
        bool status=false;
        if(email == "ravi.tambade@transflower.in" && password=="seed"){
            status=true;
        }
        return status;
    }

    public static bool Register(string firstName, string lastName, 
                            string email, string location, string contactNumber){
          bool status=false;
          try{
             User theUser=new User();
            theUser.FirstName=firstName;
            theUser.LastName=lastName;
            theUser.Email=email;
            theUser.Location=location;
            theUser.ContactNumber=contactNumber;
            //Store object into persistent medium
            status=true;

          }
          catch(Exception e){
            // Exception handling Code


          }
          finally{
          }
         return status;
    }
}
