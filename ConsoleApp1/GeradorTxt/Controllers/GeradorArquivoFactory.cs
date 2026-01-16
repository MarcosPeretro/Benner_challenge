using GeradorTxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Controllers
{
    public static class GeradorArquivoFactory
    {
        public static IGeradorArquivo Criar(int versão)
        {
            switch (versão)
            {
                case 2:
                    return new GeradorArquivoV2();
                default:
                    return new GeradorArquivoBase();
            }
        }
    }
}
