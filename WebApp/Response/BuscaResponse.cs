using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Reponse
{
    public class BuscaResponse
    {
        public string Message { get; set; }
        public List<Contact> Object { get; set; }
        public bool Sucess { get; set; }
        public DateTime ExecutionTime { get; set; }
        public BuscaResponse()
        {
            Object = new List<Contact>();
        }
    }
}