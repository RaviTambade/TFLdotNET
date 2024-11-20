namespace SecureWebApp.Models
{
    public class User
    {
        public string Email {  get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }

    }
}
