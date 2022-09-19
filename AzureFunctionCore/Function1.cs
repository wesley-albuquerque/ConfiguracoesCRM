using System.Net;
using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Query;

namespace NetFxWorker
{
    public class HttpFunction
    {
        private readonly ILogger _logger;

        public HttpFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpFunction>();
        }

        [Function(nameof(HttpFunction))]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string ponteiro = @"AuthType = OAuth;
            Username = nicobelliniAlbuquerque@angelo365.onmicrosoft.com;
            Password = We230398*;
            Url = https://nicobanco.crm2.dynamics.com/;
            AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
            RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
             LoginPrompt = Auto";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            CrmServiceClient service = new CrmServiceClient(ponteiro);


            Entity conta = service.Retrieve("account", new Guid("A16B3F4B-1BE7-E611-8101-E0071B6AF231"),new ColumnSet(true));
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString(conta.Attributes["name"].ToString() + " " + conta.Id );

            return response;
        }
    }
}