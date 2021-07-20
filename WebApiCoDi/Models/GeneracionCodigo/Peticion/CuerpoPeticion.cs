using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.Peticion
{
    public class CuerpoPeticion
    {

        public string concepto { get; set; }

        public string referencia { get; set; }
        public double monto { get; set; }

        public CuerpoPeticion()
        {
            concepto = "";
            referencia = "";
            monto = 0;
        }

        public void ValidarPropiedades()
        {
            if (this.concepto == "" || this.concepto.Length > 50)
            {
                throw new Exception("Concepto no válido");
            }
            else if(this.monto <= 0 || this.monto > 8000)
            {
                throw new Exception("Monto  no válido");
            }
        }
    }
}
