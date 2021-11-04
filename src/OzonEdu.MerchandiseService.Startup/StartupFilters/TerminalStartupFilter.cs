using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.MerchandiseService.Middleware.Middleware;

namespace OzonEdu.MerchandiseService.Middleware.StartupFilters
{
    public class TerminalStartupFilter: IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());
                app.Map("/ready", builder => builder.UseMiddleware<OkResponseMiddleware>());
                app.Map("/live", builder => builder.UseMiddleware<OkResponseMiddleware>());
                next(app);
            };
        }
    }
}