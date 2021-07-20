using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.RespuestaCodi
{
    class @Exception
    {
        public string correlationId { get; set; }
        public string exceptionCode { get; set; }
        public string timeStamp { get; set; }
        public string message { get; set; }

        public @Exception()
        {
            correlationId = "";
            exceptionCode = "";
            timeStamp = "";
            message = "";
        }
    } 
}     
