using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sso.demo0.com.Models
{
    public class SSOClients
    {
        public string ClientID { get; set; }
        public string ClientIcon { get; set; }
        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public string ClientKey { get; set; }
        public int ClientStatus { get; set; }
    }
}