namespace RESTAPI.Models;
using System.ComponentModel.DataAnnotations;

public class User{
    [Required]
    public string UserName{get;set;}
    [Required]
    public string Password{get;set;}
    public string ContactNumber{get;set;}
    public string Email{get;set;}


    public override string ToString()
    {
        return UserName +  " "+ Password + "  "+ Email;
    }





}