using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using WebApp.ClassesCompartilhadas;
using WebApp.Requests;
using WebApp.Response;

namespace WebApp.Models
{
    public class Account
    {
        public string Name { get; set; }

        public string CPF_CNPJ { get; set; }

        public string Chave { get; set; }
        public string Email { get; set; }
        public string Telephone1 { get; set; }
        public Endereco Endereco { get; set; }
        public string Site { get; set; }
        public string ListaPrecoProdutos { get; set; }

        public Regex Regex;

        public string Contato { get; set; }
       

        public ResponseCriaAtt Conta()
        {
            Conexao conexao = new Conexao();
            conexao.Autentica();
            Regex = new Regex(@"[^\d]");


            string cpf_cnpj = Regex.Replace(CPF_CNPJ, "");
            string cpf_cnpjMascara;

            if (cpf_cnpj.Length != 14 && cpf_cnpj.Length != 11)
            {
                return new ResponseCriaAtt("CPF ou CNPJ inválido", false);

            }
            if (cpf_cnpj.Length == 14)
                cpf_cnpjMascara = Int64.Parse(cpf_cnpj).ToString(@"00\.000\.000\/0000\-00");
            else 
                cpf_cnpjMascara = Int64.Parse(cpf_cnpj).ToString(@"000\.000\.000\-00");
            



            QueryExpression busca = new QueryExpression("account");
            busca.ColumnSet = new ColumnSet("naru_cpf");
            FilterExpression filtro = new FilterExpression(LogicalOperator.And);
            filtro.AddCondition("naru_cpf", ConditionOperator.Equal, cpf_cnpjMascara);
            //FilterExpression filtro2 = new FilterExpression(LogicalOperator.And);
            //filtro2.AddCondition();
            //filtro2.AddCondition();
            //filtro.AddFilter(filtro2);
            busca.Criteria.AddFilter(filtro);

            EntityCollection entidades = conexao.Service.RetrieveMultiple(busca);

            if (entidades.Entities.Count > 0)
            {
                Entity conta = new Entity("account");
                conta.Id = (entidades.Entities.First<Entity>()).Id;
                AtualizaEndereco(conta);
                if (string.IsNullOrEmpty(Name))
                    conta.Attributes["name"] = Name;
                if (string.IsNullOrEmpty(Telephone1))
                    conta.Attributes["telephone1"] = Telephone1;
                if (string.IsNullOrEmpty(Email))
                    conta.Attributes["emailaddress1"] = Email;
                if (string.IsNullOrEmpty(Endereco.CEP))
                    

                    //conta.Attributes["address1_postalcode"] = Endereco.CEP;
                if (string.IsNullOrEmpty(Contato))
                    conta.Attributes["naru_nomedocontato"] = Contato;



                conexao.Service.Update(conta);
            }

            

            //Entity conta = new Entity("account");
            //conta.Attributes["name"] = Name;
            ////conta.Attributes["naru_cpf"] = regex.Replace(CPF_CNPJ, "");
            //conta.Attributes["telephone1"] = Telephone1;
            //conta.Attributes["emailaddress1"] = Email;
            //conta.Attributes["address1_postalcode"] = Endereco.CEP;





            return new ResponseCriaAtt("ok", true);
        }
        public async Task AtualizaEndereco(Entity conta)
        {
            try
            {
                HttpClient requisicao = new HttpClient();
                string regex = Regex.Replace(Endereco.CEP, "");
               // requisicao.BaseAddress = new Uri("https://viacep.com.br/ws/" + regex + "/json");


                var response = await requisicao.GetAsync("https://viacep.com.br/ws/" + regex + "/json").ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}