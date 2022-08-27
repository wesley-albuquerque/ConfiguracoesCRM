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
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                var contexto = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
                if ((contexto.MessageName.ToLower() == "create" || contexto.MessageName.ToLower() == "update") && contexto.Mode == (int)Enum.Mode.Synchronous && contexto.Stage == (int)Enum.Stage.PostOperation)
                {
                    var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                    var service = serviceFactory.CreateOrganizationService(contexto.UserId);
                    Entity produto = (Entity)contexto.InputParameters["Target"];
                    Entity itemListaPreco = new Entity("productpricelevel");


                    itemListaPreco.Attributes["pricelevelid"] = (EntityReference)produto.Attributes["pricelevelid"];
                    itemListaPreco.Attributes["productid"] = (EntityReference)produto.Attributes["productid"];
                    itemListaPreco.Attributes["naru_custo"] = (Money)produto.Attributes["naru_custo"];
                    itemListaPreco.Attributes["naru_valordainstalacao"] = (Money)produto.Attributes["naru_precoinstalacao"];
                    itemListaPreco.Attributes["naru_valormensal"] = (Money)produto.Attributes["naru_preco"];
                    itemListaPreco.Attributes["roundingpolicycode"] = new OptionSetValue(1);

                    service.Create(itemListaPreco);
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
