using mia.Models;
using mia.Webservices.Iugu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Script.Serialization;
using mia.Controllers.Helpers;

namespace mia.Webservices
{
    public class IuguApi
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region tokens

        private const string token_homologacao = "seu_token_homologacao";
        private const string token_producao = "seu_token_producao";

        #endregion

        #region urls

        private const string URL_CLIENTES = "https://api.iugu.com/v1/customers/";
        private const string URL_FATURAS = "https://api.iugu.com/v1/invoices";
        private const string URL_PLANOS = "https://api.iugu.com/v1/plans/";
        private const string URL_PLANOS_POR_IDENTIFICADOR = "https://api.iugu.com/v1/plans/identifier/";
        private const string URL_ASSINATURAS = "https://api.iugu.com/v1/subscriptions/";


        #endregion

        #region Clientes
        public bool CadastraCliente(mia.Webservices.Iugu.Models.Cliente cliente, string token_tipo)
        {
            try
            {

                cliente.cpf_cnpj = StringHelper.RemoveMask(cliente.cpf_cnpj);
                cliente.phone_prefix = StringHelper.RemoveMask(cliente.phone_prefix);
                cliente.phone = StringHelper.RemoveMask(cliente.phone);
                cliente.zip_code = StringHelper.RemoveMask(cliente.zip_code);

                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_CLIENTES);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PostAsJsonAsync(obj_parametros, cliente).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public bool AlteraCliente(mia.Webservices.Iugu.Models.Cliente cliente, string id, string token_tipo)
        {
            try
            {

                cliente.cpf_cnpj = StringHelper.RemoveMask(cliente.cpf_cnpj);
                cliente.phone_prefix = StringHelper.RemoveMask(cliente.phone_prefix);
                cliente.phone = StringHelper.RemoveMask(cliente.phone);
                cliente.zip_code = StringHelper.RemoveMask(cliente.zip_code);

                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_CLIENTES + id);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PutAsJsonAsync(obj_parametros, cliente).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public List<mia.Webservices.Iugu.Models.Cliente> ListaClientes(Dictionary<string, object> parametros, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            if (parametros != null)
            {
                foreach (var item in parametros)
                {
                    obj_parametros += "&" + item.Key.ToString() + "=" + item.Value.ToString();
                }
            }

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_CLIENTES);
            client.BaseAddress = baseAddress;

            List<mia.Webservices.Iugu.Models.Cliente > clientes = new List<mia.Webservices.Iugu.Models.Cliente>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            Object meta = userObject["items"];

            foreach (var item in meta as object[])
            {
                Dictionary<string, object> dic_meta = (Dictionary<string, object>)item;

                var cliente = new mia.Webservices.Iugu.Models.Cliente();
                cliente.id = dic_meta["id"] != null ? dic_meta["id"].ToString() : null;
                cliente.email = dic_meta["email"] != null ? dic_meta["email"].ToString() : null;
                cliente.name = dic_meta["name"] != null ? dic_meta["name"].ToString() : null;
                cliente.cpf_cnpj = dic_meta["cpf_cnpj"] != null ? dic_meta["cpf_cnpj"].ToString() : null;
                cliente.zip_code = dic_meta["zip_code"] != null ? dic_meta["zip_code"].ToString() : null;
                cliente.number = dic_meta["number"] != null ? dic_meta["number"].ToString() : null;
                cliente.city = dic_meta["city"] != null ? dic_meta["city"].ToString() : null;
                cliente.state = dic_meta["state"] != null ? dic_meta["state"].ToString() : null;
                cliente.district = dic_meta["district"] != null ? dic_meta["district"].ToString() : null;
                cliente.street = dic_meta["street"] != null ? dic_meta["street"].ToString() : null;
                cliente.complement = dic_meta["complement"] != null? dic_meta["complement"].ToString() : null;

                clientes.Add(cliente);
            }

            return clientes;
        }
        public bool ExcluiCliente(string id, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_CLIENTES + id);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.DeleteAsync(obj_parametros).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public Dictionary<string,object> BuscaCliente(string id_cliente, string token_tipo)
        {
            var cliente = new mia.Webservices.Iugu.Models.Cliente();
            var errors = "none";
            var dic_result = new Dictionary<string, object>();

            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_CLIENTES + id_cliente);
            client.BaseAddress = baseAddress;

            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            if (! userObject.ContainsKey("errors"))
            {
                cliente.cc_emails = userObject["cc_emails"] != null ? userObject["cc_emails"].ToString() : null;
                cliente.city = userObject["city"] != null ? userObject["city"].ToString() : null;
                cliente.complement = userObject["complement"] != null ? userObject["complement"].ToString() : null;
                cliente.cpf_cnpj = userObject["cpf_cnpj"] != null ? userObject["cpf_cnpj"].ToString() : null;
                cliente.created_at = userObject["created_at"] != null ? userObject["created_at"].ToString() : null;
                cliente.default_payment_method_id = userObject["default_payment_method_id"] != null ? userObject["default_payment_method_id"].ToString() : null;
                cliente.district = userObject["district"] != null ? userObject["district"].ToString() : null;
                cliente.email = userObject["email"] != null ? userObject["email"].ToString() : null;
                cliente.id = userObject["id"] != null ? userObject["id"].ToString() : null;
                cliente.name = userObject["name"] != null ? userObject["name"].ToString() : null;
                cliente.notes = userObject["notes"] != null ? userObject["notes"].ToString() : null;
                cliente.number = userObject["number"] != null ? userObject["number"].ToString() : null;
                cliente.phone = userObject["phone"] != null ? userObject["phone"].ToString() : null;
                cliente.phone_prefix = userObject["phone_prefix"] != null ? userObject["phone_prefix"].ToString() : null;
                cliente.proxy_payments_from_customer_id = userObject["proxy_payments_from_customer_id"] != null ? userObject["proxy_payments_from_customer_id"].ToString() : null;
                cliente.state = userObject["state"] != null ? userObject["state"].ToString() : null;
                cliente.street = userObject["street"] != null ? userObject["street"].ToString() : null;
                cliente.updated_at = userObject["updated_at"] != null ? userObject["updated_at"].ToString() : null;
                cliente.zip_code = userObject["zip_code"] != null ? userObject["zip_code"].ToString() : null;
            }
            else
            {
                errors = userObject["errors"].ToString();
            }

            dic_result.Add("errors", errors);
            dic_result.Add("Cliente", cliente);

            return dic_result;
        }
        #endregion

