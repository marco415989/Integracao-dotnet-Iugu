using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mia.Webservices.Iugu.Models
{
    public class Fatura
    {
        public string id { get; set; }
        public string email { get; set; }
        public string cc_emails { get; set; }
        public string due_date { get; set; }
        public string status { get; set; }
        public string currency { get; set; }
        public bool ensure_workday_due_date { get; set; }//se true garante que a data de vencimento seja apenas em dias de semana, e não em sábados ou domingos.
        public bool ignore_canceled_email { get; set; }//Desliga o e-mail de cancelamento de fatura
        public bool fines { get; set; }//Booleano para Habilitar ou Desabilitar multa por atraso de pagamento
        public bool ignore_due_email { get; set; }//Booleano que ignora o envio do e-mail de cobrança
        public bool per_day_interest { get; set; }//Booleano que determina se cobra ou não juros por dia de atraso. 1% ao mês pro rata. Necessário passar a multa como true
        public bool early_payment_discount { get; set; }//Ativa ou desativa os descontos por pagamento antecipado. Quando true, sobrepõe as configurações de desconto da conta.
        public int late_payment_fine { get; set; }//Determine a multa % a ser cobrada para pagamentos efetuados após a data de vencimento
        public int per_day_interest_value { get; set; }//Informar o valor percentual de juros que deseja cobrar
        public string discount_cents { get; set; }
        public string items_total_cents { get; set; }
        public string total_cents { get; set; }
        public string total_paid_cents { get; set; }
        public string tax_cents { get; set; }
        public string taxes_paid { get; set; }
        public string financial_return_date { get; set; }
        public string commission { get; set; }
        public string interest { get; set; }
        public string paid_at { get; set; }
        public string updated_at { get; set; }
        public string secure_id { get; set; }
        public string secure_url { get; set; }
        public string customer_id { get; set; }
        public string subscription_id { get; set; }
        public string customer_name { get; set; }
        public string return_url { get; set; }
        public string expired_url { get; set; }
        public string notification_url { get; set; }
        public string payable_with { get; set; }
        public string created_at { get; set; }
        public string expired_at { get; set; }
        public string canceled_at { get; set; }
        public string protested_at { get; set; }
        public string commission_cents { get; set; }
        public string user_id { get; set; }
        public string total { get; set; }
        public string discount { get; set; }
        public string refundable { get; set; }
        public string installments { get; set; }
        public object[] items { get; set; }
        public object[] variables { get; set; }
        public object[] logs { get; set; }
        public object[] custom_variables { get; set; }
        public object[] financial_return_dates { get; set; }
        public object bank_slip { get; set; }
        public object[] early_payment_discounts { get; set; }//Quantidade de dias de antecedência para o pagamento receber o desconto (Se enviado, substituirá a configuração atual da conta)
        public Cliente payer{ get; set; }
    }
}