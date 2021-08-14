using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.HealthChecks
{
    public static class ServiceHealthChecksUI
    {
        public static void AddHealthChecksUI(this IServiceCollection services)
        {
            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.SetEvaluationTimeInSeconds(5); //Configures the UI to poll for healthchecks updates every 5 seconds
                setup.SetApiMaxActiveRequests(5);
                // thêm điểm cuối để check
                setup.AddHealthCheckEndpoint("ProductsAPI", "/healthz");
                setup.MaximumHistoryEntriesPerEndpoint(50);
            }).AddInMemoryStorage();
        }
    }
}
