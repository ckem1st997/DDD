using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DDD.API.Application.Commands.Delete
{
    [DataContract]
    public class DeleteProductsCommand : IRequest<bool>
    {
        [DataMember]
        public int Id { get; set; }
    }
}
