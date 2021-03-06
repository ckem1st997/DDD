using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Infrastructure.Middlewares
{
    public class FailingOptions
    {
        // url để tiến hành bật, tắt bộ chặn
        public string ConfigPath = "/Failing";
        public List<string> EndpointPaths { get; set; } = new List<string>();

        public List<string> NotFilteredPaths { get; set; } = new List<string>();
    }
}