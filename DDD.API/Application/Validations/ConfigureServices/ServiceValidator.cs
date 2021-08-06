using DDD.API.Application.Commands.Create;
using DDD.API.Application.Commands.Update;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.Validations.ConfigureServices
{
    public static class ServiceValidator
    {
        public static void AddValidator(this IServiceCollection services)
        {
            services.AddFluentValidation();
            services.AddTransient<IValidator<CreateProductsCommand>, CreateProductsCommandValidator>();
            services.AddTransient<IValidator<UpdateProductsCommand>, UpdateProductsCommandValidator>();
           // services.AddTransient<IValidator<ProductsDTO>, ProductsDTOVa>();
        }
    }
}
