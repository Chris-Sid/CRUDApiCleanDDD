using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MyApiProject.API.AuthMiddleware;
using MyApiProject.Application.DTOs;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using MyApiProject.Infrastructure.Services.Session;
using System.ComponentModel;
using MyApiProject.Application.Interfaces;

namespace MyApiProject.API.Controllers
{
    // SessionController.cs
    // This controller provides an endpoint to retrieve session data for a user.
    // It uses the Authorization header token to look up the associated session data.
    // For simplicity, the session data is mocked and does not involve database queries.
    [ApiController]
    [Route("api/dms/token")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly HttpClient _httpClient;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public SessionController(ISessionService sessionService, IHttpClientFactory httpClientFactory, IJwtTokenGenerator jwtTokenGenerator)
        {
            _sessionService = sessionService;
            _httpClient = httpClientFactory.CreateClient();
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> GetSession([DefaultValue("en-US")] string accept_Language,[FromBody] TokenRequest body, CancellationToken cancellationToken)
        {
            var token = _sessionService.CreateSession(body, TimeSpan.FromMinutes(30));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            // Return the token response
            return Ok($"Successfully logged in. Use this token in Authorization header: {token}");
        }
    }
}
