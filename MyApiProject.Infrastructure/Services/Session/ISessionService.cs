using MyApiProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Services.Session
{
    public interface ISessionService
    {
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
        /// <summary>
        /// Gets the cached value by key. If not present, creates it using the factory and caches it.
        /// </summary>
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> valueFactory, TimeSpan? expiration = null);
        Task<string?> GetOrCreateSessionAsync(TokenRequest body, TimeSpan timeSpan);
    }
}
