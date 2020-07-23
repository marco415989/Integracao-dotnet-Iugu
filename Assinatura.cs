using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mia.Webservices.Iugu.Models
{
    public class Assinatura
    {
        public string id { get; set; }
        public bool suspended { get; set; }
        public string plan_identifier { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }
        public string customer_ref { get; set; }
        public string plan_name { get; set; }
        public string plan_ref { get; set; }
        public string customer_id { get; set; }
        public string payable_with { get; set; }
        public string expires_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public bool credits_based { get; set; }
        public int price_cents { get; set; }
        public object[] subitems { get; set; }
        public object[] logs { get; set; }
        public object[] custom_variables { get; set; }
        public bool suspend_on_invoice_expired { get; set; }
        public bool only_on_charge_success { get; set; }
        public bool ignore_due_email { get; set; }
        public int credits_cycle { get; set; }
        public int credits_min { get; set; }
        public bool two_step { get; set; }
        public bool active { get; set; }

    }
}