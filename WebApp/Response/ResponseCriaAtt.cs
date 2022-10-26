using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Response
{
    public class ResponseCriaAtt
    {
        public bool Sucess { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// Construtor vazio para serialização do WebService
        /// </summary>
        public ResponseCriaAtt() { }
        
        /// <summary>
        /// Construtor de resposta EndPoint
        /// </summary>
        /// <param name="message"></param>
        /// <param name="success"></param>
        public ResponseCriaAtt(string message, bool success)
        {
            Sucess = success;
            Message = message;
        }
    }
}