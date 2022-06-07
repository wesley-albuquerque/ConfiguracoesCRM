﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

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
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public List<string> HelloWorld(string entradaUsuario, string chave)
        {
            new Validacao(chave);
            string autenticacao = @"AuthType = OAuth;
            Username = wesleyalvesdealbuquerque@wesley356.onmicrosoft.com;
            Password = We230398*;
            Url = https://bancowesley.crm2.dynamics.com/;
  AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
            RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
             LoginPrompt = Auto";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            CrmServiceClient credencial = new CrmServiceClient(autenticacao);

            Entity conta = credencial.Retrieve("account", new Guid("93C71621-BD9F-E711-8122-000D3A2BA2EA"), new ColumnSet("name", "telephone1"));
            QueryExpression parametrosBusca = new QueryExpression("account");
            parametrosBusca.ColumnSet = new ColumnSet(true);
            FilterExpression filtros = new FilterExpression();
            filtros.AddCondition("name", ConditionOperator.Like, "%" + entradaUsuario + "%");
            parametrosBusca.Criteria.AddFilter(filtros);

            EntityCollection resultadoBusca =  credencial.RetrieveMultiple(parametrosBusca);

            List<string> listaNomes = new List<string>();

            foreach(var item in resultadoBusca.Entities)
            {
                listaNomes.Add(item.GetAttributeValue<string>("name"));
                
            }










            return listaNomes;
        }
    }
}
