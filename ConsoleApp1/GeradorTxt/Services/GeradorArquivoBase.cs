using ConsoleApp1.GeradorTxt;
using ConsoleApp1.GeradorTxt.Enum;
using ConsoleApp1.GeradorTxt.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace GeradorTxt
{
    /// <summary>
    /// Implementa a geração do Leiaute 1.
    /// IMPORTANTE: métodos NÃO marcados como virtual de propósito.
    /// O candidato deve decidir onde permitir override para suportar versões futuras.
    /// </summary>
    public class GeradorArquivoBase : IGeradorArquivo
    {
        protected readonly Dictionary<TipoLinha, int> qtdLinhas;
        public GeradorArquivoBase()
        {
            qtdLinhas = new Dictionary<TipoLinha, int>
            {
                { TipoLinha.Tipo00, 0 },
                { TipoLinha.Tipo01, 0 },
                { TipoLinha.Tipo02, 0 },
            };
        }

        public void GeraTxt(List<Empresa> empresas, string outputPath)
        {
            var sb = new StringBuilder();
            foreach (var emp in empresas)
            {
                ProcessaEmpresa(sb, emp);
            }

            EscreveTipo09(sb);
            ExibeQuantidadeTotalLinhas(sb);
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }
        
        protected virtual void ProcessaEmpresa(StringBuilder sb, Empresa emp)
        {

            EscreveTipo00(sb, emp);
            ContaLinha(TipoLinha.Tipo00);
            foreach (var doc in emp.Documentos)
            {
                ProcessaDocumento(sb, doc);
            }
        }
        protected virtual void ProcessaDocumento(StringBuilder sb, Documento doc)
        {
            var valorItens = doc.Itens.Sum(i => i.Valor);
            ValidaValorTotal(doc.Valor, valorItens);

            EscreveTipo01(sb, doc);
            ContaLinha(TipoLinha.Tipo01);

            foreach (var item in doc.Itens)
            {
                ProcessaItem(sb, item);
            }
        }

        protected virtual void ProcessaItem(StringBuilder sb, ItemDocumento item)
        {
            EscreveTipo02(sb, item);
            ContaLinha(TipoLinha.Tipo02);
        }

        protected string ToMoney(decimal val)
        {
            // Força ponto como separador decimal, conforme muitos leiautes.
            return val.ToString("0.00", CultureInfo.InvariantCulture);
        }

        protected void EscreveTipo00(StringBuilder sb, Empresa emp)
        {
            // 00|CNPJEMPRESA|NOMEEMPRESA|TELEFONE
            sb.Append("00|")
              .Append(emp.CNPJ).Append("|")
              .Append(emp.Nome).Append("|")
              .Append(emp.Telefone).AppendLine();
        }

        protected decimal EscreveTipo01(StringBuilder sb, Documento doc)
        {
            // 01|MODELODOCUMENTO|NUMERODOCUMENTO|VALORDOCUMENTO
            sb.Append("01|")
              .Append(doc.Modelo).Append("|")
              .Append(doc.Numero).Append("|")
              .Append(ToMoney(doc.Valor)).AppendLine();
            return doc.Valor;
        }

        protected decimal EscreveTipo02(StringBuilder sb, ItemDocumento item)
        {
            // 02|DESCRICAOITEM|VALORITEM
            sb.Append("02|");

               EscreveCamposTipo02(sb, item);

             sb.Append("|")
               .Append(ToMoney(item.Valor)).AppendLine();
            return item.Valor;
        }

        protected virtual void EscreveCamposTipo02(StringBuilder sb, ItemDocumento item)
        {
            sb.Append(item.Descricao);
        }

        protected void ValidaValorTotal(decimal valorDocumento, decimal valorItens)
        {
            if (valorDocumento != valorItens)
            {
                throw new Exception("Valor total entre documento e itens discrepante");
            }
        }

        protected virtual void EscreveTipo09(StringBuilder sb)
        {
            /* 
             09|00|QUANTIDADE_LINHAS_DO_TIPO_00
             09|01|QUANTIDADE_LINHAS_DO_TIPO_00
             09|02|QUANTIDADE_LINHAS_DO_TIPO_00
            */
            sb.Append("09|00|").Append(qtdLinhas[TipoLinha.Tipo00]).AppendLine()
              .Append("09|01|").Append(qtdLinhas[TipoLinha.Tipo01]).AppendLine()
              .Append("09|02|").Append(qtdLinhas[TipoLinha.Tipo02]).AppendLine();
        }

        protected void ExibeQuantidadeTotalLinhas(StringBuilder sb)
        {
            // 99|QUANTIDADE_LINHAS_NO_ARQUIVO
            sb.Append("99|").Append(qtdLinhas.Values.Sum() + qtdLinhas.Count); 
            // não sei se é necessario adicionar os contadores de linhas na contagem de linhas
        }

        protected void ContaLinha(TipoLinha tipo)
        {
            qtdLinhas[tipo]++;
        }
    }
}
