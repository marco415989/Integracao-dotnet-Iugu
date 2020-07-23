using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mia.Webservices.Iugu.Models
{
    public class FaturaItens
    {
        public string id { get; set; }
        public string description { get; set; }
        public int price_cents { get; set; }
        public int quantity { get; set; }
        //public DateTime data_criacao { get; set; }
        //public DateTime data_atualizacao { get; set; }
    }
}