        #region Faturas
        public bool CadastraFatura(mia.Webservices.Iugu.Models.FaturaCad fatura, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_FATURAS);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PostAsJsonAsync(obj_parametros, fatura).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public List<Fatura> ListaFaturas(Dictionary<string, object> parametros, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

          
            string obj_parametros = "?api_token=" + token;
            if(parametros != null)
            { 
                foreach (var item in parametros)
                {
                    obj_parametros += "&" + item.Key.ToString() + "=" + item.Value.ToString();
                }
            }

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_FATURAS);
            client.BaseAddress = baseAddress;

            List<Fatura> faturas = new List<Fatura>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            Object meta = userObject["items"];

            foreach (var item in meta as object[])
            {
                Dictionary<string, object> dic_meta = (Dictionary<string, object>)item;

                var fatura = new Fatura();

                fatura.id = dic_meta["id"] != null? dic_meta["id"].ToString() : "";
                fatura.due_date = dic_meta["due_date"] != null ? dic_meta["due_date"].ToString() : "";
                fatura.secure_id = dic_meta["secure_id"] != null ? dic_meta["secure_id"].ToString() : "";
                fatura.secure_url = dic_meta["secure_url"] != null ? dic_meta["secure_url"].ToString() : "";
                fatura.customer_id = dic_meta["customer_id"] != null ? dic_meta["customer_id"].ToString() : "";
                fatura.customer_name = dic_meta["customer_name"] != null ? dic_meta["customer_name"].ToString() : "";
                fatura.total_cents = dic_meta["total_cents"] != null? dic_meta["total_cents"].ToString() : "0";
                fatura.discount_cents = dic_meta["discount_cents"] != null ? dic_meta["discount_cents"].ToString() : "0";
                fatura.tax_cents = dic_meta["tax_cents"] != null ? dic_meta["tax_cents"].ToString() : "0";
                fatura.total_paid_cents = dic_meta["total_paid_cents"] != null ? dic_meta["total_paid_cents"].ToString() : "0";
                fatura.email = dic_meta["email"] != null ? dic_meta["email"].ToString() : "";
                fatura.status = dic_meta["status"] != null ? dic_meta["status"].ToString() : "";
                fatura.created_at = dic_meta["created_at_iso"] != null ? dic_meta["created_at_iso"].ToString() : "";
                faturas.Add(fatura);
            }

