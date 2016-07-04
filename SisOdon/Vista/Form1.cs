using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisOdon.Vista
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonas formPersonas = new frmPersonas();
            formPersonas.MdiParent = this;
            formPersonas.Show();
        }

        private void citasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCitas formCitas = new frmCitas();
            formCitas.MdiParent = this;
            formCitas.Show();
        }

        private void tratamientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTratamiento formTratamiento = new frmTratamiento();
            formTratamiento.MdiParent = this;
            formTratamiento.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formUsuarios formMantUsuarios = new formUsuarios();
            formMantUsuarios.MdiParent = this;
            formMantUsuarios.Show();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
