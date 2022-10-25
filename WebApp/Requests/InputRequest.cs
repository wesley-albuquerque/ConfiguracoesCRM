using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Requests
{
    public class InputRequest : BuscaRequest
    {
        public string Nome { get; set; }
        public string CPF_CNPJ { get; set; }
        public string Email { get; set; }
        public string CEP { get; set; }

    }
}