            return faturas;
        }
        public List<Fatura> ListaFaturasPorContrato(string id_contrato, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_FATURAS);
            client.BaseAddress = baseAddress;

            List<Fatura> faturas = new List<Fatura>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            Object meta = userObject["items"];

            foreach (var item in meta as object[])
            {
                Dictionary<string, object> dic_meta = (Dictionary<string, object>)item;

                var variaveis = dic_meta["variables"] as object[];

                foreach (var variavel in variaveis)
                {
                    var dic_var = (Dictionary<string, object>)variavel;

                    var name = dic_var["variable"];

                    if (name.ToString() == "subscription_id")
                    {
                        var contrato_id = dic_var["value"];

                        if (contrato_id.ToString() == id_contrato)
                        {
                            var fatura = new Fatura();

                            fatura.id = dic_meta["id"] != null ? dic_meta["id"].ToString() : "";
                            fatura.due_date = dic_meta["due_date"] != null ? dic_meta["due_date"].ToString() : "";
                            fatura.secure_id = dic_meta["secure_id"] != null ? dic_meta["secure_id"].ToString() : "";
                            fatura.secure_url = dic_meta["secure_url"] != null ? dic_meta["secure_url"].ToString() : "";
                            fatura.customer_id = dic_meta["customer_id"] != null ? dic_meta["customer_id"].ToString() : "";
                            fatura.customer_name = dic_meta["customer_name"] != null ? dic_meta["customer_name"].ToString() : "";
                            fatura.total_cents = dic_meta["total_cents"] != null ? dic_meta["total_cents"].ToString() : "0";
                            fatura.discount_cents = dic_meta["discount_cents"] != null ? dic_meta["discount_cents"].ToString() : "0";
                            fatura.tax_cents = dic_meta["tax_cents"] != null ? dic_meta["tax_cents"].ToString() : "0";
                            fatura.total_paid_cents = dic_meta["total_paid_cents"] != null? dic_meta["total_paid_cents"].ToString() : "0";
                            fatura.email = dic_meta["email"] != null ? dic_meta["email"].ToString() : "";
                            fatura.status = dic_meta["status"] != null ? dic_meta["status"].ToString() : "";
                            fatura.created_at = dic_meta["created_at_iso"] != null ? dic_meta["created_at_iso"].ToString() : "";
                            faturas.Add(fatura);
                        }
                    }
                    
                }
            }

            return faturas;
        }
        public List<Fatura> ListaFaturasPorCliente(string id_cliente, string token_tipo)
        {
            return ListaFaturas(new Dictionary<string, object>() { {"query", id_cliente } }, token_tipo).Where(f => f.customer_id == id_cliente).ToList();
        }
        public Fatura BuscaFatura(string id_fatura, string token_tipo)
        {
            var fatura = new mia.Webservices.Iugu.Models.Fatura();

            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_FATURAS + "/" + id_fatura);
            client.BaseAddress = baseAddress;

            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            if (!userObject.ContainsKey("errors"))
            {
                fatura.id = userObject["id"] != null ? userObject["id"].ToString() : null;
                fatura.due_date = userObject["due_date"] != null ? userObject["due_date"].ToString() : null;
                fatura.currency = userObject["currency"] != null ? userObject["currency"].ToString() : null;
                fatura.discount_cents = userObject["discount_cents"] != null ? userObject["discount_cents"].ToString() : null;
                fatura.email = userObject["email"] != null ? userObject["email"].ToString() : null;
                fatura.items_total_cents = userObject["items_total_cents"] != null ? userObject["items_total_cents"].ToString() : null;
                fatura.notification_url = userObject["notification_url"] != null ? userObject["notification_url"].ToString() : null;
                fatura.return_url = userObject["return_url"] != null ? userObject["return_url"].ToString() : null;
                fatura.status = userObject["status"] != null ? userObject["status"].ToString() : null;
                fatura.tax_cents = userObject["tax_cents"] != null ? userObject["tax_cents"].ToString() : null;
                fatura.updated_at = userObject["updated_at"] != null ? userObject["updated_at"].ToString() : null;
                fatura.canceled_at = userObject["canceled_at"] != null ? userObject["canceled_at"].ToString() : null;
                fatura.total_cents = userObject["total_cents"] != null ? userObject["total_cents"].ToString() : null;
                fatura.paid_at = userObject["paid_at"] != null ? userObject["paid_at"].ToString() : null;
                fatura.commission_cents = userObject["commission_cents"] != null ? userObject["commission_cents"].ToString() : null;
                fatura.secure_id = userObject["secure_id"] != null ? userObject["secure_id"].ToString() : null;
                fatura.secure_url = userObject["secure_url"] != null ? userObject["secure_url"].ToString() : null;
                fatura.customer_id = userObject["customer_id"] != null ? userObject["customer_id"].ToString() : null;
                fatura.updated_at = userObject["updated_at"] != null ? userObject["updated_at"].ToString() : null;
                fatura.user_id = userObject["user_id"] != null ? userObject["user_id"].ToString() : null;
                fatura.total = userObject["total"] != null ? userObject["total"].ToString() : null;
                fatura.taxes_paid = userObject["taxes_paid"] != null ? userObject["taxes_paid"].ToString() : null;
                fatura.financial_return_date = userObject["financial_return_date"] != null ? userObject["financial_return_date"].ToString() : null;
                fatura.commission = userObject["commission"] != null ? userObject["commission"].ToString() : null;
                fatura.interest = userObject["interest"] != null ? userObject["interest"].ToString() : null;
                fatura.discount = userObject["discount"] != null ? userObject["discount"].ToString() : null;
                fatura.created_at = userObject["created_at"] != null ? userObject["created_at"].ToString() : null;
                fatura.refundable = userObject["refundable"] != null ? userObject["refundable"].ToString() : null;
                fatura.installments = userObject["installments"] != null ? userObject["installments"].ToString() : null;
                fatura.financial_return_dates = (object[]) userObject["financial_return_dates"];
                fatura.bank_slip = userObject["bank_slip"];
                fatura.items = (object[]) userObject["items"];
                fatura.logs = (object[]) userObject["logs"];
                fatura.variables = (object[])userObject["variables"];
                fatura.custom_variables = (object[])userObject["custom_variables"];
            }

            return fatura;
        }
        public bool GerarSegundaVia(Fatura fatura, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_FATURAS + "/" + fatura.id + "/duplicate");
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PostAsJsonAsync(obj_parametros, fatura).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public bool EnviarPorEmail(string id_fatura, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_FATURAS + "/" + id_fatura + "/send_email");
                client.BaseAddress = baseAddress;

                var fatura = BuscaFatura(id_fatura, token_tipo);

                HttpResponseMessage response = client.PostAsJsonAsync(obj_parametros, fatura).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public bool CancelaFatura(string id_fatura, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_FATURAS + "/" + id_fatura + "/cancel");
                client.BaseAddress = baseAddress;

                var fatura = BuscaFatura(id_fatura, token_tipo);

                HttpResponseMessage response = client.PutAsJsonAsync(obj_parametros, fatura).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public int FaturasCount(Dictionary<string, object> parametros, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            if (parametros != null)
            {
                foreach (var item in parametros)
                {
                    obj_parametros += "&" + item.Key + "=" + item.Value;
                }
            }

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_FATURAS);
            client.BaseAddress = baseAddress;

            List<Fatura> faturas = new List<Fatura>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            var total = userObject["totalItems"];
            int out_int = -1;

            if (int.TryParse(total.ToString(), out out_int))
            {
                return int.Parse(total.ToString());
            }
            else
            {
                return 0;
            }


        }
        public List<Fatura> ListaFaturasAtrasadas(string ambiente)
        {
            List<Fatura> faturas_atrasadas = new List<Fatura>();
            List<Fatura> faturas_pendentes = new List<Fatura>();


            var faturas_count = FaturasCount(new Dictionary<string, object>() { { "status_filter", "pending" } }, ambiente);

            var total_paginas = faturas_count % 100 > 0? (faturas_count / 100) + 1 : faturas_count / 100;

            for (int i = 1; i <= total_paginas; i++)
            {
                var pular = (i * 100) - 100;

                faturas_pendentes.AddRange(
                    ListaFaturas(
                        new Dictionary<string, object>() { 
                            { "status_filter", "pending" }, 
                            { "start", pular.ToString()} 
                        }, ambiente
                    )
                );
            }

            foreach (var item in faturas_pendentes)
            {
                var data_vencimento = DateTime.Parse(item.due_date);

                if (data_vencimento < DateTime.Today)
                {
                    faturas_atrasadas.Add(item);
                }
            }

            return faturas_atrasadas.OrderBy(f => f.due_date).ToList();
        }


        public List<Fatura> ListaFaturasSemNfse(string ambiente)
        {
            List<Fatura> faturas_agregador = new List<Fatura>();
            List<Fatura> faturas_pagas = new List<Fatura>();


            var faturas_count = FaturasCount(new Dictionary<string, object>() { { "status_filter", "paid" } }, ambiente);

            var total_paginas = faturas_count % 100 > 0 ? (faturas_count / 100) + 1 : faturas_count / 100;

            for (int i = 1; i <= total_paginas; i++)
            {
                var pular = (i * 100) - 100;

                faturas_pagas.AddRange(
                    ListaFaturas(
                        new Dictionary<string, object>() {
                            { "status_filter", "paid" },
                            { "start", pular.ToString()}
                        }, ambiente
                    )
                );
            }


            
            foreach (var item in faturas_pagas)
            {
                var fatura_id = item.id;

                var fatura_nfse = db.FaturaNfse.Where(f => f.id_fatura.Equals(fatura_id)).FirstOrDefault();

                if (fatura_nfse == null)
                {
                    faturas_agregador.Add(item);
                }

            }
            

            return faturas_agregador.OrderBy(f => f.due_date).ToList();
        }

        #endregion

        #region Planos
        public bool CadastraPlano(AssinaturaPlano plano, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_PLANOS);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PostAsJsonAsync(obj_parametros, plano).Result;
            }
            catch (Exception)
            {
                return false;
            }
            

            return true;
        }
        public bool AlteraPlano(AssinaturaPlano plano, string id, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_PLANOS + id);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PutAsJsonAsync(obj_parametros, plano).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public List<AssinaturaPlano> ListaPlanos(Dictionary<string, object> parametros, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            if (parametros != null)
            {
                foreach (var item in parametros)
                {
                    obj_parametros += "&" + item.Key.ToString() + "=" + item.Value.ToString();
                }
            }

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_PLANOS);
            client.BaseAddress = baseAddress;

            List<AssinaturaPlano> planos = new List<AssinaturaPlano>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            Object meta = userObject["items"];

            foreach (var item in meta as object[])
            {
                Dictionary<string, object> dic_meta = (Dictionary<string, object>)item;

                var plano = new AssinaturaPlano();
                plano.id = dic_meta["id"] != null? dic_meta["id"].ToString() : "";
                plano.identifier = dic_meta["identifier"] != null ? dic_meta["identifier"].ToString() : "";
                plano.name = dic_meta["name"] != null ? dic_meta["name"].ToString() : "";
                plano.interval = (int)dic_meta["interval"];
                plano.interval_type = dic_meta["interval_type"] != null ? dic_meta["interval_type"].ToString() : "";
                plano.payable_with = dic_meta["payable_with"] != null ? dic_meta["payable_with"].ToString() : "";

                object[] precos_obj = (object[]) dic_meta["prices"];

                Dictionary<string, object> precos_dic = (Dictionary<string, object>)precos_obj[0];

                plano.value_cents = int.Parse(precos_dic["value_cents"].ToString());

                planos.Add(plano);
            }

            return planos;
        }
        public bool ExcluiPlano(string id, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_PLANOS + id);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.DeleteAsync(obj_parametros).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public List<AssinaturaPlano> BuscaPlanoPorIdentificador(string identifier, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_PLANOS_POR_IDENTIFICADOR + identifier);
            client.BaseAddress = baseAddress;

            List<AssinaturaPlano> planos = new List<AssinaturaPlano>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            Object meta = userObject["items"];

            foreach (var item in meta as object[])
            {
                Dictionary<string, object> dic_meta = (Dictionary<string, object>)item;

                var plano = new AssinaturaPlano();
                plano.id = dic_meta["id"].ToString();
                plano.identifier = dic_meta["identifier"].ToString();
                plano.name = dic_meta["name"].ToString();
                plano.interval = (int)dic_meta["interval"];
                plano.interval_type = dic_meta["interval"].ToString();
                plano.payable_with = dic_meta["payable_with"].ToString();

                object[] precos_obj = (object[])dic_meta["prices"];

                Dictionary<string, object> precos_dic = (Dictionary<string, object>)precos_obj[0];

                plano.value_cents = int.Parse(precos_dic["value_cents"].ToString());

                planos.Add(plano);
            }

            return planos;
        }

        public int PlanosCount(Dictionary<string, object> parametros, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            if (parametros != null)
            {
                //Inserir código com parametros aqui.
            }

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_PLANOS);
            client.BaseAddress = baseAddress;

            List<Fatura> faturas = new List<Fatura>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            var total = userObject["totalItems"];
            int out_int = -1;

            if (int.TryParse(total.ToString(), out out_int))
            {
                return int.Parse(total.ToString());
            }
            else
            {
                return 0;
            }


        }
        #endregion

        #region Assinaturas
        public bool CadastrarAssinatura(mia.Webservices.Iugu.Models.Assinatura assinatura, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_ASSINATURAS);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PostAsJsonAsync(obj_parametros, assinatura).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public bool AlteraAssinatura(Assinatura assinatura, string id, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_ASSINATURAS + id);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PutAsJsonAsync(obj_parametros, assinatura).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public bool ExcluiAssinatura(string id, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_ASSINATURAS + id);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.DeleteAsync(obj_parametros).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public bool AtivaAssinatura(Assinatura assinatura, string id, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_ASSINATURAS + id + "/activate");
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PostAsJsonAsync(obj_parametros, assinatura).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public bool SuspendeAssinatura(Assinatura assinatura, string id, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_ASSINATURAS + id + "/suspend");
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PostAsJsonAsync(obj_parametros, assinatura).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public bool AlteraPlanoDaAssinatura(Assinatura assinatura, string id, string plan_indefier, string token_tipo)
        {
            try
            {
                string token = "";

                if (token_tipo.Equals("producao"))
                {
                    token = token_producao;
                }
                else
                {
                    token = token_homologacao;
                }

                string obj_parametros = "?api_token=" + token;

                HttpClient client = new HttpClient();
                var baseAddress = new Uri(URL_ASSINATURAS + id + "/change_plan/" + plan_indefier);
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.PostAsJsonAsync(obj_parametros, assinatura).Result;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
        public List<Assinatura> ListaAssinaturas(Dictionary<string, object> parametros, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            if (parametros != null)
            {
                foreach (var item in parametros)
                {
                    obj_parametros += "&" + item.Key.ToString() + "=" + item.Value.ToString();
                }
            }

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_ASSINATURAS);
            client.BaseAddress = baseAddress;

            List<Assinatura> assinaturas = new List<Assinatura>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            Object meta = userObject["items"];

            foreach (var item in meta as object[])
            {
                Dictionary<string, object> dic_meta = (Dictionary<string, object>)item;

                var assinatura = new Assinatura();
                assinatura.id = dic_meta["id"] != null? dic_meta["id"].ToString() : "";
                assinatura.suspended = dic_meta["suspended"] != null ?  bool.Parse(dic_meta["suspended"].ToString()) : false;
                assinatura.plan_identifier = dic_meta["plan_identifier"] != null ? dic_meta["plan_identifier"].ToString() : "";
                assinatura.price_cents = dic_meta["price_cents"] != null ? int.Parse(dic_meta["price_cents"].ToString()) : 0;
                assinatura.expires_at = dic_meta["expires_at"] != null ? dic_meta["expires_at"].ToString() : dic_meta["created_at"].ToString();
                assinatura.created_at = dic_meta["created_at"] != null ? dic_meta["created_at"].ToString() : "";
                assinatura.updated_at = dic_meta["updated_at"] != null ? dic_meta["updated_at"].ToString() : "";
                assinatura.customer_name = dic_meta["customer_name"] != null ? dic_meta["customer_name"].ToString() : "";
                assinatura.customer_email = dic_meta["customer_email"] != null ? dic_meta["customer_email"].ToString() : "";
                assinatura.payable_with = dic_meta["payable_with"] != null ? dic_meta["payable_with"].ToString() : "";
                assinatura.customer_id = dic_meta["customer_id"] != null ? dic_meta["customer_id"].ToString() : "";
                assinatura.plan_name = dic_meta["plan_name"] != null ? dic_meta["plan_name"].ToString() : "";
                assinatura.customer_ref = dic_meta["customer_ref"] != null ? dic_meta["customer_ref"].ToString() : "";
                assinatura.plan_ref = dic_meta["plan_ref"] != null ? dic_meta["plan_ref"].ToString() : "";
                assinatura.active = dic_meta["active"] != null ? bool.Parse(dic_meta["active"].ToString()) : false;
                assinatura.subitems = (object[])dic_meta["subitems"];
                assinatura.logs = (object[])dic_meta["logs"];

                assinaturas.Add(assinatura);
            }

            return assinaturas;
        }
        public List<Assinatura> ListaAssinaturasAllPages(Dictionary<string, object> parametros, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            if (parametros != null)
            {
                foreach (var item in parametros)
                {
                    obj_parametros += "&" + item.Key.ToString() + "=" + item.Value.ToString();
                }
            }

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_ASSINATURAS);
            client.BaseAddress = baseAddress;

            List<Assinatura> assinaturas = new List<Assinatura>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            var total_contratos = userObject["totalItems"];

            var total_paginas = int.Parse(total_contratos.ToString()) % 100 > 0 ? (int.Parse(total_contratos.ToString()) / 100) + 1 : int.Parse(total_contratos.ToString()) / 100;

            int i = 1;

            while (i <= total_paginas)
            {
                var quantidade_a_pular = i == 1? 0 : (i - 1) * 100;

                Dictionary<string, object> dic_parametros = new Dictionary<string, object>();

                dic_parametros.Add("start", quantidade_a_pular);

                assinaturas.AddRange(ListaAssinaturas(dic_parametros, token_tipo));

                i++;
            }

            return assinaturas;
        }
        public int AssinaturasCount(Dictionary<string, object> parametros, string token_tipo)
        {
            string token = "";

            if (token_tipo.Equals("producao"))
            {
                token = token_producao;
            }
            else
            {
                token = token_homologacao;
            }

            string obj_parametros = "?api_token=" + token;

            if (parametros != null)
            {
                foreach (var item in parametros)
                {
                    obj_parametros += "&" + item.Key + "=" + item.Value;
                }
            }

            HttpClient client = new HttpClient();
            var baseAddress = new Uri(URL_ASSINATURAS);
            client.BaseAddress = baseAddress;

            List<Assinatura> assinaturas = new List<Assinatura>();
            HttpResponseMessage response = null;

            response = client.GetAsync(obj_parametros).Result;

            string json = response.Content.ReadAsStringAsync().Result;

            Dictionary<string, object> userObject = new JavaScriptSerializer().DeserializeObject(json) as Dictionary<string, object>;

            var total = userObject["totalItems"];
            int out_int = -1;

            if (int.TryParse(total.ToString(), out out_int))
            {
                return int.Parse(total.ToString());
            }
            else
            {
                return 0;
            }

           
        }
        #endregion
    }
}