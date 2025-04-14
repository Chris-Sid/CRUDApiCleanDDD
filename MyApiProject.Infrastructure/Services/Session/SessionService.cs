using MyApiProject.Application.DTOs;
using MyApiProject.Application.Interfaces;
using MyApiProject.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;

namespace MyApiProject.Infrastructure.Services.Session
{
    public class SessionService : ISessionService
    {
        private readonly IDatabase _redisDb;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public SessionService(IConnectionMultiplexer redis, IJwtTokenGenerator jwtTokenGenerator)
        {
            _redisDb = redis.GetDatabase();
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public string CreateSession(TokenRequest request, TimeSpan duration)
        {
            var cacheKey = GetCacheKey(request.UserName);

            var existingToken = _redisDb.StringGet(cacheKey);
            if (!existingToken.IsNullOrEmpty)
            {
                return existingToken!;
            }

            var newToken = _jwtTokenGenerator.GenerateJwtToken(request.UserName);
            var sessionJson = JsonSerializer.Serialize(request);

            // Store username → token
            _redisDb.StringSet(cacheKey, newToken, duration);

            // Store token → session
            _redisDb.StringSet(newToken, sessionJson, duration);

            return newToken;
        }

        public void SaveSession(string token, TokenRequest session, TimeSpan expiration)
        {
            var sessionJson = JsonSerializer.Serialize(session);
            _redisDb.StringSet(token, sessionJson, expiration);
        }

        public TokenRequest GetSession(string token)
        {
            var sessionJson = _redisDb.StringGet(token);
            return sessionJson.IsNullOrEmpty
                ? null
                : JsonSerializer.Deserialize<TokenRequest>(sessionJson!);
        }

        public void DeleteSession(string token)
        {
            _redisDb.KeyDelete(token);
        }

        private static string GetCacheKey(string username)
        {
            return $"session_{username}";
        }

    }
}
