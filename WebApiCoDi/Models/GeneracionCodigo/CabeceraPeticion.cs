using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoDi
{
    static class CabeceraPeticion
    {
        public static string ContentType { get; set; } = "application/json";
        //public static string ClientIp_adress { get; set; } = IpConfiguradaVPN;//FALTA
        public static string XHSBCLocale { get; set; } = "es_MX";
        public static string XHSBCChnlCountryCode { get; set; } = "MX";
        public static string XHSBCChnlGroupMember { get; set; } = "HBMX";
        public static string XHSBCUserId { get; set; } = "APP_NAME";
        public static string XHSBCCAMLevel { get; set; } = "NA";
        public static string XHSBCChannelId { get; set; } = "CODI";
        //public static string XHSBCSrcDeviceId { get; set; } = ConsumerDNS; //FALTA
        public static string XHSBCSessionCorrelationId { get; set; } = "SC-APP_NAME-NA";
        public static string XHSBCSrcUserAgent { get; set; } = "Mozilla/5.0(iPad;U;CPUOS3_2_1likeMacOSX;en-us)AppleWebKit/531.21.10(KHTML,likeGecko)Mobile/7B405";
        public static string XHSBCRequestCorrelationId { get; set; } = "RC-APP_NAME-NA";
        public static string XHSBCIPId { get; set; } = "1.1.1.1";
        public static string XHSBCEimId { get; set; } = "9999999999999";


        //cabeceraPeticion.ContentType = "application/json";
        //cabeceraPeticion.ClientIp_adress = IpConfiguradaVPN;
        //cabeceraPeticion.XHSBCLocale = "es_MX";
        //cabeceraPeticion.XHSBCChnlCountryCode = "MX";
        //cabeceraPeticion.XHSBCChnlGroupMember = "HBMX";
        //cabeceraPeticion.XHSBCUserId = "APP_NAME";
        //cabeceraPeticion.XHSBCCAMLevel = "NA";
        //cabeceraPeticion.XHSBCChannelId = "CODI";
        //cabeceraPeticion.XHSBCSrcDeviceId = ConsumerDNS;
        //cabeceraPeticion.XHSBCSessionCorrelationId = "SC-APP_NAME-NA";
        //cabeceraPeticion.XHSBCSrcUserAgent = "Mozilla/5.0(iPad;U;CPUOS3_2_1likeMacOSX;en-us)AppleWebKit/531.21.10(KHTML,likeGecko)Mobile/7B405";
        //cabeceraPeticion.XHSBCRequestCorrelationId = "RC-APP_NAME-NA";
        //cabeceraPeticion.XHSBCIPId = "1.1.1.1";
        //cabeceraPeticion.XHSBCEimId = "9999999999999";

        //public CabeceraPeticion()
        //{
        //    ContentType = "";
        //    ClientIp_adress = "";
        //    XHSBCLocale = "";
        //    XHSBCChnlCountryCode = "";
        //    XHSBCChnlGroupMember = "";
        //    XHSBCUserId = "";
        //    XHSBCCAMLevel = "";
        //    XHSBCChannelId = "";
        //    XHSBCSessionCorrelationId = "";
        //    XHSBCSrcUserAgent = "";
        //    XHSBCRequestCorrelationId = "";
        //    XHSBCIPId = "";
        //    XHSBCEimId = "";
        //}

        //"Content-Type"
        //"Client_ip_adress"
        //"X-HSBC-Locale"
        //"X-HSBC-Chnl-CountryCode"
        //"X-HSBC-Chnl-Group-Member"
        //"X-HSBC-User-Id"
        //"X-HSBC-CAM-Level"
        //"X-HSBC-Channel-Id"
        //"X-HSBC-Src-Device-Id"
        //"X-HSBC-Session-Correlation-Id"
        //"X-HSBC-Src-UserAgent"
        //"X-HSBC-Request-Correlation-Id"
        //"X-HSBC-IP-Id"
        //"X-HSBC-Eim-Id"

    }
}
