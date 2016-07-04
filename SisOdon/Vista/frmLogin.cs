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

namespace SisOdon.Vista
{
    public partial class frmLogin : Form
    {
        private GestorUsuario usuarios;
        private int respuesta;

        public frmLogin()
        {
            InitializeComponent();
            
            this.usuarios = new GestorUsuario();
            this.respuesta = GestorUsuario.PASSWORD_INCORRECTO;
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            this.usuarios.cargarUsuariosTxt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nickname = this.textBoxUsuario.Text;
            string password = this.textBoxPassword.Text;
            respuesta = this.usuarios.verificarUsuario(nickname, password);
            if (respuesta == GestorUsuario.CORRECTO)
            {
                MessageBox.Show("Bienvenido!");
                this.Visible = false;
                Form1 sistema = new Form1();
                sistema.Show();
            }
            else if (respuesta == GestorUsuario.NICKNAME_INCORRECTO)
            {
                MessageBox.Show("Usuario no registrado.");
                this.textBoxPassword.Text = "";
            }
            else if (respuesta == GestorUsuario.PASSWORD_INCORRECTO)
            {
                MessageBox.Show("Password incorrecto.");
                this.textBoxPassword.Text = "";
            }
        }


    }
}
