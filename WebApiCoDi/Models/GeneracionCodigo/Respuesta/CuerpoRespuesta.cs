using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.Respuesta
{
    public class CuerpoRespuesta
    {
        public bool hayError { get; set; }
        public string mensajeError { get; set; }
        public string qrBase64 { get; set; }

        public CuerpoRespuesta()
        {
            hayError = false;
            mensajeError = "";
            qrBase64 = "";
        }
    }
}
