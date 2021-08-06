using DDD.API.Application.Models;
using DDD.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DDD.API.Application.Queries.GetAll
{
    public class GetAllProductsCommand : IRequest<IEnumerable<ProductsDTO>>
    {
        [DataMember]
        public bool All { get; set; }
    }
}
