using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    [Serializable]
    public class Persona
    {
        private int dni;
        private string nombre;
        private string apPat; /*Apellido paterno*/
        private string apMat; /*Apellido materno*/
        private string fecha; /*Fecha de nacimiento*/
        private string sexo;
        private string dir; /*Dirección*/

        public Persona (int dni, String nombre, String apPat, String apMat, String fecha, String sexo, String dir)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apPat = apPat;
            this.apMat = apMat;
            this.fecha = fecha;
            this.sexo = sexo;
            this.dir = dir;
        }
        public int Dni
        {
            get
            {
                return dni;
            }
            set
            {
                dni = value;
            }
        }
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
        public string ApPat
        {
            set
            {
                apPat = value;
            }
            get
            {
                return apPat;
            }
        }
        public string ApMat
        {
            set
            {
                apMat = value;
            }
            get
            {
                return apMat;
            }
        }
        public string Fecha
        {
            set
            {
                fecha = value;
            }
            get
            {
                return fecha;
            }
        }
        public string Sexo
        {
            set
            {
                sexo = value;
            }
            get
            {
                return sexo;
            }
        }
        public string Direccion
        {
            set
            {
                dir = value;
            }
            get
            {
                return dir;
            }
        }

    }
}
