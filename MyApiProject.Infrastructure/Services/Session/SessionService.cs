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
using Microsoft.AspNetCore.Http;
using MyApiProject.Infrastructure.Exceptions;
using System.Reflection.PortableExecutable;

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
            if (string.IsNullOrWhiteSpace(request?.UserName) || string.IsNullOrWhiteSpace(request?.Password))
                throw new ApiException("Invalid credentials.", StatusCodes.Status400BadRequest, null, null, null);

            // 1. Always validate credentials first. GenerateJwtToken verifies the BCrypt hash.
            var newToken = _jwtTokenGenerator.GenerateJwtToken(request.UserName, request.Password);
            if (newToken is null)
                throw new ApiException("Invalid credentials.", StatusCodes.Status401Unauthorized, null, null, null);

            // 2. Cache only the token (no password). Reuse is safe because credentials were just verified.
            var cacheKey = GetCacheKey(request.UserName);
            var existingToken = await _redisDb.StringGetAsync(cacheKey);
            if (!existingToken.IsNullOrEmpty)
                return existingToken!;

            await _redisDb.StringSetAsync(cacheKey, newToken, expiration);
            return newToken;
        }
        private static string GetCacheKey(string userName) => $"session:{userName}";

    }
}
