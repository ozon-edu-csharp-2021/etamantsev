using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchandiseService.Middleware.Middleware
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.ContentType != "application/grpc")
            {
                await LogResponse(context);
            }
            else
            {
                await _next(context);
            }
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                using var memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;
                await _next(context);
                memoryStream.Position = 0;
                var reader = new StreamReader(memoryStream);
                var responseBody = await reader.ReadToEndAsync();
                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBodyStream);
                _logger.LogInformation(
                    "Logged response {method} {url} with headers [{headers}] => {response}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    $"[{string.Join(", ", context.Request.Headers?.Select(c => c.Key))}]",
                    responseBody);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}