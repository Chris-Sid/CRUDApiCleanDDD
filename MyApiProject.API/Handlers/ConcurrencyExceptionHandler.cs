using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyApiProject.Application.Exceptions;

namespace MyApiProject.API.Handlers
{
    public class ConcurrencyExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ConcurrencyExceptionHandler> _logger;

        public ConcurrencyExceptionHandler(ILogger<ConcurrencyExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ConcurrencyConflictException ex)
                return false;

            _logger.LogWarning(ex, "Concurrency conflict on {Entity}", ex.EntityName);

            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Concurrency conflict",
                Detail = ex.Message,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.10",
                Instance = httpContext.Request.Path
            };
            problem.Extensions["current"] = ex.CurrentValues;

            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
            return true;
        }
    }
}
