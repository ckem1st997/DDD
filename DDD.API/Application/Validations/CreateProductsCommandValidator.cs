﻿using DDD.API.Application.Commands.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.Validations
{
    public class CreateProductsCommandValidator : AbstractValidator<CreateProductsCommand>
    {
        public CreateProductsCommandValidator()
        {
            RuleFor(order => order.ProductsCommand.Name).NotEmpty().WithMessage("not null"); ;
            RuleFor(order => order.ProductsCommand.Price).GreaterThan(0).WithMessage("not null");
        }
    }
}