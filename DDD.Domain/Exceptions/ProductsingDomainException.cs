using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Exceptions
{
    public class ProductsingDomainException : Exception
    {
        public ProductsingDomainException()
        { }

        public ProductsingDomainException(string message)
            : base(message)
        { }

        public ProductsingDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}