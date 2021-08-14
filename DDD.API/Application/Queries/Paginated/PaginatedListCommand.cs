using DDD.API.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DDD.API.Application.Queries.Paginated
{
    [DataContract]
    public class PaginatedListCommand : IRequest<PaginatedList>
    {
        [DataMember]
        public string KeySearch { get; set; }

        [DataMember]
        public decimal? fromPrice { get; set; }

        [DataMember]
        public decimal? toPrice { get; set; }
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int PageNumber { get; set; }
    }
}
