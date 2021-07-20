using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.PeticionCodi
{
    public class ConfigurationDetail
    {
        public string alias { get; set; }
        public string dv { get; set; }
        public string keysource { get; set; }
        public string responseType { get; set; }
        public string qrSize { get; set; }

        public ConfigurationDetail()
        {
            alias = "";
            dv = "";
            keysource = "";
            responseType = "";
            qrSize = "";
        }
    }
}
