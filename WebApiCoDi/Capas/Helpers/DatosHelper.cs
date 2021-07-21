using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoDi.Models.Prueba;

namespace WebApiCoDi.Capas.Helpers
{
    public static class DatosHelper
    {
        /// <summary>
        /// Método que lee del archivo appsettings.json datos para el cabecero HSBC
        /// </summary>
        /// <returns>Retorna una lista de de objetos Cabecera</returns>
        public static List<Cabecera> LeerDatosCabecera() {
            
            List<Cabecera> cabeceras = new List<Cabecera>();
            
            var configuation = GetConfiguration();

            cabeceras.Add(new Cabecera(Constantes.X_HSBC_CAM_Level, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_CAM_Level).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_Channel_Id, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_Channel_Id).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_Chnl_CountryCode, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_Chnl_CountryCode).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_Chnl_Group_Member, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_Chnl_Group_Member).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_Eim_Id, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_Eim_Id).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_IP_Id, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_IP_Id).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_Locale, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_Locale).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_Request_Correlation_Id, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_Request_Correlation_Id).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_Session_Correlation_Id, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_Session_Correlation_Id).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_Src_Device_Id, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_Src_Device_Id).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_Src_UserAgent, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_Src_UserAgent).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.X_HSBC_User_Id, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.X_HSBC_User_Id).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.Client_ip_adress, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.Client_ip_adress).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.Cache_Control, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.Cache_Control).Value.ToString())); 
            cabeceras.Add(new Cabecera(Constantes.Content_Type, configuation.GetSection("Codi").GetSection("Cabecera").GetSection(Constantes.Content_Type).Value.ToString())); 
            
            return cabeceras;
        }

        /// <summary>
        /// Se obtiene el tiempo de expiracion en días
        /// </summary>
        /// <returns>Retorna el numero de días que tendrá de vigencia representado en segundos</returns>
        public static int LeerTiempoExpiracion() {
            var configuation = GetConfiguration();

            return Int32.Parse(configuation.GetSection("Codi").GetSection("Expiracion").GetSection(Constantes.TiempoExpiracionDias).Value.ToString()) * Constantes.TIEMPO_1_DIA_EN_MILISEGUNDOS;

        }

        public static string LeerDetalle(string sec) {
            var configuation = GetConfiguration();
            return configuation.GetSection("Codi").GetSection("Detalle").GetSection(sec).Value.ToString();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
