using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebMVC
{
   public class ApplicationRole : IdentityRole
    {
        public string description { get; set; }
        public DateTime created_date { get; set; }
        public string ip_address { get; set; }
    }
}
