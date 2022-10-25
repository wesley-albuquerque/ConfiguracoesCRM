using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApp.ClassesCompartilhadas
{
    public class Conexao 
    {
        public CrmServiceClient Service { get; private set; }   

        public void Autentica()
        {
            string clientSecret = ConfigurationManager.AppSettings["ClienSecret"];
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string url = ConfigurationManager.AppSettings["url"];
            string autenticacao = @"AuthType=ClientSecret;
url=" + url + @";
ClientId=" + clientId + @";
ClientSecret=" + clientSecret;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Service = new CrmServiceClient(autenticacao);
        }



        
    }
}
//          string autenticacao = @"AuthType=ClientSecret;
//url=https://nicobanco.crm2.dynamics.com/;
//ClientId=23cf9f49-47d6-475b-a2ff-a0524af31ea6;
//ClientSecret=Y238Q~LTnXacV6C3srsNOjzqDNlmP4vYZbnNoduV";