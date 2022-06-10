using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WebApp.ClassesCompartilhadas;
using WebApp.Models;

namespace WebApp.Reponse
{
    public class BuscaResponse
    {
        public string Message { get; set; }

        [XmlElement(Type = typeof(Contact))]
        [XmlElement(Type = typeof (Account))]
        [XmlElement(Type = typeof(Product))]
        public List<object> Object { get; set; }
        public bool Sucess { get; set; }
        public DateTime ExecutionTime { get; set; }
        public BuscaResponse()
        {
            Object = new List<object>();

        }
        /// <summary>
        /// tem como entrada uma coleção de dados da entidade para atribuir os valores ao objeto
        /// </summary>
        /// <param name="Entities"></param>
        /// <returns></returns>
        public void InputContactData(DataCollection<Entity> ColecaoDadosEntidade)
        {
            foreach (var item in ColecaoDadosEntidade)
            {
                Contact contato = new Contact()
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
                Object.Add(contato);
                //response.Object.Add(item.GetAttributeValue<string>(request.AttributeName));

            }
        }

        private void InputEnderecoData()
        {

            //Endereco endereco = new Endereco()
            //{
            //    Rua = item.Contains("address1_line1") ? item.Attributes["address1_line1"].ToString() : string.Empty,
            //    Numero = item.Contains("address1_line2") ? item.Attributes["address1_line2"].ToString() : string.Empty,
            //    Cidade = item.Contains("address1_city") ? item.Attributes["address1_city"].ToString() : string.Empty,
            //};
        }
    }
}