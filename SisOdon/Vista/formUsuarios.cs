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
    public partial class formUsuarios : Form
    {
        private static int INTERVALO = 10;
        private GestorUsuario usuarios;
        private Usuario usuario;
        private int tiempo;
        private int index;

        public formUsuarios()
        {
            InitializeComponent();
            this.usuarios = new GestorUsuario();
            this.tiempo = 0;
            this.index = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nickname = this.textBoxNicknameBusqueda.Text;
            Usuario usuario = this.usuarios.buscarPorNickname(nickname);
            if (usuario != null)
                this.cargarGrilla(usuario);
            else
            {
                MessageBox.Show("No se encontró al usuario con el nickname " + nickname);
            }
        }

        private void formUsuarios_Load(object sender, EventArgs e)
        {
            this.usuarios.cargarUsuariosTxt();
            this.cargarGrilla();
            this.timer1.Start();
        }

        private void cargarGrilla(Usuario usuario)
        {
            this.dataGridView1.Rows.Clear();
            string[] rows = new string[4];
            rows[0] = usuario.Nombre;
            rows[1] = usuario.ApPat;
            rows[2] = usuario.ApMat;
            rows[3] = usuario.Nickname;
            this.dataGridView1.Rows.Add(rows);
        }

        private void cargarGrilla()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < usuarios.Count; i++)
            {
                string[] rows = new string[4];
                rows[0] = (this.usuarios[i]).Nombre;
                rows[1] = (this.usuarios[i]).ApPat;
                rows[2] = (this.usuarios[i]).ApMat;
                rows[3] = (this.usuarios[i]).Nickname;
                this.dataGridView1.Rows.Add(rows);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*PARA MODIFICAR DATOS - NO PIDEN*/
        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = this.textBoxNombre.Text;
            string apPat = this.textBoxApPat.Text;
            string apMat = this.textBoxApMat.Text;
            string nickname = this.textBox1.Text;
            string password = this.textBox2.Text;
            string password_confirmado = this.textBox3.Text;
            if (password != password_confirmado)
            {
                MessageBox.Show("Las contraseñas no coinciden");
            }
            else
            {
                this.usuarios.Add(nombre, apPat, apMat, nickname, password);
            }
            string[] rows = new string[4];
            rows[0] = nombre;
            rows[1] = apPat;
            rows[2] = apMat;
            rows[3] = nickname;
            this.dataGridView1.Rows.Add(rows);
            this.tabControl1.SelectedIndex = 0;
            this.limpiarRegistroUsuario();
        }

        private void limpiarRegistroUsuario()
        {
            this.textBoxNombre.Text = "";
            this.textBoxApPat.Text = "";
            this.textBoxApMat.Text = "";
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tiempo += 1;
            Console.WriteLine("Tiempo "+tiempo);
            if (tiempo % INTERVALO == 0)
            {
                Console.WriteLine("Hola");
                this.usuarios.cargarUsuariosTxt();
                this.cargarGrilla();
            }
        }
    }
}
