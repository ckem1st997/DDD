using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.AutoMapper.ConfigureServices
{
    public static class ServiceMapper
    {
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductsCommandProfile).Assembly);
            services.AddAutoMapper(typeof(ProductsQueriesProfile).Assembly);
        }
    }
}
