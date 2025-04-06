using Microsoft.Extensions.Caching.Memory;
using MyApiProject.Application.DTOs;
using MyApiProject.Application.Interfaces;
using MyApiProject.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Services.Session
{
    public class SessionService : ISessionService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public SessionService(IMemoryCache memoryCache, IJwtTokenGenerator jwtTokenGenerator)
        {
            _memoryCache = memoryCache;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public string CreateSession(TokenRequest request, TimeSpan duration)
        {
            var cacheKey = GetCacheKey(request.UserName);

            // Check if token already exists
            if (_memoryCache.TryGetValue(cacheKey, out string existingToken))
            {
                return existingToken;
            }

            // Generate and store new token
            var newToken = _jwtTokenGenerator.GenerateJwtToken(request.UserName);
            _memoryCache.Set(cacheKey, newToken, duration);
            _memoryCache.Set(newToken, request, duration); // Store session data by token as well

            return newToken;
        }

        public void SaveSession(string token, TokenRequest session, TimeSpan expiration)
        {
            // Store the session with an expiration time
            _memoryCache.Set(token, session, expiration);
        }

        public TokenRequest GetSession(string token)
        {
            // Try to retrieve the session data
            _memoryCache.TryGetValue(token, out TokenRequest session);
            return session;
        }

        public void DeleteSession(string token)
        {
            // Remove the session from cache
            _memoryCache.Remove(token);
        }

        private static string GetCacheKey(string username)
        {
            return $"session_{username}";
        }
    }
}
