using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.HealthChecks
{
    public static class ServiceHealthChecks
    {
        public static void AddHealthChecksProducts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddRabbitMQ(
                $"amqp://{configuration["EventBusConnection"]}",
                name: "Products - RabbitQMbus - Check",
                tags: new string[] { "rabbitmqbus" })
                .AddSqlServer(
                configuration.GetConnectionString("ProductsContext"),
                name: "SQL - Server - Check",
                tags: new string[] { "sqlserver" });

        }
    }
}
