using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchandiseService.Infrastructure.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogRequest(context.Request);
            await _next(context);
        }

        private void LogRequest(HttpRequest contextRequest)
        {
            if (contextRequest.ContentType != "application/grpc")
            {
                _logger.LogInformation(
                    "Logged request {method} {url} with headers [{headers}]",
                    contextRequest?.Method,
                    contextRequest?.Path.Value,
                    $"[{string.Join(", ", contextRequest.Headers?.Select(c => c.Key))}]");
            }
        }
    }
}