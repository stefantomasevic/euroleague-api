using Euroleague.Authorization;
using Euroleague.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Euroleague.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthorizationRepository authorizationRepository, IConfiguration configuration)
        {
            _authorizationRepository = authorizationRepository;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            try
            {
                var isRegistered = await _authorizationRepository.RegisterUserAsync(model);

                if (isRegistered)
                {
                    return Ok(new { Message = "Registration successful" });
                }
                else
                {
                    return BadRequest(new { Message = "Username already exists" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during registration", Error = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            try
            {
                var user = await _authorizationRepository.ValidateUserAsync(model);

                if (user != null)
                {
                    var token = GenerateJwtToken(user);

                    return Ok(new { Token = token, Message = "Login successful" });
                   
                }
                else
                {
                    return BadRequest(new { Message = "Invalid username or password" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during login", Error = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("is-admin")]
        public IActionResult IsAdmin()
        {
            // U ovoj akciji proveravamo da li je korisnik administrator
            var isAdmin = User.IsInRole("Admin");
            return Ok(new { IsAdmin = isAdmin });
        }



        private string GenerateJwtToken(Admin user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username))
            {
                // Ako korisnik nije validan ili nema postavljen Username
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token expiration time
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
