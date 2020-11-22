using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRunningBlog.Models.Configuration
{
    public class StravaSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RefeshToken { get; set; }
        public string GrantType { get; set; }
    }
}
