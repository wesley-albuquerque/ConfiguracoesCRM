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
    public class PostOperationProduct : IPlugin
    {
        public IOrganizationService Service { get; set; }

        public void Execute(IServiceProvider serviceProvider)
        {

            try
            {

                var contexto = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
                if ((contexto.MessageName.ToLower() == "create" || contexto.MessageName.ToLower() == "update") && contexto.Mode == (int)Enum.Mode.Synchronous && contexto.Stage == (int)Enum.Stage.PostOperation)
                {
                    var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                    Service = serviceFactory.CreateOrganizationService(contexto.UserId);
                    Entity produto = (Entity)contexto.InputParameters["Target"];
                    Entity itemListaPreco = new Entity("productpricelevel");
                   if (contexto.MessageName.ToLower() == "update")
                    {
                        QueryExpression busca = new QueryExpression("productpricelevel");
                        busca.ColumnSet = new ColumnSet("naru_custo", "amount", "naru_valormensal");
                        FilterExpression filtro = new FilterExpression();
                        filtro.AddCondition("productid", ConditionOperator.Equal, produto.Id);
                        busca.Criteria = filtro;
                        EntityCollection entidades = Service.RetrieveMultiple(busca);
                        itemListaPreco = entidades.Entities.First<Entity>();

                    } 

                    itemListaPreco = CriarItemListaPreco(produto, itemListaPreco);


                    if (contexto.MessageName.ToLower() == "create")

                        Service.Create(itemListaPreco);
                    else 
                        Service.Update(itemListaPreco);
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
        public Entity CriarItemListaPreco(Entity produto, Entity itemListaPreco)
        {
            

            if (produto.Contains("pricelevelid"))
                itemListaPreco.Attributes["pricelevelid"] = (EntityReference)produto.Attributes["pricelevelid"];
            if (produto.Contains("productid"))
                itemListaPreco.Attributes["productid"] = new EntityReference("product", (Guid)produto.Attributes["productid"]);
            if (produto.Contains("naru_custo") && (!itemListaPreco.Contains("naru_custo") || ((Money)itemListaPreco.Attributes["naru_custo"]).Value != ((Money)produto.Attributes["naru_custo"]).Value))
                itemListaPreco.Attributes["naru_custo"] = (Money)produto.Attributes["naru_custo"];
            if (produto.Contains("naru_precoinstalacao") && (!itemListaPreco.Contains("amount") || ((Money)itemListaPreco.Attributes["amount"]).Value != ((Money)produto.Attributes["naru_precoinstalacao"]).Value))
                itemListaPreco.Attributes["amount"] = (Money)produto.Attributes["naru_precoinstalacao"];
            if (produto.Contains("naru_preco") && (!itemListaPreco.Contains("naru_valormensal") || ((Money)itemListaPreco.Attributes["naru_valormensal"]).Value != ((Money)produto.Attributes["naru_preco"]).Value))
                itemListaPreco.Attributes["naru_valormensal"] = (Money)produto.Attributes["naru_preco"];
            if (produto.Contains("defaultuomid"))
                itemListaPreco.Attributes["uomid"] = produto.GetAttributeValue<EntityReference>("defaultuomid");
            itemListaPreco.Attributes["roundingpolicycode"] = new OptionSetValue(1);

            return itemListaPreco;
        }
        //

    }
}
