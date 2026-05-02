namespace  Membership;
public  class AccountManager{
    public static bool  Login(string email, string password){
        bool status=false;
        if(email=="sachin.jadhav@transflower.in" && password=="432"){
            status=true;
        }
        return status;
    }
}