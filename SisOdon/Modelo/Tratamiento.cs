using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    [Serializable]
    public class Tratamiento
    {
        private Concepto concepto;
        private string fechaExamen;
        public Tratamiento(Concepto concepto, string fechaExamen)
        {
            this.concepto = concepto;
            this.fechaExamen = fechaExamen;
        }

        public Concepto Concepto
        {
            get
            {
                return this.concepto;
            }
            set
            {
                this.concepto = value;
            }
        }
        public string FechaExamen
        {
            get
            {
                return this.fechaExamen;
            }
            set
            {
                this.fechaExamen = value;
            }
        }
    }
}
