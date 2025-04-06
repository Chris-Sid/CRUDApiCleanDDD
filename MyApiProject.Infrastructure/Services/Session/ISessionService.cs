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
        string CreateSession(TokenRequest request, TimeSpan duration);
        TokenRequest? GetSession(string token);
        void SaveSession(string token, TokenRequest request, TimeSpan duration);
        void DeleteSession(string token);
    }
}
