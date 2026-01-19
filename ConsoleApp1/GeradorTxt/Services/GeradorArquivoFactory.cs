using GeradorTxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GeradorTxt.Services
{
    public static class GeradorArquivoFactory
    {
        public static IGeradorArquivo Criar(int versão)
        {
            switch (versão)
            {
                case 1:
                    return new GeradorArquivoBase();
                case 2:
                    return new GeradorArquivoV2();
                default:
                    throw new Exception("versão inválida");
            }
        }
    }
}
