using DDD.API.Application.Models;
using DDD.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace DDD.API.Application.AutoMapper
{
    public class ProductsCommandProfile : Profile
    {
        public ProductsCommandProfile()
        {
            CreateMap<ProductsCommand, Products>()
                .ForMember(x=>x.CreateDate,opt=>opt.Ignore())
                .ForMember(x => x.ModiDate, opt => opt.Ignore());
        }
    }
}
