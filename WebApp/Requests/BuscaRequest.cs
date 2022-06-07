using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Requests
{
    public class BuscaRequest
    {
        public string EntityName { get; set; }
        public string FiltroBusca { get; set; }
        public string Chave { get; set; }
        public string AttributeName { get; set; }
    }
}