using ConsoleApp1.GeradorTxt.Enum;
using ConsoleApp1.GeradorTxt.Models;
using GeradorTxt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt
{
    internal class GeradorArquivoV2 : GeradorArquivoBase
    {
        public GeradorArquivoV2() : base()
        {
            qtdLinhas.Add(TipoLinha.Tipo03, 0);
        }

        protected override void ProcessaItem(StringBuilder sb, ItemDocumento item)
        {

            base.ProcessaItem(sb, item);

            foreach (var categoria in item.Categorias)
            {
                EscreverTipo03(sb,categoria);
                qtdLinhas[TipoLinha.Tipo03]++;
            }
        }

        protected override void EscreveCamposTipo02(StringBuilder sb, ItemDocumento item)
        {
            sb.Append(item.NumeroItem)
               .Append("|");
            base.EscreveCamposTipo02(sb, item);
        }

        protected void EscreverTipo03(StringBuilder sb, CategoriaItem cat)
        {
            // 03|NUMEROCATEGORIA|DESCRICAOCATEGORIA
            sb.Append("03|")
              .Append(cat.NumeroCategoria).Append("|")
              .Append(cat.DescricaoCategoria).AppendLine();

        }

        protected override void EscreverTipo09(StringBuilder sb)
        {
            base.EscreverTipo09(sb);

            sb.Append("09|03|").Append(qtdLinhas[TipoLinha.Tipo03]).AppendLine();
        }
    }
}
