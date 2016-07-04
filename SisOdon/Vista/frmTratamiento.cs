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
using System.Runtime.Serialization.Formatters.Binary;

using SisOdon.Controlador;
using SisOdon.Modelo;

namespace SisOdon.Vista
{
    public partial class frmTratamiento : Form
    {
        private GestorPersonas listaPaciente;
        private GestorConcepto listaConcepto;
        private Paciente pacienteSeleccionada;
        private int tiempo;

        public frmTratamiento()
        {
            InitializeComponent();
            listaPaciente = new GestorPersonas();
            listaConcepto = new GestorConcepto();
            pacienteSeleccionada = null;

            this.textBoxNombreTrat.Enabled = false;
            this.textBoxApPat.Enabled = false;
            this.textBoxApMat.Enabled = false;
            this.textBoxDNITrat.Enabled = false;

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ReadOnly = true;

            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ReadOnly = true;

            this.tiempo = 0;
            this.textBoxInactividad.Text = "0";
        }

        private void frmTratamiento_Load(object sender, EventArgs e)
        {
            this.listaPaciente.cargarPacientesXML();
            this.listaConcepto.CargarConceptosArchivo();

            this.cargarGrilla();
            this.cargarConceptos();
            this.timer1.Start();
        }
        private void cargarGrilla()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < listaPaciente.cantidadPersonas(); i++)
            {
                string[] rows = new string[4];
                rows[0] = (this.listaPaciente[i]).Nombre;
                rows[1] = (this.listaPaciente[i]).ApPat;
                rows[2] = (this.listaPaciente[i]).ApMat;
                rows[3] = ((this.listaPaciente[i]).Dni).ToString();
                this.dataGridView1.Rows.Add(rows);
            }
        }
        private void cargarConceptos()
        {
            for (int i = 0; i < this.listaConcepto.Count; i++)
            {
                this.comboBoxConcepto.Items.Add(listaConcepto[i].Nombre);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int dni = int.Parse(this.textBoxDNIBusqueda.Text);
                Persona persona = this.listaPaciente.BuscarPersona(dni);
                this.dataGridView1.Rows.Clear();
                if (persona != null)
                {
                    string[] rows = new string[4];
                    rows[0] = persona.Nombre;
                    rows[1] = persona.ApPat;
                    rows[2] = persona.ApMat;
                    rows[3] = persona.Dni.ToString();
                    this.dataGridView1.Rows.Add(rows);
                }
                else
                {
                    MessageBox.Show("Persona no encontrada");
                }
            }
            catch (Exception)
            {
                this.cargarGrilla();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRowSelected = this.dataGridView1.SelectedCells[0].RowIndex;
            int cantidadColumnas = this.dataGridView1.SelectedRows.Count;
            //CANTIDAD COLUMNAS = 0 -> SE SELECCIONO UNA SOLA CELDA
            //CANTIDAD COLUMNAS = 1 -> SE SELECCIONO TODA UNA FILA
            if (cantidadColumnas == 0 || cantidadColumnas == 1)
            {
                int dni = int.Parse(dataGridView1.Rows[indexRowSelected].Cells[3].Value.ToString());
                pacienteSeleccionada = (Paciente) this.listaPaciente.BuscarPersona(dni);
                if (pacienteSeleccionada != null)
                {
                    this.cargarPacienteTratamientos(pacienteSeleccionada);
                    this.tabControl1.SelectedIndex = 1;
                }
            }
        }

        private void cargarPacienteTratamientos(Paciente paciente)
        {
            this.textBoxNombreTrat.Text = pacienteSeleccionada.Nombre;
            this.textBoxApPat.Text = pacienteSeleccionada.ApPat;
            this.textBoxApMat.Text = pacienteSeleccionada.ApMat;
            this.textBoxDNITrat.Text = pacienteSeleccionada.Dni.ToString();

            double total = 0;
            
        
            this.dataGridView2.Rows.Clear();
            for (int i = 0; i < paciente.CountTratamiento(); i++)
            {
                string[] rows = new string[3];
                rows[0] = paciente[i].Concepto.Nombre;
                rows[1] = paciente[i].FechaExamen;
                rows[2] = paciente[i].Concepto.Costo.ToString();
                total += paciente[i].Concepto.Costo;
                this.dataGridView2.Rows.Add(rows);
            }

            this.textBoxTotalAPagar.Text = total.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pacienteSeleccionada != null)
            {
                int indexConcepto = comboBoxConcepto.SelectedIndex;
                Concepto concepto = this.listaConcepto[indexConcepto];
                string fechaExamen = dateTimePicker1.Text;
                this.pacienteSeleccionada.AddTratamiento(new Tratamiento(concepto, fechaExamen));
                string[] rows = new string[3];
                rows[0] = concepto.Nombre;
                rows[1] = fechaExamen;
                rows[2] = concepto.Costo.ToString();
                this.dataGridView2.Rows.Add(rows);

                double total = double.Parse(this.textBoxTotalAPagar.Text);
                total += concepto.Costo;
                this.textBoxTotalAPagar.Text = total.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.pacienteSeleccionada != null)
            {
                this.listaPaciente.CrearArchivoTratamientoPaciente(pacienteSeleccionada);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tiempo = int.Parse(this.textBoxInactividad.Text);
            tiempo += 1;
            this.textBoxInactividad.Text = tiempo.ToString();

            //TIEMPO 60 SEG
            if (tiempo == 60)
            {
                MessageBox.Show("Se cerrará la aplicación");
                //FORZAR CIERRE
                Form padre = this.MdiParent;
                padre.Close();
            }
            
        }


        private void frmTratamiento_MouseHover(object sender, EventArgs e)
        {
            this.tiempo = 0;
        }
    }
}
