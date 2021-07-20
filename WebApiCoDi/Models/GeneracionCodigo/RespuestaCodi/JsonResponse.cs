using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.RespuestaCodi
{
    class JsonResponse
    {
        public int TYP { get; set; }
        public V v { get; set; }
		public Ic ic { get; set; }
		public string CRY { get; set; }
    }
}
