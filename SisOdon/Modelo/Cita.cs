using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    [Serializable]
    public class Cita
    {
        private Odontologo odontologo;
        private Paciente paciente; 
        private string fecha; /*fecha de la cita, formato: DD/MM/AAAA*/
        private string hora; /*hora de la cita, formato: HH:MM*/
        private int duracion; /*expresado en minutos*/
        private int estado; /*1-> paciente asistió; 2->paciente no asistió; 3->odontólogo canceló*/
        
        //CONSTANTES
        private static int DEFDUR = 30; /*duración por defecto 30 minutos*/
        public static int PAC_ASISTIO = 0;
        public static int PAC_NO_ASISTIO = 1;
        public static int CANCELADO = 2;
        public static string PAC_ASISTIO_STR = "Asistió";
        public static string PAC_NO_ASISTIO_STR = "No Asistió";
        public static string CANCELADO_STR = "Cancelado";


        public Cita(Odontologo odontologo, Paciente paciente, string fecha, string hora)
        {
            this.odontologo = odontologo;
            this.paciente = paciente;
            this.fecha = fecha;
            this.hora = hora;
            this.duracion = DEFDUR;
            this.estado = PAC_ASISTIO;
        }
        public Cita(Odontologo odontologo, Paciente paciente, string fecha, string hora, int duracion)
        {
            this.odontologo = odontologo;
            this.paciente = paciente;
            this.fecha = fecha;
            this.hora = hora;
            this.duracion = duracion;
            this.estado = PAC_ASISTIO;
        }

        public string horaFin()
        {
            string[] horas = new string[10];
            char[] separador = new char[1];
            separador[0] = ':';
            horas = this.hora.Split(separador);
            int hh = int.Parse(horas[0]);
            int mm = int.Parse(horas[1]);
            int fin = hh * 60 + mm + this.duracion;
            int hhFin = fin / 60;
            int mmFin = fin % 60;
            return (hhFin.ToString() + ":" + mmFin.ToString());
        }
        public static int calcularDuracion(string horaIni, string horaFin)
        {
            string[] horasIni = new string[10];
            string[] horasFin = new string[10];
            char[] separador = new char[1];
            separador[0] = ':';
            horasIni = horaIni.Split(separador);
            horasFin = horaFin.Split(separador);
            int hhIni, mmIni, hhFin, mmFin;
            int duracion;
            try
            {
                hhIni = int.Parse(horasIni[0]);
                mmIni = int.Parse(horasIni[1]);
                hhFin = int.Parse(horasFin[0]);
                mmFin = int.Parse(horasFin[1]);
                duracion = (hhFin - hhIni) * 60 + (mmFin - mmIni);
            }
            catch {
                hhIni = mmIni = hhFin = mmFin = 0;
                duracion = -1;
            }
            return duracion;
        }

        public Odontologo Odontologo
        {
            set
            {
                odontologo = value;
            }
            get
            {
                return odontologo;
            }
        }
        public Paciente Paciente
        {
            set
            {
                paciente = value;
            }
            get
            {
                return paciente;
            }
        }
        public string FechaCita
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
        public string HoraCita
        {
            set
            {
                hora = value;
            }
            get
            {
                return hora;
            }
        }
        public int DuracionCita
        {
            set
            {
                duracion = value;
            }
            get
            {
                return duracion;
            }
        }
        public int Estado
        {
            set
            {
                estado = value;
            }
            get
            {
                return estado;
            }
        }
    }
}
