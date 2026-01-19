using ConsoleApp1.GeradorTxt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt
{
    public interface IGeradorArquivo
    {
        void GeraTxt(List<Empresa> empresas, string outputPath);
    }
}
