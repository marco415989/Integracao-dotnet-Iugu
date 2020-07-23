using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mia.Webservices.Iugu.Models
{
    public class FaturaCad
    {
        public string email { get; set; }
        public string due_date { get; set; }
        public object[] items { get; set; }
    }
}