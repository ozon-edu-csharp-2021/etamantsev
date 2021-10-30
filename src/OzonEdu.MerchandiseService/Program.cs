using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using OzonEdu.MerchandiseService.Infrastructure.Extensions;

namespace OzonEdu.MerchandiseService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>()
                    //Код для запуска http2 под macos для тестирования.
                    /*.ConfigureKestrel(opt =>
                    {
                        opt.ListenAnyIP(5000,
                            listenOptions => { listenOptions.Protocols = HttpProtocols.Http1;});
                        
                        opt.ListenAnyIP(50054,
                            listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
                    })*/;
                    
                })
                .AddInfrastructure();
    }
}