using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdon.Modelo
{
    class Usuario
    {
        private string nombre;
        private string apPat;
        private string apMat;
        private string nickname;
        private string password;

        public Usuario( string nombre, string apPat, string apMat, string nickname, string password)
        {
            this.nombre = nombre;
            this.apMat = apMat;
            this.apPat = apPat;
            this.nickname = nickname;
            this.password = password;
        }

        public string Nombre
        {
            set
            {
                this.nombre = value;
            }
            get
            {
                return this.nombre;
            }
        }
        public string ApPat
        {
            set
            {
                this.apPat = value;
            }
            get
            {
                return this.apPat;
            }
        }
        public string ApMat
        {
            set
            {
                this.apMat = value;
            }
            get
            {
                return this.apMat;
            }
        }
        public string Nickname
        {
            set
            {
                this.nickname = value;
            }
            get
            {
                return this.nickname;
            }
        }
        public string Password
        {
            set
            {
                this.password = value;
            }
            get
            {
                return this.password;
            }
        }
      
    }
}
