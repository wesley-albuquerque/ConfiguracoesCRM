using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class PostOperationItemPrice : IPlugin
    {
        public IOrganizationService Service { get; set; }
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                IPluginExecutionContext contexto = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                if (contexto.MessageName.ToLower() == "update")
                {
                    IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                    Service = serviceFactory.CreateOrganizationService(contexto.UserId);
                    Entity itemListaPreco = contexto.InputParameters["Target"] as Entity;

                    var fetchXml = $@"<?xml version=""1.0"" encoding=""utf-16""?>
<fetch top=""1"">
  <entity name=""product"">
    <attribute name=""naru_custo"" />
    <attribute name=""naru_preco"" />
    <attribute name=""naru_precoinstalacao"" />
    <link-entity name=""productpricelevel"" from=""productid"" to=""productid"">
      <filter>
        <condition attribute=""productpricelevelid"" operator=""eq"" value=""{itemListaPreco.Id}"" />
      </filter>
    </link-entity>
  </entity>
</fetch>";

                    EntityCollection listaProdutos = Service.RetrieveMultiple(new FetchExpression(fetchXml));
                    Entity produto = listaProdutos.Entities.FirstOrDefault();

                    //produto.Attributes["productid"] = itemListaPreco.GetAttributeValue<EntityReference>("productid");
                    //if (itemListaPreco.Contains("naru_custo") && (Money)produto.Attributes["naru_custo"] != (Money)itemListaPreco.Attributes["naru_custo"])
                    //    produto.Attributes["naru_custo"] = (Money)itemListaPreco.Attributes["naru_custo"];
                    //if (itemListaPreco.Contains("amount") && (Money)produto.Attributes["naru_precoinstalacao"] != (Money)itemListaPreco.Attributes["amount"])
                    //    produto.Attributes["naru_precoinstalacao"] = (Money)itemListaPreco.Attributes["amount"];
                    if (itemListaPreco.Contains("naru_valormensal") && (Money)produto.Attributes["naru_preco"] != (Money)itemListaPreco.Attributes["naru_valormensal"])
                        produto.Attributes["naru_preco"] = (Money)itemListaPreco.Attributes["naru_valormensal"];



                    Service.Update(produto);


                }

            }
            catch (FaultException<OrganizationServiceFault> f)
            {
                throw new InvalidPluginExecutionException(string.Format("Erro: {0} Detalhe: {1}", f.Message, f.Detail.Message));
            }
            catch (InvalidPluginExecutionException i)
            {
                throw new InvalidPluginExecutionException(i.Message);
            }
            catch (Exception e)
            {
                throw new InvalidPluginExecutionException(e.Message, e);
            }
        }
    }
}
