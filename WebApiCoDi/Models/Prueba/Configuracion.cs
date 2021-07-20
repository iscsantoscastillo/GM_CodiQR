using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoDi.Models.Prueba
{
    public class Configuracion
    {
        public string url { get; set; }
        public string cuerpoPeticionJson { get; set; }

        public Configuracion()
        {
            url = "";
            cuerpoPeticionJson = "";
        }
    }
}
