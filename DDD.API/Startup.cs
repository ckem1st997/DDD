using DDD.API.Application.AutoMapper;
using DDD.API.Application.AutoMapper.ConfigureServices;
using DDD.API.Application.Behaviors.ConfigureServices;
using DDD.API.Application.Validations.ConfigureServices;
using DDD.API.Controllers;
using DDD.API.Filters;
using DDD.Domain.IRepositories;
using DDD.Infrastructure.Context;
using DDD.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

using System.Reflection;


namespace DDD.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
                .AddApplicationPart(typeof(ProductsController).Assembly)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
              //      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DDD.API", Version = "v1" });
            });
            services.AddDbContext<ProductsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProductsContext"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );
            services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));
            services.AddScoped<IDapper, Dapperr>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMapper();
            services.AddValidator();
            services.AddBehavior();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
