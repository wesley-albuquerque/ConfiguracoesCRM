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
            string ponteiro = "string connection";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            CrmServiceClient servico = new CrmServiceClient(ponteiro);

            //Entity unidade = servico.Retrieve("uom", new Guid("98277211-f499-4838-8b32-9abbe41d6d0f"), new ColumnSet(true));
            //var teste = ((EntityReference)unidade.Attributes["uomscheduleid"]).Id;
            //unidade.Attributes["uomscheduleid"] = null;
            //servico.Update(unidade);

            //servico.Delete("uom", unidade.Id);

            QueryExpression expressao = new QueryExpression("product");


            expressao.ColumnSet = new ColumnSet(true);
            FilterExpression filtro = new FilterExpression(LogicalOperator.And);
            filtro.AddCondition("statecode", ConditionOperator.Equal, 0);
            filtro.AddCondition("statuscode", ConditionOperator.Equal, 1);
            expressao.Criteria = filtro;
            EntityCollection produtos = servico.RetrieveMultiple(expressao);

            //Entity entidade = produtos.Entities.First<Entity>();

            //var var = entidade.Attributes["pricelevelid"];
            //Guid guid = new Guid();
            //servico.Update(entidade);

            foreach (var item in produtos.Entities)
            {
                Entity produtoAtualizado = new Entity("product");
                produtoAtualizado.Id = item.Id;
                produtoAtualizado.Attributes["statecode"] = new OptionSetValue(1);
                produtoAtualizado.Attributes["statuscode"] = new OptionSetValue(2);
                servico.Update(produtoAtualizado);

            }


        }
    }
}
