using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.dto;
using WebApi.User;

namespace WebApi.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            // 1. Check if user exists and password is correct using _userService.
            var user = await _userService.ValidateUser(req.Email, req.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            // 2. If user is valid, generate a JWT and return it.
            var token = GenerateJwtToken(user); // This method will generate the JWT.
            return Ok(new { token });
        }

        private string GenerateJwtToken(UserModel user)
        {
            var jwtSettings = _config.GetSection("Jwt").Get<JwtSettings>(); // Assuming you have a JwtSettings class

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("UserId", user.UserId.ToString()),
             new Claim(ClaimTypes.Email, user.Email)
            // Add more claims if needed
        }),
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                SigningCredentials = credentials,
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
