using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Infrastructure.Middlewares;

namespace UserAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseFailing(options =>
                    {
                        // url bật tắt
                        options.ConfigPath = "/Failing";
                        // url chặn, tuyệt đối, vd:/api/Users
                        options.EndpointPaths.AddRange(new[] { "" });
                        // url hông bị chặn, tuyệt đối
                        options.NotFilteredPaths.AddRange(new[] { "" });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    
    }

}
