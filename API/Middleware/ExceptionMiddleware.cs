using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        // RequestDelegate: the function that can process an HTTP request
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        // Middleware method: InvokeAsync
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // if no exception occures, the request moves on to the next middleware
                await _next(context);
            }
            catch(Exception ex)
            {
                // log the exception message to console
                _logger.LogError(ex, ex.Message);
                // send back the exception to client in json format with statusCode equals to 500
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // if the application runs in developer mode, send error message in details.
                // if it's in production mode, simply send internal server error code 500 back.
                var response = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options); // response in JSON format

                await context.Response.WriteAsync(json);
            }   
        }
    }
}