using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        [XmlElement(Type = typeof(Account))]
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
                    Endereco = InputEnderecoData(item)
                };
                Object.Add(contato);
            }
            //response.Object.Add(item.GetAttributeValue<string>(request.AttributeName));

        }
        public void InputProductData(DataCollection<Entity> ColecaoDadosEntidade)
        {
            foreach (var item in ColecaoDadosEntidade)
            {
                Product product = new Product()
                {
                    Descricao = item.Contains("description") ? item.GetAttributeValue<string>("description") : String.Empty,
                    Nome = item.Contains("name") ? item.GetAttributeValue<string>("name") : String.Empty,
                    ProdutoID = item.Contains("productnumber") ? item.Attributes["productnumber"].ToString() : string.Empty,
                    ValidoAPartir = item.Contains("validfromdate") ? (DateTime)item.Attributes["validfromdate"] : new DateTime(1900, 1, 1),
                    ValidoAte = item.Contains("validtodate") ? DateTime.ParseExact(item.Attributes["validtodate"].ToString(), "dd/mm/yyyy hh:mm:ss", CultureInfo.InvariantCulture).Date : new DateTime(1900, 1, 1)
                };

                Object.Add(product);
            }
        }
        public void InputAccountData(DataCollection<Entity> ColecaoDadosEntidade)
        {
            foreach (var item in ColecaoDadosEntidade)
            {

                Account conta = new Account()
                {
                    Name = item.Contains("name") ? item.GetAttributeValue<string>("name") : "",
                    Telephone1 = item.Contains("telephone1") ? item.GetAttributeValue<string>("telephone1") : "",
                    Site = item.Contains("websiteurl") ? item.Attributes["websiteurl"].ToString() : "",
                    ListaPrecoProdutos = item.Contains("defaultpricelevelid") ? item.GetAttributeValue<string>("defaultpricelevelid") : string.Empty,
                    Endereco = InputEnderecoData(item)

                };
            }
        }
        public Endereco InputEnderecoData(Entity EntidadeComEndereco)
        {

            Endereco endereco = new Endereco()
            {
                Rua = EntidadeComEndereco.Contains("address1_line1") ? EntidadeComEndereco.Attributes["address1_line1"].ToString() : string.Empty,
                Numero = EntidadeComEndereco.Contains("address1_line2") ? EntidadeComEndereco.Attributes["address1_line2"].ToString() : string.Empty,
                Cidade = EntidadeComEndereco.Contains("address1_city") ? EntidadeComEndereco.Attributes["address1_city"].ToString() : string.Empty,
            };
            return endereco;
        }
    }
}



