using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    [Serializable]
    public class HistorialSede
    {
        Sede sede;
        private string fechaInicio;
        private string fechaFin;

        public HistorialSede(Sede sede, string fI, string fF){
            this.sede = sede;
            this.fechaFin = fF;
            this.fechaInicio = fI;
        }
        public Sede Sede
        {
            get
            {
                return this.sede;
            }
            set
            {
                this.sede = value;
            }
        }
        public string FechaInicio
        {
            get
            {
                return this.fechaInicio;
            }
            set
            {
                this.fechaInicio = value;
            }
        }
        public string FechaFin
        {
            get
            {
                return this.FechaFin;
            }
            set
            {
                this.fechaFin = value;
            }
        }
    }
}
