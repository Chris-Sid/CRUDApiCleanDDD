using MyApiProject.Infrastructure.Services.Session;

namespace MyApiProject.API.AuthMiddleware
{
    public class DebugJwtMiddleware
    {
        private readonly RequestDelegate _next; 

        public DebugJwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) //invoke session service to debug in case of exceptions
        {
            var authorizationHeader = context.Request.Headers["X-Authorization"].ToString();

            if (authorizationHeader?.StartsWith("Bearer ") == true)
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                Console.WriteLine($"Received token: {token}");
                var sessionService = context.RequestServices.GetRequiredService<ISessionService>();
                // Check if the token exists in the session store
                var session = sessionService.GetSession(token);

                if (session == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Session expired or not found.");
                    return;
                }
            }

            await _next(context);
        }
    }
}