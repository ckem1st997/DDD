using DDD.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Events
{
   public class CreateProductDomainEvent : INotification
    {
        public Products Products { get; }

        public CreateProductDomainEvent(Products products)
        {
            Products = products;
        }
    }
}
