using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoDi.Repository
{
    public interface ISolicitudRepo
    {
        bool existeSolicitud(string claveSolicitud);
    }
}
