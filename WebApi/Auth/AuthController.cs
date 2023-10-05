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
            var user = await _userService.ValidateUser(req.Email, req.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Check for specific email
          /*  if (user.Email.ToLower() != "a@a.com")
            {
                return Unauthorized("This user is not authorized to receive a token.");
            }*/

            var token = GenerateJwtToken(user);

            // Set the JWT as a cookie.
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Ensure it's sent over HTTPS
                Expires = DateTime.UtcNow.AddMinutes(_config.GetSection("Jwt").Get<JwtSettings>().DurationInMinutes)
            };

            Response.Cookies.Append("access_token", token, cookieOptions);

            return Ok(new
            {
                Message = "Authentication successful.",
                User = new
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                    // Add any other relevant properties of the user you'd like to include in the response.
                }
            });
        }

        private string GenerateJwtToken(UserModel user)
        {
            var jwtSettings = _config.GetSection("Jwt").Get<JwtSettings>();

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

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            return Ok(new { Message = "Logout successful." });
        }
    }
}
