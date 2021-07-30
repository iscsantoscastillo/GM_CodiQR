using Microsoft.Data.SqlClient;
using NLog;
using System;
using System.Data;
using WebApiCoDi.Capas.AD;
using WebApiCoDi.Capas.Helpers;

namespace WebApiCoDi.Repository
{
    public class SolicitudRepoImpl : ISolicitudRepo
    {
        Logger log = LogManager.GetCurrentClassLogger();
        public bool existeSolicitud(string claveSolicitud) {
            bool existe = false;
            ConexionAD conexion = new ConexionAD();
            try
            {
                using (SqlConnection cnn = new SqlConnection(conexion.cnCadena(Constantes.BD_SOFT)))
                {
                    cnn.Open();
                    string sp = "sp_sfc_existe_solicitud";
                    using (SqlCommand sqlCommand = new SqlCommand(sp, cnn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add("@clave_solicitud", SqlDbType.VarChar);
                        sqlCommand.Parameters["@clave_solicitud"].Value = claveSolicitud;

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.HasRows)
                            {                                
                                log.Info("Se encontró la información.");

                                while (sqlDataReader.Read())
                                {
                                    int x = Int32.Parse(sqlDataReader["existe"].ToString());
                                    existe = x == Constantes.ENTERO_CERO ? false : true; //0 = No existe, 1 = Sí existe
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                log.Error("Ocurrió un error al momento de consultar la información en el servidor: " + ex.Message);
                throw new Exception(ex.Message);
            }

            return existe;
        }
    
    }
}
