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
    internal class PostOperationOportunity : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                IPluginExecutionContext contexto = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                if (contexto.MessageName.ToLower() == "update" && contexto.Mode == (int)Enum.Mode.Synchronous && contexto.Stage == (int)Enum.Stage.PostOperation)
                {
                    IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                    IOrganizationService servico = serviceFactory.CreateOrganizationService(contexto.UserId);
                    Entity oportunidade = contexto.InputParameters["Target"] as Entity;
                    if (((OptionSetValue)oportunidade.Attributes["status code"]).Value == 3)
                    {
                        QueryExpression busca = new QueryExpression("naru_proposta");
                        busca.ColumnSet = new ColumnSet(true);
                        EntityCollection entidades = servico.RetrieveMultiple(busca);
                        Entity proposta = entidades.Entities.First<Entity>();
                        var oportunityID = new EntityReference("oportunity", oportunidade.Id);
                        var contaID = (EntityReference)oportunidade.Attributes["parentaccountid"];


                    }



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
