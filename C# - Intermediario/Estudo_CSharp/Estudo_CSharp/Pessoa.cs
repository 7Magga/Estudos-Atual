using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estudo_CSharp
{
    class Pessoa
    {
        public string Nome;
        public string Sexo;
        public DateTime DtNascimento;
        public double PosicaoX, PosicaoY;

        public Pessoa(string nome, string sexo, DateTime dtNascimento, double posicaoX,double posicaoY)
        {
            Nome = nome;
            Sexo = sexo;
            DtNascimento = dtNascimento;
            PosicaoX = posicaoX; 
            PosicaoY = posicaoY;
        }
        public Pessoa(){}
        public string ResumoPessoa()
        {
            string Resumo = $"Nome: {Nome} \nSexo: {Sexo} \nDtNascimento {DtNascimento}"; 
            return Resumo;
        }

        public Double[] GetPosicao()
        {
            Double[] Posicoes = new Double[]
            {
                PosicaoX,
                PosicaoY
            };

            return Posicoes;
        } 

        public void SetPosicao(double x, double y)
        {
            PosicaoX = x;
            PosicaoY = y;
        }
    }
}
