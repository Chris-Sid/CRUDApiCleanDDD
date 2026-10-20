using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApiProject.API.ExceptionHandling
{
    public class ConcurrencyExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not DbUpdateConcurrencyException)
                return false;

            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Concurrency conflict",
                Detail = "The lead was modified or deleted by another user. Reload it and try again."
            }, cancellationToken);

            return true;
        }
    }
}