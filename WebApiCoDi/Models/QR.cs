using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using WebApiCoDi.GeneracionCodigo.Peticion;
using WebApiCoDi.GeneracionCodigo.PeticionCodi;
using WebApiCoDi.GeneracionCodigo.RespuestaCodi;
using CuerpoPeticionCodi = WebApiCoDi.GeneracionCodigo.PeticionCodi.CuerpoPeticionCodi;
using Microsoft.Extensions.Configuration;
using WebApiCoDi.GeneracionCodigo.Respuesta;
using WebApiCoDi.Models.Prueba;
using WebApiCoDi.Capas.Helpers;
using System.Text.Json;
using NLog;

namespace WebApiCoDi.Models
{
    public class QR
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        public static CuerpoRespuesta GenerarQRDatosBase(CuerpoPeticion cuerpoPeticion)
        {
            try
            {
                //Se obtiene el actual epoch time del sistema
                var epochTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                //Se agrega tiempo de expiración representado en mili-segundos
                var fechaExpiracion = epochTime + DatosHelper.LeerTiempoExpiracion();                
                List<Cabecera> cabecer = new List<Cabecera>();
               
                cabecer = DatosHelper.LeerDatosCabecera();
                
                //Armar objeto para serializar en string
                PaymentDetail paymentDetails = new PaymentDetail();
                paymentDetails.des = cuerpoPeticion.referencia;
                paymentDetails.amo = cuerpoPeticion.monto.ToString();
                paymentDetails.com = DatosHelper.LeerDetalle(Constantes.DETALLE_COM);//fijo
                paymentDetails.@ref = DatosHelper.LeerDetalle(Constantes.DETALLE_REF);//fijo
                
                paymentDetails.v = new GeneracionCodigo.PeticionCodi.V();
                paymentDetails.v.nam = DatosHelper.LeerDetalle(Constantes.DETALLE_NAM);//?
                paymentDetails.v.acc = DatosHelper.LeerDetalle(Constantes.DETALLE_ACC);//?
                paymentDetails.v.ban = DatosHelper.LeerDetalle(Constantes.DETALLE_BAN);//fijo
                paymentDetails.v.tyc = DatosHelper.LeerDetalle(Constantes.DETALLE_TYC);//fijo
                
                paymentDetails.paymentType = new PaymentType();
                paymentDetails.paymentType.typ = DatosHelper.LeerDetalle(Constantes.DETALLE_TYP);//fijo
                paymentDetails.paymentType.mdt = fechaExpiracion.ToString();//fecha expiración

                ConfigurationDetail configurationDetails = new ConfigurationDetail();
                configurationDetails.alias = DatosHelper.LeerDetalle(Constantes.DETALLE_ALIAS);//?
                configurationDetails.dv = DatosHelper.LeerDetalle(Constantes.DETALLE_DV);//?
                configurationDetails.keysource = DatosHelper.LeerDetalle(Constantes.DETALLE_KEYSOURCE);//?
                configurationDetails.responseType = DatosHelper.LeerDetalle(Constantes.DETALLE_RESPONSE_TYPE);
                configurationDetails.qrSize = DatosHelper.LeerDetalle(Constantes.DETALLE_QR_SIZE);//fijo
                var obj = (new
                {
                    paymentDetails,
                    configurationDetails
                });
                
                var cadena = System.Text.Json.JsonSerializer.Serialize(obj);
                log.Info("JSON SIN-OPCIONES: " + cadena);

                JsonSerializerOptions jso = new JsonSerializerOptions();
                jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                cadena = System.Text.Json.JsonSerializer.Serialize(obj, jso);
                log.Info("JSON CON-OPCIONES: " + cadena);

                //Tomar URL (appsettings.json) y JSON (dinamico)
                Configuracion config = new Configuracion();
                config.url = DatosHelper.LeerServicio("url");
                config.cuerpoPeticionJson = cadena;
                
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(config.url);
                request.Method = "POST";

                foreach (var cab in cabecer)
                {
                    request.Headers.Add(cab.clave, cab.valor);
                }

                byte[] postBytes = Encoding.UTF8.GetBytes(config.cuerpoPeticionJson);

                request.ContentType = "application/json; charset=UTF-8";

                request.Accept = "application/json";
                request.ContentLength = postBytes.Length;
                Stream requestStream = request.GetRequestStream();


                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();


                //SetAllowUnsafeHeaderParsing20();
                System.Net.HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                string result;
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                    log.Info("result: " + result);
                    CuerpoRespuestaCodi respuesta = JsonConvert.DeserializeObject<CuerpoRespuestaCodi>(Convert.ToString(result));
                    if (respuesta.returnCodes.returnCode == "00")
                    {
                        CuerpoRespuesta respuestaFinal = new CuerpoRespuesta();
                        respuestaFinal.hayError = false;
                        respuestaFinal.mensajeError = "";
                        respuestaFinal.qrBase64 = respuesta.imageResponse;

                        return respuestaFinal;

                    }
                    else
                    {
                        CuerpoRespuesta respuestaFinal = new CuerpoRespuesta();
                        respuestaFinal.hayError = true;
                        respuestaFinal.mensajeError = "Ocurrió el siguiente error:[" + respuesta.returnCodes.returnCode + "] " + respuesta.returnCodes.description;
                        respuestaFinal.qrBase64 = "";

                        return respuestaFinal;
                    }
                }
            }
            catch (System.Exception ex)
            {
                log.Error("Ocurrió un error: " + ex.Message);
                string err = "";
                err = ex.Message;

                if (ex.Message.Length > 250)
                {
                    err = ex.Message.Substring(0, 249);
                }

                //Console.WriteLine("Error:" + ex.Message);

                CuerpoRespuesta respuestaFinal = new CuerpoRespuesta();
                respuestaFinal.hayError = true;
                respuestaFinal.mensajeError = ex.Message;
                respuestaFinal.qrBase64 = "";
                return respuestaFinal;
            }
        }

        public static CuerpoRespuesta GenerarQR(CuerpoPeticion cuerpoPeticion)
        {
            try
            {
                var configuration = GetConfiguration();
                string url= configuration.GetSection("Codi").GetSection("Servicio").GetSection("NombreVendedorCodi").Value.ToString();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";

                request.Headers.Add("Content-Type", CabeceraPeticion.ContentType);
                request.Headers.Add("Client_ip_adress", configuration.GetSection("Codi").GetSection("Servicio").GetSection("ClientIpAdress").Value.ToString());
                request.Headers.Add("X-HSBC-Locale", CabeceraPeticion.XHSBCLocale);
                request.Headers.Add("X-HSBC-Chnl-CountryCode", CabeceraPeticion.XHSBCChnlCountryCode);
                request.Headers.Add("X-HSBC-Chnl-Group-Member", CabeceraPeticion.XHSBCChnlGroupMember);
                request.Headers.Add("X-HSBC-User-Id", CabeceraPeticion.XHSBCUserId);
                request.Headers.Add("X-HSBC-CAM-Level", CabeceraPeticion.XHSBCCAMLevel);
                request.Headers.Add("X-HSBC-Channel-Id", CabeceraPeticion.XHSBCChannelId);
                request.Headers.Add("X-HSBC-Src-Device-Id", configuration.GetSection("Codi").GetSection("Servicio").GetSection("XHSBCSrcDeviceId").Value.ToString());
                request.Headers.Add("X-HSBC-Session-Correlation-Id", CabeceraPeticion.XHSBCSessionCorrelationId);
                request.Headers.Add("X-HSBC-Src-UserAgent", CabeceraPeticion.XHSBCSrcUserAgent);
                request.Headers.Add("X-HSBC-Request-Correlation-Id", CabeceraPeticion.XHSBCRequestCorrelationId);
                request.Headers.Add("X-HSBC-IP-Id", CabeceraPeticion.XHSBCIPId);
                request.Headers.Add("X-HSBC-Eim-Id", CabeceraPeticion.XHSBCEimId);

                CuerpoPeticionCodi peticionCodi = new CuerpoPeticionCodi();

                GeneracionCodigo.PeticionCodi.V v = new GeneracionCodigo.PeticionCodi.V();
                v.nam = configuration.GetSection("Codi").GetSection("Servicio").GetSection("NombreVendedorCodi").Value.ToString();
                v.acc = configuration.GetSection("Codi").GetSection("Servicio").GetSection("CuentaVendedorCodi").Value.ToString();
                v.ban = configuration.GetSection("Codi").GetSection("Servicio").GetSection("CuentaSpeiVendedor").Value.ToString();
                v.tyc = configuration.GetSection("Codi").GetSection("Servicio").GetSection("TipoCuentaVendedor").Value.ToString();

                PaymentType paymentType = new PaymentType();
                paymentType.typ = configuration.GetSection("Codi").GetSection("Servicio").GetSection("CodigoCobro").Value.ToString();
                TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
                double unixTime = span.TotalSeconds;
                string fechaLimitePago = Convert.ToUInt64(unixTime).ToString();

                paymentType.mdt = fechaLimitePago;//si son cobros recurrentes, cual seria la fecha unix que se podria usar?


                PaymentDetail paymentDetail = new PaymentDetail();
                paymentDetail.des = cuerpoPeticion.concepto;
                paymentDetail.amo = cuerpoPeticion.monto.ToString();
                paymentDetail.com = configuration.GetSection("Codi").GetSection("Servicio").GetSection("PagaComision").Value.ToString();
                paymentDetail.@ref = cuerpoPeticion.referencia;
                paymentDetail.v = v;
                paymentDetail.paymentType = paymentType;

                peticionCodi.paymentDetails = paymentDetail;

                ConfigurationDetail configurationDetail = new ConfigurationDetail();
                configurationDetail.alias = configuration.GetSection("Codi").GetSection("Servicio").GetSection("AliasNumeroSerieCertificado").Value.ToString();
                configurationDetail.dv = configuration.GetSection("Codi").GetSection("Servicio").GetSection("DigitoVerificadorBancoMexico").Value.ToString();
                configurationDetail.keysource = configuration.GetSection("Codi").GetSection("Servicio").GetSection("KeySource").Value.ToString();
                configurationDetail.responseType = configuration.GetSection("Codi").GetSection("Servicio").GetSection("TipoRespuesta").Value.ToString();
                configurationDetail.qrSize = configuration.GetSection("Codi").GetSection("Servicio").GetSection("QrSize").Value.ToString();

                peticionCodi.configurationDetails = configurationDetail;

                var jsonPeticion = JsonConvert.SerializeObject(peticionCodi);

                byte[] postBytes = Encoding.UTF8.GetBytes(jsonPeticion);

                request.ContentType = "application/json; charset=UTF-8";

                request.Accept = "application/json";
                request.ContentLength = postBytes.Length;
                Stream requestStream = request.GetRequestStream();


                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();


                //SetAllowUnsafeHeaderParsing20();
                System.Net.HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                string result;
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                    CuerpoRespuestaCodi respuesta = JsonConvert.DeserializeObject<CuerpoRespuestaCodi>(Convert.ToString(result));
                    if (respuesta.returnCodes.returnCode == "00")
                    {
                        CuerpoRespuesta respuestaFinal = new CuerpoRespuesta();
                        respuestaFinal.hayError = false;
                        respuestaFinal.mensajeError = "";
                        respuestaFinal.qrBase64 = respuesta.imageResponse;

                        return respuestaFinal;

                    }
                    else
                    {
                        CuerpoRespuesta respuestaFinal = new CuerpoRespuesta();
                        respuestaFinal.hayError = true;
                        respuestaFinal.mensajeError = "Ocurrió el siguiente error:[" + respuesta.returnCodes.returnCode +"] " + respuesta.returnCodes.description;
                        respuestaFinal.qrBase64 = "";
                        
                        return respuestaFinal;
                    }
                }
            }
            catch (System.Exception ex)
            {
                log.Error("Ocurrió un error: " + ex.Message);
                string err = "";
                err = ex.Message;

                if (ex.Message.Length > 250)
                {
                    err = ex.Message.Substring(0, 249);
                }

                //Console.WriteLine("Error:" + ex.Message);

                CuerpoRespuesta respuestaFinal = new CuerpoRespuesta();
                respuestaFinal.hayError = true;
                respuestaFinal.mensajeError = ex.Message;
                respuestaFinal.qrBase64 = "";
                return respuestaFinal;
            }
        }

        ////void IniciarVariables()
        ////{
        ////    string URLWebService = "https://api-idmz.oat.mx.hsbc/hbmx-rpio/codi/v1/payment-message"; //ip : 172.22.65.193
        ////    string IpConfiguradaVPN = "";
        ////    string ConsumerDNS = "";


        ////    string descripcionServicio = "SL202100009065 abono macropay"; //40 caracteres sin carac especiales
        ////    int montoCobro = 100;
        ////    string NumeroDeOrden = "1548754"; // problema con macropay tiene muchos caracteres //clave asignada por webserice?

        ////    int pagaComision = 2; //1 cliente emisor,2 cliente beneficiario
        ////    string nombreVendedorCodi = "ALGO"; //vendedor de codi, donde se toma?
        ////    string cuentaVendedorCodi = "ALGO"; //donde se toma?
        ////    string cuentaSPeiVendedor = "algo";// se va usar, donde se toma? ¿si no se usa no se pondría este campo en el json?
        ////    string tipoCuentaVendedor = "40"; //2 caracteres max, se puso fijo con valor 40, ¿ese valor quedará?
        ////    string CodigoCobro = "20"; //20 para cobros de una ocasion,21 para cobros recurrentes, ¿cual quedaría:20??
        ////    string aliasNumeroSerieCertificado = "ALGO"; // alias del numero de serie del certificado asignado por el Banco mundial ¿Donde se toma?
        ////    string digitoVerificadorBancoMexico = "ALGO"; //Digito verificador asignado por el banco de mexico , ¿Donde se toma?
        ////    string keySource = "ALGO"; //Se asigna por el banco de méxico al registro ,¿Donde se toma?
        ////    string tipoRespuesta = "JSON"; //tipo de respuesta //JSON,IMAGE,BOTH , hay que probar como funciona ¿Solo es para el campo de codigo de barras?
        ////    string qrSize = "250"; //tamanño del QR en pixeles

        ////    //la fecha de vencimiento cuando son pagos recurrentes que fecha se pondría?

        ////    //CabeceraPeticion cabeceraPeticion = new CabeceraPeticion();

        ////    //cabeceraPeticion.ContentType = "application/json";
        ////    //cabeceraPeticion.ClientIp_adress = IpConfiguradaVPN;
        ////    //cabeceraPeticion.XHSBCLocale = "es_MX";
        ////    //cabeceraPeticion.XHSBCChnlCountryCode = "MX";
        ////    //cabeceraPeticion.XHSBCChnlGroupMember = "HBMX";
        ////    //cabeceraPeticion.XHSBCUserId = "APP_NAME";
        ////    //cabeceraPeticion.XHSBCCAMLevel = "NA";
        ////    //cabeceraPeticion.XHSBCChannelId = "CODI";
        ////    //cabeceraPeticion.XHSBCSrcDeviceId = ConsumerDNS;
        ////    //cabeceraPeticion.XHSBCSessionCorrelationId = "SC-APP_NAME-NA";
        ////    //cabeceraPeticion.XHSBCSrcUserAgent = "Mozilla/5.0(iPad;U;CPUOS3_2_1likeMacOSX;en-us)AppleWebKit/531.21.10(KHTML,likeGecko)Mobile/7B405";
        ////    //cabeceraPeticion.XHSBCRequestCorrelationId = "RC-APP_NAME-NA";
        ////    //cabeceraPeticion.XHSBCIPId = "1.1.1.1";
        ////    //cabeceraPeticion.XHSBCEimId = "9999999999999";

        ////    CuerpoPeticionCodi cuerpoPeticion = new CuerpoPeticionCodi();

        ////    GeneracionCodigo.PeticionCodi.V v = new GeneracionCodigo.PeticionCodi.V();
        ////    v.nam = nombreVendedorCodi;
        ////    v.acc = cuentaVendedorCodi;
        ////    v.ban = cuentaSPeiVendedor;
        ////    v.tyc = tipoCuentaVendedor;

        ////    PaymentType paymentType = new PaymentType();
        ////    paymentType.typ = CodigoCobro;
        ////    TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        ////    double unixTime = span.TotalSeconds;
        ////    string fechaLimitePago = Convert.ToUInt64(unixTime).ToString();

        ////    paymentType.mdt = fechaLimitePago;//si son cobros recurrentes, cual seria la fecha unix que se podria usar?


        ////    PaymentDetail paymentDetail = new PaymentDetail();
        ////    paymentDetail.des = descripcionServicio;
        ////    paymentDetail.amo = montoCobro.ToString();
        ////    paymentDetail.com = pagaComision.ToString();
        ////    paymentDetail.@ref = NumeroDeOrden;
        ////    paymentDetail.v = v;
        ////    paymentDetail.paymentType = paymentType;

        ////    cuerpoPeticion.paymentDetails = paymentDetail;

        ////    ConfigurationDetail configurationDetail = new ConfigurationDetail();
        ////    configurationDetail.alias = aliasNumeroSerieCertificado;
        ////    configurationDetail.dv = digitoVerificadorBancoMexico;
        ////    configurationDetail.keysource = keySource;
        ////    configurationDetail.responseType = tipoRespuesta;
        ////    configurationDetail.qrSize = qrSize;

        ////    cuerpoPeticion.configurationDetails = configurationDetail;


        ////    GenerarQR(URLWebService, cuerpoPeticion);
        ////}

        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
