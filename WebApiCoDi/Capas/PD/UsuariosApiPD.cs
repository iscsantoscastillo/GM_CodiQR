using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoDi.Models;
using WebApiCoDi.Helpers.Cifrado;
using WebApiCoDi.Capas.AD.ContextModelTables;

namespace WebApiCoDi.Capas.PD
{
    public class UsuariosApiPD
    {
        public async Task<UsuariosApiMacroPay> CrearUsuario(AddUsuarioReq usuariosApi)
        {
          
           
            string pass = usuariosApi.Password;
            Cifrado objFuncion = new Cifrado();
            UsuariosApiMacroPay oNUsuario = new UsuariosApiMacroPay();
            var t = await Task.Run(() =>
            {
            try
            {

                var tkEncrytp = objFuncion.Sha256encrypt(pass);
                if (tkEncrytp.Result != "Error")
                {

                    oNUsuario.Nombre = usuariosApi.Nombre;
                    oNUsuario.Apellido = usuariosApi.Apellido;
                    oNUsuario.EmailUsuario = usuariosApi.Email;
                    oNUsuario.PasswordUsuario = tkEncrytp.Result;
                    oNUsuario.FechaRegistroUsuario = DateTime.Now;
                    oNUsuario.PerfilUsuario = "ADMIN";
                    oNUsuario.StatusUsuario = "AC";
                    oNUsuario.FechaAlta = DateTime.Now;

                    return oNUsuario;
                }
                else
                {
                    return oNUsuario = null;
                }
                }
                catch (Exception e)
                {

                    throw e;
                }
            });
           
            return t;
        }

        public async Task<string> CifrarCadena(string palabra)
        {
            try
            {
                Cifrado objFuncion = new Cifrado();
                return await objFuncion.Sha256encrypt(palabra);
            }
            catch (Exception e)
            {
             ///   return "Error";
                throw e;
            }

        }
    }
}
