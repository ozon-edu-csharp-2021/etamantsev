using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchandiseService.Infrastructure.MiddlewareRoutes
{
    public class OkResponseMiddleware
    {
        public OkResponseMiddleware(RequestDelegate next)
        {
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync($"{context.Response.StatusCode} Ok");
        }
    }
}