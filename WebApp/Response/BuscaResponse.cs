using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WebApp.Models;

namespace WebApp.Reponse
{
    public class BuscaResponse
    {
        public string Message { get; set; }

        [XmlElement(Type = typeof(Contact))]
        [XmlElement(Type = typeof (Account))]
        public List<object> Object { get; set; }
        public bool Sucess { get; set; }
        public DateTime ExecutionTime { get; set; }
        public BuscaResponse()
        {
            Object = new List<object>();
        }
    }
}