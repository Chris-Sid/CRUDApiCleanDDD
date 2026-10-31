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
using Microsoft.Extensions.Options;
using MyApiProject.Application.DTOs;

namespace MyApiProject.Infrastructure.Helpers
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        // Valid BCrypt hash of a random value, used so unknown users cost the same time as known users
        private static readonly string DummyHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString(), 11);

        private readonly AppDbContext _dbContext;
        private readonly JwtSettings _jwt;

        public JwtTokenGenerator(AppDbContext dbContext, IOptions<JwtSettings> jwtOptions)
        {
            _dbContext = dbContext;
            _jwt = jwtOptions.Value;
        }

        public string? GenerateJwtToken(string username, string password)
        {
            var admin = _dbContext.Admins.SingleOrDefault(a => a.Username == username);

            // Always run a verify so response time doesn't reveal whether the user exists
            var passwordValid = BCrypt.Net.BCrypt.Verify(password, admin?.PasswordHash ?? DummyHash);
            if (admin is null || !passwordValid || admin.Role != "Admin")
                return null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.Username),
                new Claim(ClaimTypes.Role, admin.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
