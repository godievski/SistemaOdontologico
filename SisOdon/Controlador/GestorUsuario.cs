using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SisOdon.Modelo;

namespace SisOdon.Controlador
{
    class GestorUsuario
    {
        List<Usuario> usuarios;
        public static int NICKNAME_INCORRECTO = 0;
        public static int PASSWORD_INCORRECTO = 1;
        public static int CORRECTO = 2;

        public GestorUsuario()
        {
            this.usuarios = new List<Usuario>();
        }
        public Usuario this[int index]
        {
            get
            {
                return this.usuarios[index];
            }
        }

        public void Add(string nombre, string apPat, string apMat, string nickname, string password)
        {
            Usuario usuario = new Usuario(nombre, apPat, apMat, nickname, password);
            this.usuarios.Add(usuario);
            this.agregarUsuarioTxt(usuario);
        }

        public void Add(Usuario usuario)
        {
            this.usuarios.Add(usuario);
            this.agregarUsuarioTxt(usuario);
        }

        private void agregarUsuarioTxt(Usuario usuario)
        {
            FileStream fileUsuario = new FileStream("usuarios.txt", FileMode.Append, FileAccess.Write);
            StreamWriter escritor = new StreamWriter(fileUsuario);
            string linea = usuario.Nombre;
            linea += "," + usuario.ApPat;
            linea += "," + usuario.ApMat;
            linea += "," + usuario.Nickname;
            linea += "," + usuario.Password;
            escritor.WriteLine(linea);
            escritor.Close();
            fileUsuario.Close();
        }

        public void cargarUsuariosTxt()
        {
            //CARGAR ARCHIVO UNIVERSIDADES
            FileStream fileUsuario = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read);
            StreamReader lector = new StreamReader(fileUsuario);
            this.usuarios.Clear();
            while (true)
            {
                string linea = lector.ReadLine();
                if (linea == null) break;
                string[] palabras = new string[5];
                char[] separador = { ',' };
                palabras = linea.Split(separador);
                Usuario usuario = new Usuario(palabras[0], palabras[1], palabras[2], palabras[3], palabras[4]);
                this.usuarios.Add(usuario);
            }
            lector.Close();
            fileUsuario.Close();
        }


        public int verificarUsuario(string nickname, string password)
        {
            Usuario usuario;
            usuario = buscarPorNickname(nickname);
            if (usuario == null)
                return NICKNAME_INCORRECTO;
            if (usuario.Password != password)
                return PASSWORD_INCORRECTO;
            return CORRECTO;
        }

        public int Count
        {
            get
            {
                return this.usuarios.Count;
            }
        }

        public Usuario buscarPorNickname(string nickname)
        {
            for (int i = 0; i < this.usuarios.Count; i++)
            {
                Usuario user = this.usuarios[i];
                if (user.Nickname == nickname)
                    return user;
            }
            return null;
        }
    }
}
