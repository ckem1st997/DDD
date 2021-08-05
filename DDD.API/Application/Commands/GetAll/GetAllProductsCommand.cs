using DDD.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DDD.API.Application.Commands.GetAll
{
    public class GetAllProductsCommand : IRequest<IEnumerable<Products>>
    {
        [DataMember]
        public bool All { get; set; }
    }
}

