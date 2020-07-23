using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mia.Webservices.Iugu.Models
{
    public class AssinaturaPlano
    {
        public string id { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
        public int interval { get; set; }
        public string interval_type { get; set; }
        public decimal value_cents { get; set; }
        public string payable_with { get; set; }//'all', 'credit_card' ou 'bank_slip'
    }
}