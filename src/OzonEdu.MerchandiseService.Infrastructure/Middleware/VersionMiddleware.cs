using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchandiseService.Infrastructure.Middleware
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version";
            var response = JsonSerializer.Serialize(new { Version = version, ServiceName = "MerchandiseService" });
            await context.Response.WriteAsync(response);
        }
    }
}