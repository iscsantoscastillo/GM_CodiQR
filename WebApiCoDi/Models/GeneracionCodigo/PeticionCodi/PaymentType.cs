using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.PeticionCodi
{
    public class PaymentType
    {
        public string typ { get; set; }
        public string mdt { get; set; }
        public PaymentType()
        {
            typ = "";
            mdt = "";
        }
    }
}
