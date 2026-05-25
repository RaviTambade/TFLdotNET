using Microsoft.AspNetCore.Mvc;
using AuthApi.Models;
namespace AuthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    // -------------------------------------------------------
    // LOGIN
    // POST: api/auth/login
    // -------------------------------------------------------
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        _logger.LogInformation(
            "Login method called at {Time} for User: {UserName}",
            DateTime.Now,
            dto.UserName);

        // Dummy validation
        if (dto.UserName == "admin" &&
            dto.Password == "admin123")
        {
            _logger.LogInformation(
                "User {UserName} logged in successfully.",
                dto.UserName);

            return Ok(new
            {
                Message = "Login successful"
            });
        }

        _logger.LogWarning(
            "Invalid login attempt for User: {UserName}",
            dto.UserName);

        return Unauthorized(new
        {
            Message = "Invalid username or password"
        });
    }

    // -------------------------------------------------------
    // REGISTER
    // POST: api/auth/register
    // -------------------------------------------------------
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        _logger.LogInformation(
            "Register method called at {Time} for User: {UserName}",
            DateTime.Now,
            dto.UserName);

        // Dummy registration logic
        _logger.LogInformation(
            "User {UserName} registered successfully.",
            dto.UserName);

        return Ok(new
        {
            Message = "Registration successful"
        });
    }

    // -------------------------------------------------------
    // CHANGE PASSWORD
    // POST: api/auth/changepassword
    // -------------------------------------------------------
    [HttpPost("changepassword")]
    public IActionResult ChangePassword(
        [FromBody] ChangePasswordDto dto)
    {
        _logger.LogInformation(
            "ChangePassword method called at {Time} for User: {UserName}",
            DateTime.Now,
            dto.UserName);

        // Dummy password validation
        if (dto.NewPassword.Length < 6)
        {
            _logger.LogWarning(
                "Weak password attempt for User: {UserName}",
                dto.UserName);

            return BadRequest(new
            {
                Message = "Password must be at least 6 characters"
            });
        }

        _logger.LogInformation(
            "Password changed successfully for User: {UserName}",
            dto.UserName);

        return Ok(new
        {
            Message = "Password changed successfully"
        });
    }

    // -------------------------------------------------------
    // FORGOT PASSWORD
    // POST: api/auth/forgotpassword
    // -------------------------------------------------------
    [HttpPost("forgotpassword")]
    public IActionResult ForgotPassword(
        [FromBody] ForgotPasswordDto dto)
    {
        _logger.LogInformation(
            "ForgotPassword method called at {Time} for Email: {Email}",
            DateTime.Now,
            dto.Email);

        // Dummy email sending logic
        _logger.LogInformation(
            "Password reset link sent to Email: {Email}",
            dto.Email);

        return Ok(new
        {
            Message = "Password reset link sent"
        });
    }
}