using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.RespuestaCodi
{
    class CuerpoRespuestaCodi
    {
        public ReturnCode returnCodes { get; set; }
        public JsonResponse jsonResponse { get; set; }
        public string imageResponse { get; set; }
        public string @ref { get; set; }

        public @Exception exception { get; set; }

        public CuerpoRespuestaCodi()
        {
            returnCodes = new ReturnCode();
            jsonResponse = new JsonResponse();
            imageResponse = "";
            @ref = "";
            exception = new @Exception();
        }
    }
}
