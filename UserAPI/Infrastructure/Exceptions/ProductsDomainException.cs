using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Infrastructure.Exceptions
{
    public class ProductsDomainException : Exception
    {
        public ProductsDomainException()
        { }

        public ProductsDomainException(string message)
            : base(message)
        { }

        public ProductsDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}