using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    [Serializable]
    public class Odontologo : Persona
    {
        private string esp; /*Especialidad del odontólogo*/
        private string univ; /*Universidad*/
        private Sede sedeActual;
        private List<HistorialSede> historialSedes;

        public Odontologo(string esp, string univ, int dni, string nombre, string apPat, string apMat, string fecha, string sexo, string dir, Sede sede, string fechaInicio)
            : base(dni, nombre, apPat, apMat, fecha, sexo, dir)
        {
            this.esp = esp;
            this.univ = univ;
            this.sedeActual = sede;
            this.historialSedes = new List<HistorialSede>();
            HistorialSede historialSede = new HistorialSede(sede, fechaInicio, "");
            if (sede != null)
                historialSedes.Add(historialSede);
        }
        public string Especialidad
        {
            set
            {
                esp = value;
            }
            get
            {
                return esp;
            }
        }

        public string Universidad
        {
            set
            {
                univ = value;
            }
            get
            {
                return univ;
            }
        }
        public HistorialSede this[int index]
        {
            get
            {
                return this.historialSedes[index];
            }
            set
            {
                this.historialSedes[index] = value;
            }
        }
        public Sede SedeActual
        {
            set
            {
                this.sedeActual = value;
            }
            get
            {
                return this.sedeActual;
            }
        }
    }
}
