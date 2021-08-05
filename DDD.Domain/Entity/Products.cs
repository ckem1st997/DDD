using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Entity
{
    public class Products : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
