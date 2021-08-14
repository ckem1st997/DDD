using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Domain.Entity;

namespace UserAPI.Service
{
    public class Paginated
    {
        public IEnumerable<Users> Users { get; set; }

        public int totalCount { get; set; }

        public Paginated()
        {
            totalCount = 0;
        }
    }
}
