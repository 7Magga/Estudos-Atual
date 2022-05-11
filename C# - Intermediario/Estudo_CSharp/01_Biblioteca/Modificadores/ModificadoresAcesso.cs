using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Biblioteca
{
    class ModificadoresAcesso
    {
        //Pode ser visivel a todos, inclusive a outros projetos
        public string ModificadorPublic;

        //Visivel a classe que criou e a classe derivada
        protected string ModificadorProtec;

        //Visivel apenas dentro da classe
        private string ModificadorPrivate;

        //Visivel apenas dentro do projeto
        internal string ModificadorInternal;

        //Pode ser acessado por Metodos que referenciam a biblioteca
        //protected internal ModificadorInternalProtected;
    }
}
