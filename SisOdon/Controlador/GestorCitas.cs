using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using SisOdon.Modelo;

namespace SisOdon.Controlador
{
    public class GestorCitas
    {
        private List<Cita> citas;
        
        //CONSTANTES EN MINUTOS
        public static int HORA_MIN = 8 * 60;
        public static int HORA_MAX = 18 * 60;

        public GestorCitas()
        {
            this.citas = new List<Cita>();
        }
        public Cita this[int index]
        {
            get
            {
                return this.citas[index];
            }
            set
            {
                this.citas[index] = value;
            }
        }
        public void AgregarCita(Cita cita)
        {
            this.citas.Add(cita);   
        }
        public void RegistrarCita(Odontologo odontologo, Paciente paciente, string fecha, string hora)
        {
            Cita nuevaCita = new Cita(odontologo, paciente, fecha, hora);
            citas.Add(nuevaCita);
        }
        public void ModificarCita(int dniPac, string fecha, int estado)
        {
            Cita cita = ObtenerCita(dniPac, fecha);
            cita.Estado = estado;
        }
        public Cita ObtenerCita(int dniPac, string fecha)
        {
            for (int i = 0; i < citas.Count; i++)
            {
                Paciente pac = citas[i].Paciente;
                if (pac.Dni == dniPac && citas[i].FechaCita == fecha) return citas[i];
            }
            return null;
        }
        public List<Cita> BuscarCita(int dniOdo, string fecha)
        {
            List<Cita> listaCitasOdo = new List<Cita>();
            for (int i = 0; i < citas.Count; i++)
            {
                Odontologo odo = citas[i].Odontologo;
                if (odo.Dni == dniOdo && citas[i].FechaCita == fecha)
                {
                    listaCitasOdo.Add(citas[i]);
                    this.citas.RemoveAt(i);
                    i--;
                }
            }
            return listaCitasOdo;
        }
        public void generarArchivo()
        {//SERIALIZAR
            Stream stream = File.Open("citas.bin", FileMode.Create);
            BinaryFormatter bin = new BinaryFormatter();
            bin.Serialize(stream, this.citas);
            stream.Close();
        }
        public void cargarArchivo()
        {///DESERIALIZAR
            try
            {
                Stream stream = File.Open("citas.bin", FileMode.Open);
                BinaryFormatter bin = new BinaryFormatter();
                this.citas = (List<Cita>)bin.Deserialize(stream);
                stream.Close();
            }
            catch (Exception)
            {
                //NO HAY ARCHIVO
                //NO CARGA
            }
        }
    }
}
