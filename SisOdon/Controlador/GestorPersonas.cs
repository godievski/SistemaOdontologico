using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SisOdon.Modelo;
using System.Xml;

namespace SisOdon.Controlador
{
    public class GestorPersonas
    {
        private List<Persona> personas;

        public GestorPersonas()
        {
            this.personas = new List<Persona>();
        }

        public int Cantidad()
        {
            return this.personas.Count;
        }

        public Persona this[int index]
        {
            get
            {
                return this.personas[index];
            }
            set
            {
                this.personas[index] = value;
            }
        }
        public int cantidadPersonas()
        {
            return personas.Count;
        }
        public void Serializar()
        {
            Stream stream = File.Open("personas.bin", FileMode.Create);
            BinaryFormatter bin = new BinaryFormatter();
            bin.Serialize(stream, this.personas);
            stream.Close();
        }
        public void Deserializar()
        {
            try
            {
                Stream stream = File.Open("personas.bin", FileMode.Open);
                BinaryFormatter bin = new BinaryFormatter();
                this.personas = (List<Persona>)bin.Deserialize(stream);
                stream.Close();
            } catch (Exception)
            {
                //NO HAY ARCHIVO
                //NO CARGA
            }
            
        }

        public void cargarPersonasXML()
        {
            this.cargarOdontologosXML();
            this.cargarPacientesXML();
        }

        public void cargarOdontologosXML()
        {
            //ODONTOLOGOS
            XmlDocument xDocOdo = new XmlDocument();
            xDocOdo.Load("Odontologos.xml");
            XmlNodeList objOdontologos = xDocOdo.GetElementsByTagName("Odontologos");
            XmlNodeList listaOdo = ((XmlElement)objOdontologos[0]).GetElementsByTagName("Odontologo");
            foreach (XmlElement nodo in listaOdo)
            {
                int DNI = int.Parse(nodo.GetElementsByTagName("DNI")[0].InnerText);
                string Nombre = nodo.GetElementsByTagName("Nombre")[0].InnerText;
                string ApellidoPat = nodo.GetElementsByTagName("ApellidoPat")[0].InnerText;
                string ApellidoMat = nodo.GetElementsByTagName("ApellidoMat")[0].InnerText;
                string FechaNac = nodo.GetElementsByTagName("FechaNac")[0].InnerText;
                string sexo = nodo.GetElementsByTagName("sexo")[0].InnerText;
                string Direccion = nodo.GetElementsByTagName("Direccion")[0].InnerText;
                string Especialidad = nodo.GetElementsByTagName("Especialidad")[0].InnerText;
                string Universidad = nodo.GetElementsByTagName("Universidad")[0].InnerText;
                Odontologo odontologo = new Odontologo(Especialidad, Universidad, DNI, Nombre, ApellidoPat, ApellidoMat, FechaNac, sexo, Direccion, null, "09/05/2016");
                this.personas.Add(odontologo);
            }
        }
        public void cargarPacientesXML()
        {
            //PACIENTES
            XmlDocument xDocPac = new XmlDocument();
            xDocPac.Load("Pacientes.xml");
            XmlNodeList objPacientes = xDocPac.GetElementsByTagName("Pacientes");
            XmlNodeList listaPac = ((XmlElement)objPacientes[0]).GetElementsByTagName("Paciente");
            foreach (XmlElement nodo in listaPac)
            {
                int DNI = int.Parse(nodo.GetElementsByTagName("DNI")[0].InnerText);
                string Nombre = nodo.GetElementsByTagName("Nombre")[0].InnerText;
                string ApellidoPat = nodo.GetElementsByTagName("ApellidoPat")[0].InnerText;
                string ApellidoMat = nodo.GetElementsByTagName("ApellidoMat")[0].InnerText;
                string FechaNac = nodo.GetElementsByTagName("FechaNac")[0].InnerText;
                string sexo = nodo.GetElementsByTagName("sexo")[0].InnerText;
                string Direccion = nodo.GetElementsByTagName("Direccion")[0].InnerText;
                Paciente paciente = new Paciente(DNI, Nombre, ApellidoPat, ApellidoMat, FechaNac, sexo, Direccion);
                this.personas.Add(paciente);
            }
        }

