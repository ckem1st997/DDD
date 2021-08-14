
using Autofac;
using DDD.API.Application.IntegrationEvents;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using IntegrationEventLogEF.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.ConfigureServices.EventBus
{
    public static class EventBusMQ
    {
        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
               sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IProductsIntegrationEventService, ProductsIntegrationEventService>();


            services.AddSingleton<IEventBus, EventRabbitMQ>(sp =>
            {
                var subscriptionClientName = configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            // thêm xử lí event trong app
            //   services.AddTransient<OrderStartedIntegrationEventHandler>();
        }


        public static void ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();


            // đăng ký xử lý
            //  eventBus.Subscribe<OrderStartedIntegrationEvent, OrderStartedIntegrationEventHandler>();
        }
    }
}
