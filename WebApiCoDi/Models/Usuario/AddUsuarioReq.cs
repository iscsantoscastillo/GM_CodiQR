using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoDi.Models
{
    public class AddUsuarioReq
    {
        [Required(ErrorMessage = "{0} se requiere para su registro")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "El nombre debe contener almenos 3 caracteres")]
        public string Nombre { get; set; }

        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "El Apellido debe contener almenos 3 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "{0} se requiere para su registro")]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(100, MinimumLength = 8,
        ErrorMessage = "El Password debe contener almenos 8 caracteres")]
        public string Password { get; set; }
    }
}
  