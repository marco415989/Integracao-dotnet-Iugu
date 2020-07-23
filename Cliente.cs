using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mia.Webservices.Iugu.Models
{
    public class Cliente
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public string phone { get; set; }
        public string phone_prefix { get; set; }
        public string cpf_cnpj { get; set; }
        public string cc_emails { get; set; }
        public string zip_code { get; set; }
        public string number { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string complement { get; set; }
        public string default_payment_method_id { get; set; }
        public string proxy_payments_from_customer_id { get; set; }
        public object[] custom_variables { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }


    }
}