        public List<Paciente> obtenerPacientes()
        {
            List<Paciente> pacientes = new List<Paciente>();
            for (int i = 0; i < this.personas.Count; i++)
            {
                if (personas[i] is Paciente)
                    pacientes.Add((Paciente)personas[i]);
            }
            return pacientes;
        }
        public List<Odontologo> obtenerOdontologos()
        {
            List<Odontologo> odontologos = new List<Odontologo>();
            for (int i = 0; i < this.personas.Count; i++)
            {
                if (personas[i] is Odontologo)
                    odontologos.Add((Odontologo)personas[i]);
            }
            return odontologos;
        }

        public void AgregarPersona(Persona persona)
        {
            this.personas.Add(persona);
        }

        public void AgregarPersona(int dni, string nombre, string apPat, string apMat, string fecha,
                                   string sexo, string dir, int tipo, string esp, string univ, string fechaInicio, Sede sede)
        {
            //0-> PACIENTE, 1->ODONTOLOGO
            Persona persona = null;
            if (tipo == 1)
                persona = new Odontologo(esp, univ, dni, nombre, apPat, apMat, fecha, sexo, dir, sede, fechaInicio);
            else if (tipo == 0)
                persona = new Paciente(dni, nombre, apPat, apMat, fecha, sexo, dir);
            else return;
            personas.Add(persona);
        }
        public void AgregarPersona(int dni, string nombre, string apPat, string apMat, string fecha,
                                   string sexo, string dir, int tipo, string esp, string univ, string fechaInicio)
        {
            //0-> PACIENTE, 1->ODONTOLOGO
            Persona persona = null;
            if (tipo == 1)
                persona = new Odontologo(esp, univ, dni, nombre, apPat, apMat, fecha, sexo, dir, null, fechaInicio);
            else if (tipo == 0)
                persona = new Paciente(dni, nombre, apPat, apMat, fecha, sexo, dir);
            else return;
            personas.Add(persona);
        }

        public Persona BuscarPersona(int dni)
        {
            for (int i = 0; i < personas.Count; i++)
            {
                if (personas[i].Dni == dni) return personas[i];
            }
            return null;
        }
        public List<Persona> BuscarPersonas(int dni, int tipo)
        {
            //0 -> NINGUN TIPO, 1-> Paciente , 2-> Odontologo
            List<Persona> listaPersonas = new List<Persona>();
            for (int i = 0; i < personas.Count; i++)
            {
                if (personas[i].Dni == dni)
                {
                    if (tipo == 1 && personas[i] is Paciente)
                        listaPersonas.Add(personas[i]);
                    else if (tipo == 2 && personas[i] is Odontologo)
                        listaPersonas.Add(personas[i]);
                    else if (tipo != 1 && tipo != 2)
                    {
                        listaPersonas.Add(personas[i]);
                    }
                } else if (dni == -1){
                    if (tipo == 1 && personas[i] is Paciente)
                        listaPersonas.Add(personas[i]);
                    else if (tipo == 2 && personas[i] is Odontologo)
                        listaPersonas.Add(personas[i]);
                    else if (tipo != 1 && tipo != 2)
                    {
                        listaPersonas.Add(personas[i]);
                    }
                }
            }
            return listaPersonas;
        }

        //NUEVO
        public void ModificarPersona(Persona persona, string nombre, string apPat, string apMat,
            string dir, string sexo, string fecha,
            string especialidad, string univ)
        {
            persona.Nombre = nombre;
            persona.ApPat = apPat;
            persona.ApMat = apMat;
            persona.Direccion = dir;
            persona.Sexo = sexo;
            persona.Fecha = fecha;
            if (persona is Odontologo)
            {
                Odontologo odo = (Odontologo)persona;
                odo.Especialidad = especialidad;
                odo.Universidad = univ;
            }
        }
        public void CrearArchivoTratamientoPaciente(Paciente paciente)
        {
            Stream stream = File.Open(paciente.Dni.ToString() + ".bin", FileMode.Create);
            BinaryFormatter bin = new BinaryFormatter();
            bin.Serialize(stream,paciente.ObtenerListaTratamiento());
            stream.Close();
        }
        public void CargarArchivoTratamientoPaciento(Paciente paciente){
            try
            {

                Stream stream = File.Open(paciente.Dni.ToString() + ".bin", FileMode.Open);
                BinaryFormatter bin = new BinaryFormatter();
                paciente.CargarListaTratamiento( (List<Tratamiento>)bin.Deserialize(stream) );
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
