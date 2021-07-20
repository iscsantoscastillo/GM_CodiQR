using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoDi.Models
{
    public class RespuestaModel
    {
        public Respuesta Respuesta { get; set; }
        public RespuestaModel()
        {
            Respuesta = new Respuesta();
        }

    }
    public class Respuesta
    {
        public int status { get; set; }
       // public object response { get; set; }
        public string mensaje { get; set; }

        public Respuesta()
        {
            status = 400;

            mensaje = "";
        }
    }
}
