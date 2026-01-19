using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Models
{
    public class ItemDocumento
    {
        public int? NumeroItem { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public List<CategoriaItem> Categorias { get; set; }
    }
}
