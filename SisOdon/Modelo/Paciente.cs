using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    [Serializable]
    public class Paciente : Persona
    {
        private Odontograma odont;
        private List<Tratamiento> tratamientos;

        public Paciente(int dni, string nombre, string apPat, string apMat, string fecha, string sexo, string dir)
            : base(dni, nombre, apPat, apMat, fecha, sexo, dir)
        {
            odont = new Odontograma();
            tratamientos = new List<Tratamiento>();
        }
        public Odontograma Odont
        {
            set
            {
                odont = value;
            }
            get
            {
                return odont;
            }
        }

        public Tratamiento this[int index]
        {
            get
            {
                return this.tratamientos[index];
            }
            set
            {
                this.tratamientos[index] = value;
            }
        }

        public void AddTratamiento(Tratamiento tratamiento)
        {
            tratamientos.Add(tratamiento);
        }

        public int CountTratamiento()
        {
            return this.tratamientos.Count;
        }
        public List<Tratamiento> ObtenerListaTratamiento()
        {
            return this.tratamientos;
        }
        public void CargarListaTratamiento(List<Tratamiento> listaTratamiento)
        {
            this.tratamientos = listaTratamiento;
        }
    }
}
