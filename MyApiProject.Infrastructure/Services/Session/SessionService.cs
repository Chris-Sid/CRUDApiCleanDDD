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

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _redisDb.StringSetAsync(key, json, expiration);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _redisDb.StringGetAsync(key);
            if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            await _redisDb.KeyDeleteAsync(key);
        }

        /// <summary>
        /// Gets the cached value if it exists, otherwise creates it using the factory and stores it.
        /// </summary>
        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> valueFactory, TimeSpan? expiration = null)
        {
            var existing = await GetAsync<T>(key);
            if (existing != null)
                return existing;

            var newValue = await valueFactory();
            await SetAsync(key, newValue, expiration);
            return newValue;
        }

        public async Task<string> GetOrCreateSessionAsync(TokenRequest request, TimeSpan expiration)
        {
            var cacheKey = GetCacheKey(request.UserName);

            var existingToken = await _redisDb.StringGetAsync(cacheKey);
            if (!existingToken.IsNullOrEmpty)
            {
                return existingToken!;
            }

            var newToken = _jwtTokenGenerator.GenerateJwtToken(request.UserName);

            var sessionJson = JsonSerializer.Serialize(request);
            await _redisDb.StringSetAsync(cacheKey, newToken, expiration);
            await _redisDb.StringSetAsync(newToken, sessionJson, expiration);

            return newToken;
        }
        private static string GetCacheKey(string userName) => $"session:{userName}";

    }
}
