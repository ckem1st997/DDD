using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Infrastructure.Middlewares
{
    public class MessageFailingMiddleware
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
