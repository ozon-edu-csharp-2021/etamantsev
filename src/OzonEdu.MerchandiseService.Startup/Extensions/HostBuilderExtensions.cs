using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.MerchandiseService.Middleware.Filters;
using OzonEdu.MerchandiseService.Middleware.Interceptors;
using OzonEdu.MerchandiseService.Middleware.StartupFilters;

namespace OzonEdu.MerchandiseService.Middleware.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                services.AddSingleton<IStartupFilter, RequestResponseLoggingStartupFilter>();
                services.AddGrpc(options => options.Interceptors.Add<GrpcLoggingInterceptor>());
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "OzonEdu.MerchandiseService", Version = "v1" });
                });
                services.AddControllers(options => options.Filters.Add<AppExceptionFilter>());
            });
            return builder;
        }
    }
}