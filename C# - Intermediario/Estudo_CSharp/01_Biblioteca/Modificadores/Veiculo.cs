using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Biblioteca
{
    public class Veiculo
    {
        public string Marca;
        protected string Modelo;
        private DateTime AnoFabricacao;
        internal DateTime AnoModelo;
        void InfoVeiculos()
        {
            Marca = "Classe Veiculo";
            Modelo = "Modelo";
            AnoFabricacao = new DateTime(2021);
            AnoModelo = new DateTime(2022);
            Console.WriteLine(Marca);
        }
    }
}
