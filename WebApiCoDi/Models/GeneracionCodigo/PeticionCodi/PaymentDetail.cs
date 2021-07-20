using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.PeticionCodi
{
    public class PaymentDetail
    {

        public string des { get; set; }
        public string amo { get; set; }
        public string com { get; set; }
        public string @ref { get; set; }
        public V v { get; set; }
        public PaymentType paymentType { get; set; }

        public PaymentDetail()
        {
            des = "";
            amo = "";
            com = "";
            @ref = "";
            v = new V();
        }
    }
}
