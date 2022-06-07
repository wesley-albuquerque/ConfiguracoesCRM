using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.ClassesCompartilhadas;

namespace WebApp.Models
{
    public class Contact
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }
    }
}