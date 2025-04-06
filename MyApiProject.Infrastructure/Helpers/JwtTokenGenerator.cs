using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MyApiProject.Application.Interfaces;

namespace MyApiProject.Infrastructure.Helpers
{
    public class JwtTokenGenerator: IJwtTokenGenerator
    {
        public string GenerateJwtToken(string username)
        {
            // Define the security key and signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("onedealerTokenKeyForDummyTest2025!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define the claims for the token
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        // Add additional claims as needed
    };

            // Create the token
            var token = new JwtSecurityToken(
                issuer: "OneDealer.gr",
                audience: "test",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            // Return the serialized token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
