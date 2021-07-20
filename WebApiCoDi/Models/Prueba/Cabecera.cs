using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoDi.Models.Prueba
{
    public class Cabecera
    {
        public string clave { get; set; }
        public string valor { get; set; }

        public Cabecera()
        {
            clave = "";
            valor = "";
        }

        public Cabecera(string c, string v)
        {
            this.clave = c;
            this.valor = v;
        }
    }
}
