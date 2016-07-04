using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    [Serializable]
    public class Sede
    {
        private string direccion;
        private string departamento;
        private string provincia;
        private string distrito;
        private Odontologo jefe;
        private string fechaApertura;

        public Sede(string direccion, string departamento, string provincia,
                    string distrito, Odontologo jefe, string fechaApertura)
        {
            this.direccion = direccion;
            this.departamento = departamento;
            this.provincia = provincia;
            this.distrito = distrito;
            this.jefe = jefe;
            this.fechaApertura = fechaApertura;
        }
        public string Direccion
        {
            get
            {
                return direccion;
            }
            set
            {
                direccion = value;
            }
        }
        public string Departamento
        {
            get
            {
                return departamento;
            }
            set
            {
                departamento = value;
            }
        }
        public string Provincia
        {
            get
            {
                return provincia;
            }
            set
            {
                provincia = value;
            }
        }
        public string Distrito
        {
            get
            {
                return distrito;
            }
            set
            {
                distrito = value;
            }
        }
        public Odontologo Jefe
        {
            get
            {
                return jefe;
            }
            set
            {
                jefe = value;
            }
        }
        public string FechaApertura
        {
            get
            {
                return fechaApertura;
            }
            set
            {
                fechaApertura = value;
            }
        }
    }
}
