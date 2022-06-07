using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public class Validacao
    {
        public string StringConnection { get; private set; }

        public Validacao(string chave) 
        {
            StringConnection = chave;
            if (StringConnection != Token)
                throw new Exception("Chave de Validação inválida");

        }

        public const string Token = "12345678";
    }
}