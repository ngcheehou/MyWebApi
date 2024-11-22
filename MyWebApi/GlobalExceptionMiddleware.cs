using Azure;
using Microsoft.AspNetCore.Http;

namespace MyWebApi
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
                _logger.LogError(ex, "An unhandled exception occurred."+ ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                MyCustomException => 999,
                ApplicationException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,

                _ => StatusCodes.Status500InternalServerError
            };

          

            var response = new APIResponse<object>
            {
                Success = false,
                Message = exception.Message,
                Data = null,
                Errors = context.Response.StatusCode == StatusCodes.Status500InternalServerError
         ? new { detail = "An unexpected error occurred. Please try again later." }
         : null
            };

            var task = context.Response.WriteAsJsonAsync(response);
            context.Response.Body.Flush(); // Ensure the response body is sent
            return task;
        }


    }

}
