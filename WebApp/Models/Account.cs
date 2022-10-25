using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public string Telephone1  { get; set; }
        public Endereco Endereco { get; set; }
        public string Site { get; set; }
        public string ListaPrecoProdutos { get; set; }

        public ResponseCriaAtt Conta()
        {
            if (CPF_CNPJ == "" || CPF_CNPJ == null)
            {
                return new ResponseCriaAtt("CPF ou CNPJ é obrigatório", false);
                
            }
            Regex regexRemCaracter = new Regex("/[^d] / g");
            Regex regexIncMascCPF = new Regex("^/d{3}/./d{3}/./d{3}-/d{2}$");
            Regex regexInMascCNPJ = new Regex("^(d{2}).(d{3}).(d{3})/(d{4})-(d{2})");
            string teste = "12.345.678/9012-34";
            string cpf_cnpj = regexRemCaracter.Replace(teste, "");
            string cpfcomMarcara = int.Parse(cpf_cnpj).ToString(@"00\.000\.000\/0000\-00");
            

            QueryExpression busca = new QueryExpression("account");
            busca.ColumnSet = new ColumnSet("naru_cpf");
            FilterExpression filtro = new FilterExpression(LogicalOperator.Or);
            filtro.AddCondition("naru_cpf", ConditionOperator.Equal, cpf_cnpj);
            //FilterExpression filtro2 = new FilterExpression(LogicalOperator.And);
            //filtro2.AddCondition();
            //filtro2.AddCondition();
            //filtro.AddFilter(filtro2);
            busca.Criteria = filtro;

            Entity conta = new Entity("account");
            conta.Attributes["name"] = Name;
            //conta.Attributes["naru_cpf"] = regex.Replace(CPF_CNPJ, "");
            conta.Attributes["telephone1"] = Telephone1;
            conta.Attributes["emailaddress1"] = Email;
            conta.Attributes["address1_postalcode"] = Endereco.CEP;





            return new ResponseCriaAtt("ok", true);
        }
    }
}