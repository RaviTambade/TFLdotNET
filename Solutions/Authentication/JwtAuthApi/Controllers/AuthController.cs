using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthApi.Dtos;
namespace JwtAuthApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration configuration,ILogger<AuthController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    // -------------------------------------------------------
    // REGISTER
    // POST: api/auth/register
    // -------------------------------------------------------
    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        _logger.LogInformation( "Register method called for User: {UserName}",dto.UserName);

        // Dummy registration logic

        return Ok(new
        {
            Message = "User registered successfully"
        });
    }

    // -------------------------------------------------------
    // LOGIN
    // POST: api/auth/login
    // -------------------------------------------------------
    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        _logger.LogInformation("Login method called for User: {UserName}",dto.UserName);

        // Dummy user validation
        if (dto.UserName != "admin" || dto.Password != "seed123")
        {
            return Unauthorized(new
            {
                Message = "Invalid username or password"
            });
        }

        // Generate JWT Token
        var token = GenerateJwtToken(dto.UserName);

        return Ok(new
        {
            Token = token
        });
    }

    // -------------------------------------------------------
    // GENERATE JWT TOKEN
    // -------------------------------------------------------
    private string GenerateJwtToken(string userName)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}