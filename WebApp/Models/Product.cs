using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Product
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ProdutoID { get; set; }
        public DateTime ValidoAte { get; set; }
        public DateTime ValidoAPartir {get;set; }
    }
}