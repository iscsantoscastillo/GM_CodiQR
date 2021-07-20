using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.RespuestaCodi
{
    class ReturnCode
    {
        public string returnCode { get; set; }
		public string application { get; set; }
		public string description { get; set; }

        public ReturnCode()
        {
            returnCode = "";
            application = "";
            description = "";
        }
    }
}
