using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoEngenhariaSoftware.Models
{
    public class OngClass
    {
        public int id { get; set; }
        public string CNPJ { get; set; }
        public string nome { get; set; }
        public int MyProperty { get; set; }
        public List<PetClass> Pets { get; set; }

    }
}