using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.ClassesCompartilhadas;

namespace WebApp.Models
{
    public class Account
    {
        public string Name { get; set; }
        public string Telephone1  { get; set; }
        public Endereco Endereco { get; set; }
    }
}