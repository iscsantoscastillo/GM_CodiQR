using System;
using System.Collections.Generic;

namespace WebApiCoDi.Capas.AD.ContextModelTables
{
    public partial class UsuariosApiMacroPay
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string EmailUsuario { get; set; }
        public string PerfilUsuario { get; set; }
        public string StatusUsuario { get; set; }
        public string PasswordUsuario { get; set; }
        public DateTime? FechaRegistroUsuario { get; set; }
        public string Token { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaModificado { get; set; }
    }
}
