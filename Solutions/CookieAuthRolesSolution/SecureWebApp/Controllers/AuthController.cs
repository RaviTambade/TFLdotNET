using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecureWebApp.Services;
using System.Security.Claims;

namespace SecureWebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _userService.Validate(email,password);
            if (user != null)
            {
                // Create a claims identity
                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, email),
                        new Claim(ClaimTypes.Role, string.Join(",", user.Roles)) // Add roles as a claim
                };
                var claimsIdentity = new ClaimsIdentity(claims, 
                                                        CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in the user
               await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                             claimsPrincipal);
                return RedirectToAction("Welcome", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // Access Denied page
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
