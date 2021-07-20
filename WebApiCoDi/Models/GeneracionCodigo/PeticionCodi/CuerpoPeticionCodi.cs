using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi.GeneracionCodigo.PeticionCodi
{
    public class CuerpoPeticionCodi
    {
        public PaymentDetail paymentDetails { get; set; }
	    public ConfigurationDetail configurationDetails { get; set; }

        public CuerpoPeticionCodi()
        {
            paymentDetails = new PaymentDetail();
            configurationDetails = new ConfigurationDetail();
        }
    }
}
