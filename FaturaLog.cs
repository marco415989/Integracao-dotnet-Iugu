using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mia.Webservices.Iugu.Models
{
    public class FaturaLog
    {
        public string id { get; set; }
        public string descricao { get; set; }
        public string notas { get; set; }
        public DateTime data_criacao { get; set; }
    }
}