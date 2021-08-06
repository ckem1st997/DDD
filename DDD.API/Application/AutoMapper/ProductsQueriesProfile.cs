using AutoMapper;
using DDD.API.Application.Models;
using DDD.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.AutoMapper
{
    public class ProductsQueriesProfile : Profile
    {
        public ProductsQueriesProfile()
        {
            CreateMap<Products, ProductsDTO>();
        }
    }
}
