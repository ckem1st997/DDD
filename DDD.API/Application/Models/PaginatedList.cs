using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.Models
{
    public class PaginatedList
    {
        public IEnumerable<ProductsDTO> ProductsDTOs { get; set; }

        public int totalCount { get; set; }

        public PaginatedList()
        {
            ProductsDTOs = null;
            totalCount = 0;
        }
    }
}
