using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using WebApp.ClassesCompartilhadas;
using WebApp.Models;
using WebApp.Reponse;
using WebApp.Requests;
using WebApp.Response;

namespace WebApp
{
    /// <summary>
    /// Summary description for CriaAttConta
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CriaAttConta : System.Web.Services.WebService
    {

        [WebMethod]
        public ResponseCriaAtt CriaAttConta2(Account request)
        {
            try
            {
                DateTime inicio = DateTime.Now;

                new Validacao(request.Chave);

                Conexao conexao = new Conexao();
                conexao.Autentica();
                ResponseCriaAtt response = new ResponseCriaAtt("ok", true);









                return response;
            }
            catch (Exception ex)
            {
                throw ex;
                //return response;
            }
        }
    }
}
