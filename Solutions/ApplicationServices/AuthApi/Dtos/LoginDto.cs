namespace AuthApi.Models;
 
public class LoginDto
{
    public string UserName { get; set; }

    public string Password { get; set; }
}

public class ChangePasswordDto
{
    public string UserName { get; set; }

    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}

