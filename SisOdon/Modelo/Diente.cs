using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    [Serializable]
    public class Diente
    {
        private string tipo;
        private int sectores;

        public string Tipo
        {
            set
            {
                tipo = value;
            }
            get
            {
                return tipo;
            }
        }
        public int Sectores
        {
            set
            {
                sectores = value;
            }
            get
            {
                return sectores;
            }
        }
        
    }
}
