using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SisOdon.Modelo;
using SisOdon.Controlador;
namespace SisOdon.Vista
{
    public partial class frmPersonas : Form
    {
        private GestorPersonas personas;
        //ATRIBUTO NUEVO
        private Persona personaSeleccionada;

        public frmPersonas()
        {
            InitializeComponent();
            this.personas = new GestorPersonas();
            //AGREGADO
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ReadOnly = true;
        }

        private void frmPersonas_Load(object sender, EventArgs e)
        {
            //CARGAR ARCHIVO ESPECIALIDAD
            this.cargarEspecialidades();
            //CARGAR ARCHIVO UNIVERSIDADES
            this.cargarUniversidades();
            //CARGAR PERSONAS
            this.personas.Deserializar();
            //LLENAR GRILLAR
            this.cargarGrilla();
            //DEFAULT CMBBOX
            this.comboBoxTipoBusq.SelectedIndex = 0;
            this.comboBoxTipoReg.SelectedIndex = 0;
            this.comboBoxSexo.SelectedIndex = 0;
        }
        private void cargarEspecialidades()
        {
            //CARGAR ARCHIVO ESPECIALIDAD
            FileStream fileEsp = new FileStream("especialidades.txt", FileMode.Open, FileAccess.Read);
            StreamReader lector = new StreamReader(fileEsp);
            while (true)
            {
                string esp = lector.ReadLine();
                if (esp == null) break;
                //AGREGO ESPECIALIDADES
                this.comboBoxEspecialidad.Items.Add(esp);
            }
            lector.Close();
            fileEsp.Close();
        }
        private void cargarUniversidades()
        {
            //CARGAR ARCHIVO UNIVERSIDADES
            FileStream fileUniv = new FileStream("universidades.txt", FileMode.Open, FileAccess.Read);
            StreamReader lector2 = new StreamReader(fileUniv);
            while (true)
            {
                string univ = lector2.ReadLine();
                if (univ == null) break;
                //AGREGO UNIVERSIDADES
                this.comboBoxUniv.Items.Add(univ);
            }
        }
        private void cargarGrilla()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < personas.cantidadPersonas(); i++)
            {
                string[] rows = new string[5];
                rows[0] = (this.personas[i]).Nombre;
                rows[1] = (this.personas[i]).ApPat;
                rows[2] = (this.personas[i]).ApMat;
                rows[3] = ((this.personas[i]).Dni).ToString();
                if (this.personas[i] is Paciente)
                    rows[4] = "Paciente";
                else if (this.personas[i] is Odontologo)
                    rows[4] = "Odontologo";
                else
                    rows[4] = "Ninguno";
                this.dataGridView1.Rows.Add(rows);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dniStr = this.textBoxDNIBusq.Text;
            int dni = -1;
            
            try
            {
                dni = int.Parse(dniStr);
            }
            catch (FormatException)
            {
                if (dniStr != "")
                {
                    MessageBox.Show("Ingrese un valor de dni válido.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.dataGridView1.Rows.Clear();
                    return;
                }
            }

            int tipo = comboBoxTipoBusq.SelectedIndex; //0 -> NINGUN TIPO, 1-> Paciente , 2-> Odontologo
            List<Persona> listaPersonas = personas.BuscarPersonas(dni, tipo);
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < listaPersonas.Count; i++)
            {
                string[] rows = new string[5];
                rows[0] = (listaPersonas[i]).Nombre;
                rows[1] = (listaPersonas[i]).ApPat;
                rows[2] = (listaPersonas[i]).ApMat;
                rows[3] = ((listaPersonas[i]).Dni).ToString();
                if (listaPersonas[i] is Paciente)
                    rows[4] = "Paciente";
                else if (listaPersonas[i] is Odontologo)
                    rows[4] = "Odontologo";
                else
                    rows[4] = "Ninguno";
                this.dataGridView1.Rows.Add(rows);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tipoPersona = this.comboBoxTipoReg.SelectedIndex;
            
            if (tipoPersona == 0)
            {
                //PACIENTE
                this.comboBoxEspecialidad.Enabled = false;
                this.comboBoxUniv.Enabled = false;
                this.comboBoxEspecialidad.SelectedIndex = -1;
                this.comboBoxUniv.SelectedIndex = -1;
            }
            else if (tipoPersona == 1 || tipoPersona == -1)
            {
                //PACIENTE
                this.comboBoxEspecialidad.Enabled = true;
                this.comboBoxUniv.Enabled = true;
                this.comboBoxEspecialidad.SelectedIndex = 0;
                this.comboBoxUniv.SelectedIndex = 0;
            }
        }
        private void limpiarRegistro(){
            this.comboBoxTipoReg.SelectedIndex = 0;
            this.textBoxNombre.Text = "";
            this.textBoxDNIReg.Text = "";
            this.textBoxApPat.Text = "";
            this.textBoxApMat.Text = "";
            this.comboBoxSexo.SelectedIndex = 0;
            this.textBoxDireccion.Text = "";
            this.comboBoxEspecialidad.SelectedIndex = -1;
            this.comboBoxUniv.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tipoPersona = this.comboBoxTipoReg.SelectedIndex;
            //0-> PACIENTE, 1->ODONTOLOGO
            string nombre = this.textBoxNombre.Text;
            int dni = -1;
            try
            {
                dni = int.Parse(this.textBoxDNIReg.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Ingrese un valor de dni válido.", "ERROR",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.dataGridView1.Rows.Clear();
                return;
            }
            string apPat = this.textBoxApPat.Text;
            string apMat = this.textBoxApMat.Text;
            string fechaNac = this.dateTimePicker1.Text;
            string sexo = this.comboBoxSexo.Text;
            string direccion = this.textBoxDireccion.Text;
            string esp = this.comboBoxEspecialidad.Text;
            string univ = this.comboBoxUniv.Text;
            //AGREGO PERSONA
            personas.AgregarPersona(dni, nombre, apPat, apMat, fechaNac,
                                   sexo, direccion, tipoPersona, esp, univ, "27/04/2016");
            this.limpiarRegistro();
            this.cargarGrilla();
            this.tabControl1.SelectedIndex = 0;
        }

        private void frmPersonas_FormClosing(object sender, FormClosingEventArgs e)
        {
            personas.Serializar();
            MessageBox.Show("Se guardó exitosamente");
            //PARCHE: ELIMINARSE DE SU PADRE
        }


        //ADICIONADO 

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            int indexTab = this.tabControl1.SelectedIndex;
            if (indexTab == 1)
            {
                this.buttonRegistrar.Enabled = true;
                this.textBoxDNIReg.Enabled = true;
                this.comboBoxTipoReg.Enabled = true;
                this.buttonModificar.Enabled = false;
                this.limpiarRegistro();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //AGREGAR DATOS A TAB REGISTRO
            int indexRow = this.dataGridView1.SelectedCells[0].RowIndex;
            int cantidadColumnas = this.dataGridView1.SelectedRows.Count;
            //CANTIDAD COLUMNAS = 0 -> SE SELECCIONO UNA SOLA CELDA
            //CANTIDAD COLUMNAS = 1 -> SE SELECCIONO TODA UNA FILA
            if (cantidadColumnas == 0 || cantidadColumnas == 1)
            {
                this.tabControl1.SelectedIndex = 1;
                this.buttonRegistrar.Enabled = false;
                this.textBoxDNIReg.Enabled = false;
                this.comboBoxTipoReg.Enabled = false;
                this.buttonModificar.Enabled = true;
                int dni = int.Parse(dataGridView1.Rows[indexRow].Cells[3].Value.ToString());
                this.personaSeleccionada = personas.BuscarPersona(dni);
                this.llenarRegistrosConPersona(this.personaSeleccionada);
            }
        }
        private void llenarRegistrosConPersona(Persona persona)
        {
            if (persona != null)
            {
                if (persona is Paciente)
                {
                    this.comboBoxTipoReg.SelectedIndex = 0;
                }
                else if (persona is Odontologo)
                {
                    this.comboBoxTipoReg.SelectedIndex = 1;
                    Odontologo odo = (Odontologo)persona;
                    seleccionarEspecialidad(odo.Especialidad);
                    seleccionarUniversidad(odo.Universidad);
                }
                this.textBoxNombre.Text = persona.Nombre;
                this.textBoxDNIReg.Text = persona.Dni.ToString();
                this.textBoxApPat.Text = persona.ApPat;
                this.textBoxApMat.Text = persona.ApMat;
                this.dateTimePicker1.Text = persona.Fecha;
                if (persona.Sexo == "Femenino")
                    this.comboBoxSexo.SelectedIndex = 0;
                else if (persona.Sexo == "Masculino")
                    this.comboBoxSexo.SelectedIndex = 1;
                this.textBoxDireccion.Text = persona.Direccion;
            }
        }
        private void seleccionarEspecialidad(string especialidad)
        {
            for (int i = 0; i < this.comboBoxEspecialidad.Items.Count; i++)
            {
                this.comboBoxEspecialidad.SelectedIndex = i;
                string content = this.comboBoxEspecialidad.Text;
                if (content == especialidad) break;
            }
        }
        private void seleccionarUniversidad(string universidad)
        {
            for (int i = 0; i < this.comboBoxUniv.Items.Count; i++)
            {
                this.comboBoxUniv.SelectedIndex = i;
                string content = this.comboBoxUniv.Text;
                if (content == universidad) break;
            }
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            this.personas.ModificarPersona(this.personaSeleccionada, this.textBoxNombre.Text, this.textBoxApPat.Text,
                this.textBoxApMat.Text, this.textBoxDireccion.Text,
                this.comboBoxSexo.Text, this.dateTimePicker1.Text,
                this.comboBoxEspecialidad.Text, this.comboBoxUniv.Text);
            this.limpiarRegistro();
            this.cargarGrilla();
            this.tabControl1.SelectedIndex = 0;
        }
    }
}
