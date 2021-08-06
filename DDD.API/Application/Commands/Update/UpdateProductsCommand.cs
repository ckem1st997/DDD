using DDD.API.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DDD.API.Application.Commands.Update
{
    [DataContract]
    public class UpdateProductsCommand : IRequest<bool>
    {
        [DataMember]
        public ProductsCommand ProductsCommand { get; set; }
    }
}
