using DDD.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.API.Application.Models
{
    public class ProductsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModiDate { get; set; }
    }
}
