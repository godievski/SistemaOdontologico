using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SisOdon.Controlador;
using SisOdon.Modelo;

namespace SisOdon.Vista
{
    public partial class frmCitas : Form
    {
        private GestorCitas listaCitas;
        private GestorPersonas listaPersonas;
        private List<int> listaDniOdontologo;
        private List<int> listaDniPaciente;
        private List<Cita> citasTemporales;
        private int indexRowSelected;
        private bool flagCitasCambio;

        public frmCitas()
        {
            InitializeComponent();
            this.listaCitas = new GestorCitas();
            this.listaPersonas = new GestorPersonas();
            this.listaDniOdontologo = new List<int>();
            this.listaDniPaciente = new List<int>();
            this.citasTemporales = new List<Cita>();
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ReadOnly = true;
            this.flagCitasCambio = false;
        }

        private void frmCitas_Load(object sender, EventArgs e)
        {
            listaCitas.cargarArchivo();
            listaPersonas.cargarPersonasXML();
            this.cargarComboBoxDoctor();
            this.cargarComboBoxPaciente();
            this.cargarComboBoxEstadoCita();
            this.cambiarEstadoFigurasCitas(false);
            this.comboBox3.Enabled = false;
        }
        private void cambiarEstadoFigurasCitas(bool estado)
        {
            this.textBox1.Enabled = estado;
            this.textBox2.Enabled = estado;
            this.comboBox2.Enabled = estado;
            this.button2.Enabled = estado;
        }
        private void cargarComboBoxDoctor()
        {
            for (int i = 0; i < listaPersonas.Cantidad(); i++)
            {
                if (listaPersonas[i] is Odontologo)
                {
                    this.comboBox1.Items.Add(listaPersonas[i].Nombre + " " + listaPersonas[i].ApPat);
                    this.listaDniOdontologo.Add(listaPersonas[i].Dni);
                }
            }
        }
        private void cargarComboBoxPaciente()
        {
            for (int i = 0; i < listaPersonas.Cantidad(); i++)
            {
                if (listaPersonas[i] is Paciente)
                {
                    this.comboBox2.Items.Add(listaPersonas[i].Nombre + " " + listaPersonas[i].ApPat);
                    this.listaDniPaciente.Add(listaPersonas[i].Dni);
                }
            }
        }
        private void cargarComboBoxEstadoCita()
        {
            this.comboBox3.Items.Add(Cita.PAC_ASISTIO_STR);
            this.comboBox3.Items.Add(Cita.PAC_NO_ASISTIO_STR);
            this.comboBox3.Items.Add(Cita.CANCELADO_STR);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int indexDoctor = this.comboBox1.SelectedIndex;
            if (indexDoctor >= 0)
            {
                if (!flagCitasCambio && this.citasTemporales.Count > 0)
                {
                    for (int i = 0; i < this.citasTemporales.Count; i++)
                    {
                        this.listaCitas.AgregarCita(citasTemporales[i]);
                    }
                }
                this.flagCitasCambio = false;
                int dniOdo = this.listaDniOdontologo[indexDoctor];
                string fecha = this.dateTimePicker1.Text;
                this.citasTemporales = this.listaCitas.BuscarCita(dniOdo, fecha);
                this.cambiarEstadoFigurasCitas(true);
                this.borrarDatosCitas();
                this.comboBox3.Enabled = false;
                cargarAgendaCitas(citasTemporales);
            }
        }
        private void borrarDatosCitas()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.comboBox2.SelectedIndex = -1;
        }
        private void cargarAgendaCitas(List<Cita> citasOdo)
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < citasOdo.Count; i++)
            {
                /*HORA-INICIO | HORA-FIN | PACIENTE | ODONTOLOGO | ESTADO */
                string[] rows = new string[5];
                rows[0] = citasOdo[i].HoraCita;
                rows[1] = citasOdo[i].horaFin();
                rows[2] = citasOdo[i].Paciente.Nombre + " " + citasOdo[i].Paciente.ApPat;
                rows[3] = citasOdo[i].Odontologo.Nombre + " " + citasOdo[i].Odontologo.ApPat;
                if (citasOdo[i].Estado == Cita.CANCELADO)
                    rows[4] = Cita.CANCELADO_STR;
                else if (citasOdo[i].Estado == Cita.PAC_ASISTIO)
                    rows[4] = Cita.PAC_ASISTIO_STR;
                else if (citasOdo[i].Estado == Cita.PAC_NO_ASISTIO)
                    rows[4] = Cita.PAC_NO_ASISTIO_STR;
                this.dataGridView1.Rows.Add(rows);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string horaIni = this.textBox1.Text;
            string horaFin = this.textBox2.Text;
            int duracion = Cita.calcularDuracion(horaIni, horaFin);
            int indexOdontologo = this.comboBox1.SelectedIndex;
            int indexPaciente = this.comboBox2.SelectedIndex;
            Paciente paciente = (Paciente)listaPersonas.BuscarPersona(this.listaDniPaciente[indexPaciente]);
            Odontologo odo = (Odontologo)listaPersonas.BuscarPersona(this.listaDniOdontologo[indexOdontologo]);
            Cita cita = new Cita(odo, paciente, this.dateTimePicker1.Text, horaIni);
            cita.DuracionCita = duracion;
            this.agregarCitaGrilla(cita);
            this.citasTemporales.Add(cita);
            this.flagCitasCambio = true;
        }
        private void agregarCitaGrilla(Cita cita){
             /*HORA-INICIO | HORA-FIN | PACIENTE | ODONTOLOGO | ESTADO */
                string[] rows = new string[5];
                rows[0] = cita.HoraCita;
                rows[1] = cita.horaFin();
                rows[2] = cita.Paciente.Nombre + " " + cita.Paciente.ApPat;
                rows[3] = cita.Odontologo.Nombre + " " + cita.Odontologo.ApPat;
                if (cita.Estado == Cita.CANCELADO)
                    rows[4] = Cita.CANCELADO_STR;
                else if (cita.Estado == Cita.PAC_ASISTIO)
                    rows[4] = Cita.PAC_ASISTIO_STR;
                else if (cita.Estado == Cita.PAC_NO_ASISTIO)
                    rows[4] = Cita.PAC_NO_ASISTIO_STR;
                this.dataGridView1.Rows.Add(rows);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRowSelected = this.dataGridView1.SelectedCells[0].RowIndex;
            int cantidadColumnas = this.dataGridView1.SelectedRows.Count;
            //CANTIDAD COLUMNAS = 0 -> SE SELECCIONO UNA SOLA CELDA
            //CANTIDAD COLUMNAS = 1 -> SE SELECCIONO TODA UNA FILA
            if (cantidadColumnas == 0 || cantidadColumnas == 1)
            {
                this.cambiarEstadoFigurasCitas(false);
                this.comboBox3.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cita cita = this.citasTemporales[indexRowSelected];
            string estadoStr = "";
            cita.Estado = this.comboBox3.SelectedIndex;
            if (cita.Estado == Cita.CANCELADO)
                estadoStr = Cita.CANCELADO_STR;
            else if (cita.Estado == Cita.PAC_ASISTIO)
                estadoStr = Cita.PAC_ASISTIO_STR;
            else if (cita.Estado == Cita.PAC_NO_ASISTIO)
                estadoStr = Cita.PAC_NO_ASISTIO_STR;
            dataGridView1.Rows[indexRowSelected].Cells[4].Value = estadoStr;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.flagCitasCambio = true;
            this.citasTemporales.RemoveAt(this.indexRowSelected);
            this.cargarAgendaCitas(this.citasTemporales);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.citasTemporales.Count; i++)
            {
                this.listaCitas.AgregarCita(citasTemporales[i]);
            }
            this.citasTemporales.Clear();
            this.cambiarEstadoFigurasCitas(false);
            this.dataGridView1.Rows.Clear();
            this.borrarDatosCitas();
            this.comboBox1.SelectedIndex = -1;
            this.comboBox3.SelectedIndex = -1;
            this.comboBox3.Enabled = true;
            this.flagCitasCambio = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.listaCitas.generarArchivo();
        }
        
    }
}
