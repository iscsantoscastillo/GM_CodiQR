using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebApiCoDi.Capas.AD
{
    public class ConexionAD
    {
        public string cnCadena()
        {
            var configuation = GetConfiguration();
            string con = configuation.GetSection("ConnectionStrings").GetSection("ConexionBD").Value.ToString();
            return con;
        }

        public string cnCadena(string basedatos)
        {
            var configuation = GetConfiguration();
            string con = configuation.GetSection("ConnectionStrings").GetSection(basedatos).Value.ToString();
            return con;
        }

        public string keyToken()
        {
            var configuation = GetConfiguration();
            string con = configuation.GetSection("AppSettings").GetSection("KeyJwt").Value.ToString();
            return con;
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
