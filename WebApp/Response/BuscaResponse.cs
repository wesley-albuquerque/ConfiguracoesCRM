using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Reponse
{
    public class BuscaResponse
    {
        public string Message { get; set; }
        public List<object> Object { get; set; }
        public bool Sucess { get; set; }
        public DateTime ExecutionTime { get; set; }
    }
}