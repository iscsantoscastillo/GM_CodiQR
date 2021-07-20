using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoDi.Models.Prueba
{
    public class BaseDatos
    {

        public string SERVIDOR_BD { get; set; } = "192.168.123.44";
        public string BASEDATOS_BD { get; set; } = "SoftCredito";
        public string USUARIO_BD { get; set; } = "test";
        public string CONTRASENIA_BD { get; set; } = "GrupoMacro2017";

        public Configuracion ConsultarConfiguracion()
        {
            string cadenaConexion = "Data Source=" + SERVIDOR_BD + ";Initial Catalog=" + BASEDATOS_BD + ";User id=" + USUARIO_BD + ";Password=" + CONTRASENIA_BD;

            SqlConnection conexion = null;
            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();

                SqlCommand comandoActualizaClienteSap = new SqlCommand("SELECT * FROM mpf_configuracion_json_codi");

                Configuracion conf = new Configuracion();

                comandoActualizaClienteSap.Connection = conexion;
                
                SqlDataReader reader= comandoActualizaClienteSap.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        conf.url = reader["url"].ToString();
                        conf.cuerpoPeticionJson = reader["json_cuerpo"].ToString();
                    }
                }
                conexion.Close();
                return conf;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
                throw new Exception(ex.Message);
            }
        }

        public List<Cabecera> ConsultarCabeceras()
        {
            string cadenaConexion = "Data Source=" + SERVIDOR_BD + ";Initial Catalog=" + BASEDATOS_BD + ";User id=" + USUARIO_BD + ";Password=" + CONTRASENIA_BD;

            SqlConnection conexion = null;
            try
            {
                List<Cabecera> cabeceras = new List<Cabecera>();
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();

                SqlCommand comandoActualizaClienteSap = new SqlCommand("SELECT * FROM mpf_cabeceras_codi");
                comandoActualizaClienteSap.Connection = conexion;

                SqlDataReader reader= comandoActualizaClienteSap.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cabecera cab = new Cabecera();

                        cab.clave = reader["cabecera"].ToString();
                        cab.valor = reader["valor"].ToString();
                        cabeceras.Add(cab);
                    }
                }

                conexion.Close();
                return cabeceras;
            }
            catch (Exception ex)
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
                throw new Exception(ex.Message);
            }
        }
    }
}
