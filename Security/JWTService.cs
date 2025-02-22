using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PawListBackend.Models;
using PawListBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace PawListBackend.Services
{
    public class JWTService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public JWTService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return new AuthResult { Success = false, Message = "User not found" };
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new AuthResult { Success = false, Message = "Invalid password" };
            }

            // Get the secret key from configuration
            var secret = _configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                throw new Exception("JWT secret is not configured.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);  // Ensure the key is long enough
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AuthResult { Success = true, Token = tokenString, Message = "Login successful" };
        }
        
        // AuthService.cs

        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            // Check if the username already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);
            if (existingUser != null)
            {
                return new AuthResult { Success = false, Message = "User already exists" };
            }

            // Hash the password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create the new user
            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResult { Success = true, Message = "User registered successfully" };
        }

        
    }
}
