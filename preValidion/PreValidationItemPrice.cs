using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class PreValidationItemPrice : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                IPluginExecutionContext contexto = (IPluginExecutionContext)serviceProvider.GetService(typeof (IPluginExecutionContext));
                if (contexto.MessageName.ToLower() == "update" && contexto.Stage == (int)Enum.Stage.PreValidation && contexto.Mode == (int)Enum.Mode.Synchronous)
                {
                    
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
