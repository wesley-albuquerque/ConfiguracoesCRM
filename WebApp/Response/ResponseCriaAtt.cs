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

        public ResponseCriaAtt(string message, bool success)
        {
            Sucess = success;
            Message = message;
        }
    }
}