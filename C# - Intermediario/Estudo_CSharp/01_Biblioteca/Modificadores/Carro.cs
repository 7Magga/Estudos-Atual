using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Biblioteca
{
    public class Carro : Veiculo
    {
        byte QtdRodas = 4;

        void InfoCarro()
        {
            Marca = "Wolks";
            //protected
            Modelo = "Gol";
            // PRIVATE
            //AnoFabricacao = new DateTime(2021);
            //Internal
            AnoModelo = new DateTime(2022);
        }
    }
}
