using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiCoDi.Capas.AD;
using WebApiCoDi.Capas.AD.ContextDataBase;
using WebApiCoDi.Capas.AD.ContextModelTables;
using WebApiCoDi.Capas.PD;
using WebApiCoDi.Helpers.Mensajes;
using WebApiCoDi.Models;

namespace WebApiCoDi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UsuariosApiCodiController : Controller
    {
        private readonly SoftCreditoContext _context;
        private IConfiguration _configuration;
        public UsuariosApiCodiController(SoftCreditoContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }
        private MensajeRespuesta objMenRespuesta = new MensajeRespuesta();
        private Task<RespuestaModel> mensajeR;

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "UsuariosApi" };
        }

        // GET api/<UsuariosApiMacroPayController>/5
        [Route("getAllUsuarios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // GET: api/UsuariosApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosApiMacroPay>>> GetUsuariosApi()
        {
            return await _context.UsuariosApiMacroPay.ToListAsync();
        }

        // POST api/<UsuariosApiMacroPayController>

        // POST: api/UsuariosApi

        [Route("addUsuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<RespuestaModel>> PostUsuariosApi([FromBody] AddUsuarioReq usuariosApi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuariosApiPD oUsuarioPD = new UsuariosApiPD();
                    UsuariosApiMacroPay mUsuario = await oUsuarioPD.CrearUsuario(usuariosApi);
                    if (mUsuario != null)
                    {
                        _context.UsuariosApiMacroPay.Add(mUsuario);
                        await _context.SaveChangesAsync();


                        mensajeR = objMenRespuesta.ArmarMensajeRespuesta(200, "Usuario Creado con Exito");
                        return Ok(mensajeR.Result);

                        //return CreatedAtAction("GetUsuarioApi", new { id = mUsuario.IdUsuario }, mUsuario);
                    }
                    else
                    {
                        mensajeR = objMenRespuesta.ArmarMensajeRespuesta(NotFound().StatusCode, "Hubo un problema al intentar Registrar al Usuario, Intente de nuevo si el problema continua contacte al administrador");

                        return NotFound(mensajeR.Result);
                    }
                }
                catch (Exception e)
                {

                    mensajeR = objMenRespuesta.ArmarMensajeRespuesta(NotFound().StatusCode, "Hubo un problema al intentar Registrar al Usuario, Intente de nuevo si el problema continua contacte al administrador: " + e.Message);


                    return NotFound(mensajeR.Result);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosApiMacroPay>> GetUsuarioApi(int id)
        {
            var usuariosApi = await _context.UsuariosApiMacroPay.FindAsync(id);

            if (usuariosApi == null)
            {
                mensajeR = objMenRespuesta.ArmarMensajeRespuesta(Ok().StatusCode, "No se encontro al Usuario con id: " + id);
                // jsonMensaje = JsonSerializer.Serialize<RespuestaModel>(mensajeR.Result, options);
                return Ok(mensajeR.Result);
            }

            return usuariosApi;
        }

        private bool UsuariosApiExists(int id)
        {
            return _context.UsuariosApiMacroPay.Any(e => e.IdUsuario == id);
        }

        // DELETE api/<UsuariosApiMacroPayController>/5
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public async Task<ActionResult<RespuestaModel>> DeleteUsuariosApi(int id)
        //{
        //    var usuariosApi = await _context.UsuariosApiMacroPay.FindAsync(id);
        //    if (usuariosApi == null)
        //    {
        //        mensajeR = objMenRespuesta.ArmarMensajeRespuesta(Ok().StatusCode, "No se encontro al Usuario con id: " + id);
        //        // jsonMensaje = JsonSerializer.Serialize<RespuestaModel>(mensajeR.Result, options);
        //        return Ok(mensajeR.Result);
        //    }

        //    _context.UsuariosApiMacroPay.Remove(usuariosApi);
        //    await _context.SaveChangesAsync();

        //    mensajeR = objMenRespuesta.ArmarMensajeRespuesta(Ok().StatusCode, "Se elimino al usuario con id: " + id);

        //    return Ok(mensajeR.Result);

        //}

        [HttpPost]
        [Route("generarTokenSesion")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerarToken([FromBody] inicioSesionReq requsuario)
        {
            if (ModelState.IsValid)
            {
                IActionResult resultado = null;
                string pass = requsuario.Password;
                string passCifrado = "";
                try
                {
                    UsuariosApiPD oUsuarioPD = new UsuariosApiPD();
                    var resultTK = oUsuarioPD.CifrarCadena(pass);
                    passCifrado = resultTK.Result;
                    if (passCifrado != "Error")
                    {
                        using (var context = new SoftCreditoContext())
                        {
                            var query = from us in context.UsuariosApiMacroPay
                                        where us.EmailUsuario == requsuario.Email
                                        where us.PasswordUsuario == passCifrado

                                        select us;

                            UsuariosApiMacroPay usuario = await query.FirstOrDefaultAsync<UsuariosApiMacroPay>();

                            if (usuario is null)
                            {
                                mensajeR = objMenRespuesta.ArmarMensajeRespuesta(Ok().StatusCode, "No se encontro al usuario proporcionado");

                                resultado = Ok(mensajeR.Result);
                            }
                            else
                            {
                                resultado = BuildToken(usuario);
                            }
                        }
                        return resultado;
                    }
                    else
                    {
                        mensajeR = objMenRespuesta.ArmarMensajeRespuesta(NotFound().StatusCode, "Hubo un problema al  intentar obtener Token, Intente de nuevo si el problema continua contacte al administrador");

                        return NotFound(mensajeR.Result);
                    }
                }
                catch (Exception e)
                {
                    mensajeR = objMenRespuesta.ArmarMensajeRespuesta(NotFound().StatusCode, "Hubo un problema al  intentar obtener Token, Intente de nuevo si el problema continua contacte al administrador: " + e.Message);

                    return NotFound(mensajeR.Result);
                    ;
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private IActionResult BuildToken(UsuariosApiMacroPay userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.EmailUsuario),
                new Claim("llave", "R3J1cG9NYWNybzIwMjA="),//GrupoMacro2020
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            ConexionAD oCAD = new ConexionAD();
            string stoken = oCAD.keyToken();
            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["key_Ambiente"]));
            var key =  new SymmetricSecurityKey( Encoding.ASCII.GetBytes(stoken));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //tiempo de vida de Token
            var expiration = DateTime.UtcNow.AddHours(2);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "grupomacro.mx",
               audience: "grupomacro.mx",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });

        }

    }
}
