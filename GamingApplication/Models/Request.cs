using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingApplication.Models
{
    public class Request
    {
        public string guess { get; set; }
        public int bet { get; set; }
        public int no_spin { get; set; }
        public string player { get; set; }
    }
}