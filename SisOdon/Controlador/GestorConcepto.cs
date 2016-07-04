using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SisOdon.Modelo;
namespace SisOdon.Controlador
{
    class GestorConcepto
    {
        private List<Concepto> listaConcepto;

        public GestorConcepto()
        {
            this.listaConcepto = new List<Concepto>();
        }
        public Concepto this[int index]
        {
            get
            {
                if (index < this.listaConcepto.Count)
                {
                    return this.listaConcepto[index];
                }
                else return null;
            }
            set
            {
                if (index < this.listaConcepto.Count)
                    this.listaConcepto[index] = value;
            }
        }
        public int Count
        {
            get
            {
                return this.listaConcepto.Count;
            }
        }
        public void CargarConceptosArchivo()
        {
            //CARGAR ARCHIVO UNIVERSIDADES
            FileStream fileConcepto = new FileStream("Conceptos.txt", FileMode.Open, FileAccess.Read);
            StreamReader lector = new StreamReader(fileConcepto);
            while (true)
            {
                string linea = lector.ReadLine();
                if (linea == null) break;
                string[] palabras = new string[2];
                char[] separador = { ',' };
                palabras = linea.Split(separador);
                string nombre = palabras[0];
                double costo = double.Parse(palabras[1]);
                this.listaConcepto.Add(new Concepto(nombre, costo));
            }
            lector.Close();
            fileConcepto.Close();
        }
    }
}
