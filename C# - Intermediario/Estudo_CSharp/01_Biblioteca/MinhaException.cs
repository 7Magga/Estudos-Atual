using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Biblioteca
{
    public class MinhaException : Exception
    {
        string LinhaDoErro;
        public MinhaException(string Linha):base("Minha Exception")
        {
            LinhaDoErro = Linha;
        }
    }
}
