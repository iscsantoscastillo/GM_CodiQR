using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoDi.Models;

namespace WebApiCoDi.Helpers.Mensajes
{
    public class MensajeRespuesta
    {
        public async Task<RespuestaModel> ArmarMensajeRespuesta(int code, string mensaje)
        {
            var t = await Task.Run(() =>
            {
                RespuestaModel obResM = new RespuestaModel();
                obResM.Respuesta = new Respuesta();
                obResM.Respuesta.status = code;
                obResM.Respuesta.mensaje = mensaje;

            return obResM;
            });
            return t;
        }
    }
}
