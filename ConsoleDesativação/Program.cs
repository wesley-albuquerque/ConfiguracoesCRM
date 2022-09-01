using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace ConsoleDesativacao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ponteiro = @"AuthType = OAuth;
            Username = narualbuquerque@naru365.onmicrosoft.com;
            Password = We230398*;
            Url = https://devbanconaru.crm2.dynamics.com/;
            AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
            RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
             LoginPrompt = Auto";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            CrmServiceClient servico = new CrmServiceClient(ponteiro);

            QueryExpression expressao = new QueryExpression("product");

            expressao.ColumnSet = new ColumnSet(true);
            //FilterExpression filtro = new FilterExpression(LogicalOperator.And);
            //filtro.AddCondition("statecode", ConditionOperator.Equal, 0);
            //filtro.AddCondition("statuscode", ConditionOperator.Equal, 1);
            //expressao.Criteria = filtro;
            EntityCollection produtos = servico.RetrieveMultiple(expressao);

            Entity entidade = produtos.Entities.First<Entity>();

            var var = entidade.Attributes["pricelevelid"];
            Guid guid = new Guid();
            servico.Update(entidade);

            //foreach (var item in produtos.Entities)
            //{
            //    Entity produtoAtualizado = new Entity("product");
            //    produtoAtualizado.Id = item.Id;
            //    produtoAtualizado.Attributes["statecode"] = new OptionSetValue(1);
            //    produtoAtualizado.Attributes["statuscode"] = new OptionSetValue(2);
            //    servico.Update(produtoAtualizado);

            //}


        }
    }
}
