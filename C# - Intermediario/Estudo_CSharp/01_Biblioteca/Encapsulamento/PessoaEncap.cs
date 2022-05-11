using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Biblioteca
{
    public class PessoaEncap
    {
        private string Nome;
        string CEP;
        string Endereco;
        string CPF;

        public string setNome(string nome)
        {
            return Nome = nome.Trim().ToLower();
        }

        public string getNome()
        {
            return Nome.ToUpper();
        }
    }
}
