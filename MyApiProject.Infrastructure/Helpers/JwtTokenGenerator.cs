using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MyApiProject.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Infrastructure.Persistence;

namespace MyApiProject.Infrastructure.Helpers
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly AppDbContext _dbContext;
        public JwtTokenGenerator(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string GenerateJwtToken(string username, string password)
        {
            // Fetch the user from the database
            var admin = _dbContext.Admins.SingleOrDefault(a => a.Username == username);

            // Validate user exists and has Admin role
            if (admin == null || admin.Role != "Admin")
                return null;

            if (!BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                // Wrong password
                return null;
            }

            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "test.gr",
                audience: "test",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
