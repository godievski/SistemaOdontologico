using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    [Serializable]
    public class Odontograma
    {
        /*La ubicación se maneja por los indices*/
        private Diente[] dientes;
        public Odontograma ()
        {
            dientes = new Diente[32];
            for (int i = 0; i < 32; i++)
            {
                if (dientes[i] == null) break;
                dientes[i].Sectores = 4;
                if (i < 8)
                    dientes[i].Tipo ="Incisivo";
                else if (i < 12)
                    dientes[i].Tipo ="Canino";
                else if (i < 20)
                    dientes[i].Tipo ="Premolares";
                else if (i < 28)
                    dientes[i].Tipo ="Molares";
                else if (i < 32)
                    dientes[i].Tipo = "Juicio";
                else
                    dientes[i].Tipo = "Ninguno";
            }
        }
        public string tipoDiente (int ubicacion)
        {
            int i = ubicacion - 1;
            if (i >= 0)
                return dientes[i].Tipo;
            else
                return "Ubicacion inválida";
        }
        public void modificarDiente (int ubicacion, string tipo)
        {
            int i = ubicacion - 1;
            if (i >= 0 && i < 32)
                dientes[i].Tipo = tipo;
        }
    }
}
