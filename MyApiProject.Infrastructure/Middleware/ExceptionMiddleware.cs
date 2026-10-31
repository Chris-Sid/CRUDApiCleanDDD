using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyApiProject.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }       

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // continue pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception. TraceId: {TraceId}", httpContext.TraceIdentifier);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception is ApiException apiEx
                ? apiEx.StatusCode
                : (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problem = new ProblemDetails
            {
                Title = statusCode >= 500 ? "An unexpected error occurred." : "The request could not be processed.",
                Detail = exception is ApiException || _env.IsDevelopment()
                    ? exception.Message
                    : "Contact support and provide the trace id.",
                Status = statusCode,
                Type = $"https://httpstatuses.com/{statusCode}",
                Instance = context.TraceIdentifier
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }

    }
}
