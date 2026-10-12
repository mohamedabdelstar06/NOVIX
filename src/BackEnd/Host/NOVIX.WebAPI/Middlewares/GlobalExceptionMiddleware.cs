using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace NOVIX.WebAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Internal Server Error from the custom middleware.";

            switch (exception)
            {
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "You are not authorized to access this resource.";
                    break;
                case ArgumentException:
                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = "The requested resource was not found.";
                    break;
                default:
                    // Keep the default 500
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Detailed = exception.Message // In production, hide this!
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
