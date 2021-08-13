using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Domain.Entity
{
    public class Users : BaseEntity
    {

        public string Username { get; set; }

        public string Pass { get; set; }

        public string Roleu { get; set; }

        public bool Active { get; set; }

    }
}
