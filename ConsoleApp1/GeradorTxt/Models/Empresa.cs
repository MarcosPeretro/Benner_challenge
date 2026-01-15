using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Models
{
    public class Empresa
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public List<Documento> Documentos { get; set; }
    }
}
