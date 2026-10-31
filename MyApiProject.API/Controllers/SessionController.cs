using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MyApiProject.Application.DTOs;
using MyApiProject.Application.Interfaces;
using MyApiProject.Infrastructure.Services.Session;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyApiProject.Application.Exceptions;
using Microsoft.AspNetCore.Http.Features;
using System.Linq;
using MyApiProject.Infrastructure.Exceptions;

namespace MyApiProject.API.Controllers
{
    // SessionController.cs
    // This controller provides an endpoint to retrieve session data for a user.
    // It uses the Authorization header token to look up the associated session data.
    // For simplicity, the session data is mocked and does not involve database queries.
    [ApiController]
    [AllowAnonymous]
    [EnableRateLimiting("login")]
    [Route("api/dms/token")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ILogger<SessionController> _logger;

        public SessionController(ISessionService sessionService, ILogger<SessionController> logger)
        {
            _sessionService = sessionService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetSession([FromBody] TokenRequest body)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            try
            {
                var token = await _sessionService.GetOrCreateSessionAsync(body, TimeSpan.FromMinutes(30));

                _logger.LogInformation("Successful login for {UserName} from {Ip}", Sanitize(body.UserName), ip);
                return Ok(new { accessToken = token, tokenType = "Bearer", expiresInSeconds = 1800 });
            }
            catch (ApiException ex) when (ex.StatusCode == StatusCodes.Status401Unauthorized)
            {
                _logger.LogWarning("Failed login for {UserName} from {Ip}", Sanitize(body?.UserName), ip);
                throw; // ExceptionMiddleware still produces the 401 response
            }
        }

        // Prevents log forging with line breaks and caps the length of attacker-controlled text
        private static string Sanitize(string? value) =>
            string.IsNullOrEmpty(value)
                ? "(empty)"
                : new string(value.Where(c => !char.IsControl(c)).Take(100).ToArray());
    }
}
