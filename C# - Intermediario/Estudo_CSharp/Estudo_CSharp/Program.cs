using _01_Biblioteca;
using System;
using System.Linq;

namespace Estudo_CSharp
{
    class Program  : ProgramBase
    {
        string Nome;

        static void Main(string[] args)
        {
            #region Pessoa
            Pessoa pessoa = new Pessoa();
            pessoa.Nome = "Matheus";
            pessoa.Sexo = "MSC";
            pessoa.DtNascimento = new DateTime(2001,04,30);
            //pessoa.NomeRead = "";
            var _nome = pessoa.NomeRead;

            var retorno = pessoa.ResumoPessoa();
            
            Console.WriteLine(retorno);
            Console.ReadKey();

            Pessoa pessoa1 = new Pessoa()
            {
                Nome = "Matheus",
                Sexo = "MSC",
                DtNascimento = new DateTime(2001, 04, 30)
            };
            pessoa1.SetPosicao(20.5,10);
            var teste = pessoa1.GetPosicao().ToList();
            
            foreach (var item in teste)
                Console.WriteLine($"\nPosição: {item}");
            Console.ReadKey();
            #endregion

            #region Soma
            Calculos calculos = new Calculos();
            var calculo = calculos.Soma(1,4);
            Console.WriteLine($"\nSoma: {calculo}");
            Console.ReadKey();
            #endregion

            #region Modificadores
            Veiculo veiculo = new Veiculo();
            Carro carro = new Carro();
            Caminhao caminhao = new Caminhao();

            veiculo.Marca = "";
            carro.Marca = "Fiat";
            caminhao.Marca = "Volvo";

            //Não é possivel acessar
            //veiculo.Modelo = "";
            //carro.Modelo = "";
            //caminhao.Modelo = "";

            #endregion

            #region Encapsulamento
            PessoaEncap pessoa2 = new PessoaEncap();
            pessoa2.setNome("       Teste");
            string nome = pessoa2.getNome();
            #endregion

            #region Exception
            try
            {
                throw new MinhaException("10");
            }
            catch (MinhaException e)
            {
                Console.WriteLine($"\n{e.Message}");
            }
            Console.ReadKey();
            #endregion

            #region Boxing e Unboxing
            //Boxing
            int a = 10;
            object b = a;

            //Unboxing
            int _a = (int)b;
            #endregion
        }

        void SetNome(string Nome)
        {
            this.Nome = Nome;
        }

        void SetNomeBase(string Nome)
        {
            this.Nome = base.SetNome(Nome);
        }
    }
}
