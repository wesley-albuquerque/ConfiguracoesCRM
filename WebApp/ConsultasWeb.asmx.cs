using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WebApp.ClassesCompartilhadas;
using WebApp.Models;
using WebApp.Reponse;
using WebApp.Requests;

namespace WebApp
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ConsultasWeb : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public BuscaResponse ConsultaCRM(BuscaRequest request)
        {
            try
            {
                new Validacao(request.Chave);
                string autenticacao = @"AuthType = OAuth;
            Username = wesleyalvesdealbuquerque@wesley356.onmicrosoft.com;
            Password = We230398*;
            Url = https://bancowesley.crm2.dynamics.com/;
  AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
            RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
             LoginPrompt = Auto";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                CrmServiceClient credencial = new CrmServiceClient(autenticacao);

                //Entity conta = credencial.Retrieve("account", new Guid("93C71621-BD9F-E711-8122-000D3A2BA2EA"), new ColumnSet("name", "telephone1"));
                QueryExpression parametrosBusca = new QueryExpression(request.EntityName);
                parametrosBusca.ColumnSet = new ColumnSet(true);
                FilterExpression filtros = new FilterExpression();
                filtros.AddCondition(request.AttributeName, ConditionOperator.Like, "%" + request.FiltroBusca + "%");
                parametrosBusca.Criteria.AddFilter(filtros);

                EntityCollection resultadoBusca = credencial.RetrieveMultiple(parametrosBusca);

                BuscaResponse response = new BuscaResponse();

                //response.Object = new Contact();
                Contact contato = new Contact();
                foreach (var item in resultadoBusca.Entities)
                {
                    contato = new Contact()
                    {
                        Cargo = item.Contains("jobtitle") ? item.GetAttributeValue<string>("jobtitle") : string.Empty,
                        Nome = item.Contains("firstname") ? item.Attributes["firstname"].ToString() : string.Empty,
                        Sobrenome = item.Contains("lastname") ? item.Attributes["lastname"].ToString() : string.Empty,
                        Email = item.Contains("emailaddress1") ? item.Attributes["emailaddress1"].ToString() : string.Empty,
                        Endereco = new Endereco()
                        {
                            Rua = item.Contains("address1_line1") ? item.Attributes["address1_line1"].ToString() : string.Empty,
                            Numero = item.Contains("address1_line2") ? item.Attributes["address1_line2"].ToString() : string.Empty,
                            Cidade = item.Contains("address1_city") ? item.Attributes["address1_city"].ToString() : string.Empty,
                        }
                    };
                    response.Object.Add(contato);
                    //response.Object.Add(item.GetAttributeValue<string>(request.AttributeName));

                }










                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
