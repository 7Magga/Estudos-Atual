using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Biblioteca
{
    public class Caminhao : Veiculo
    {
        byte QtdRodas = 16;

        void InfoCaminhao()
        {
            Marca = "SCANIA";
            Modelo = "1520";
            // PRIVATE
            //AnoFabricacao = new DateTime(2021);
            //Internal
            AnoModelo = new DateTime(2022);
        }
    }
}
