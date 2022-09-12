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
    public class postOperationProduct : IPlugin
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
                   
                    Entity itemListaPreco = CriarItemListaPreco(produto);


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
        public Entity CriarItemListaPreco(Entity produto)
        {
            Entity itemListaPreco = new Entity("productpricelevel");

            QueryExpression busca = new QueryExpression("uom");
            busca.ColumnSet = new ColumnSet("uomid","name");
            //ColumnSet colunas = new ColumnSet("uomid");
            var unidade = Service.RetrieveMultiple(busca);


            if (produto.Contains("pricelevelid"))
                itemListaPreco.Attributes["pricelevelid"] = (EntityReference)produto.Attributes["pricelevelid"];
            if (produto.Contains("productid"))
                itemListaPreco.Attributes["productid"] = new EntityReference("product", (Guid)produto.Attributes["productid"]);
            if (produto.Contains("naru_custo"))
                itemListaPreco.Attributes["naru_custo"] = (Money)produto.Attributes["naru_custo"];
            if (produto.Contains("naru_precoinstalacao"))
                itemListaPreco.Attributes["amount"] = (Money)produto.Attributes["naru_precoinstalacao"];
            if (produto.Contains("naru_preco"))
                itemListaPreco.Attributes["naru_valormensal"] = (Money)produto.Attributes["naru_preco"];
            if (produto.Contains("defaultuomid"))
                itemListaPreco.Attributes["uomid"] = new EntityReference("uom", (produto.GetAttributeValue<EntityReference>("defaultuomid")).Id);
            itemListaPreco.Attributes["roundingpolicycode"] = new OptionSetValue(1);

            return itemListaPreco;
        }
        //

    }
}
