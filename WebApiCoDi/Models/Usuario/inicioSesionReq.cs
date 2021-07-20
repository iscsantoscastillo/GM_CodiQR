using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoDi.Models
{
    public class inicioSesionReq
    {
        [Required(ErrorMessage = "{0} se requiere para el inicio de sesion")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} se requiere para el inicio de sesion")]
        public string Password { get; set; }
    }
}
