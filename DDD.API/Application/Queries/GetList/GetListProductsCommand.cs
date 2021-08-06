using DDD.API.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DDD.API.Application.Queries.GetList
{
    public class GetListProductsCommand : IRequest<IEnumerable<ProductsDTO>>
    {

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}

