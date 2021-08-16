using DDD.API.Application.Models;
using DDD.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DDD.API.Application.Commands.Create
{
    [DataContract]
    public class CreateProductsCommand: IRequest<bool>
    {
        [DataMember]
        public ProductsCommand ProductsCommand { get; set; }
    }
}
