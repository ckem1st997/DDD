using DDD.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.Models
{
    public class ProductsDTO:BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Imgage { get; set; }
    }
}
