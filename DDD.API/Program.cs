using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DDD.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                    .ConfigureLogging(logging =>
                    {
                        logging.AddFilter("Grpc", LogLevel.Debug);
                    })
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        webBuilder.ConfigureKestrel(options =>
                        {
                            //// Setup a HTTP/2 endpoint without TLS.
                            //options.ListenLocalhost(44378, o => o.Protocols =
                            //    HttpProtocols.Http2);
                            options.Listen(IPAddress.Any, 44367, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                            });

                            options.Listen(IPAddress.Any, 5001, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http2;
                            });
                        });
                    }
                    webBuilder.UseStartup<Startup>();
                });
    }

}


