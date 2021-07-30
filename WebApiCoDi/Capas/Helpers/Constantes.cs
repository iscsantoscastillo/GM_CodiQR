using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoDi.Capas.Helpers
{
    public static class Constantes
    {
        public const string X_HSBC_CAM_Level = "X-HSBC-CAM-Level";
        public const string X_HSBC_Channel_Id = "X-HSBC-Channel-Id";
        public const string X_HSBC_Chnl_CountryCode = "X-HSBC-Chnl-CountryCode";
        public const string X_HSBC_Chnl_Group_Member = "X-HSBC-Chnl-Group-Member";
        public const string X_HSBC_Eim_Id = "X-HSBC-Eim-Id";
        public const string X_HSBC_IP_Id = "X-HSBC-IP-Id";
        public const string X_HSBC_Locale = "X-HSBC-Locale";
        public const string X_HSBC_Request_Correlation_Id = "X-HSBC-Request-Correlation-Id";
        public const string X_HSBC_Session_Correlation_Id = "X-HSBC-Session-Correlation-Id";
        public const string X_HSBC_Src_Device_Id = "X-HSBC-Src-Device-Id";
        public const string X_HSBC_Src_UserAgent = "X-HSBC-Src-UserAgent";
        public const string X_HSBC_User_Id = "X-HSBC-User-Id";
        public const string Client_ip_adress = "Client_ip_adress";
        public const string Cache_Control = "Cache-Control";
        public const string Content_Type = "Content-Type";
        public const string TiempoExpiracionDias = "TiempoExpiracionDias";


        public const int TIEMPO_1_HORA_EN_SEGUNDOS = 3600;
        public const int TIEMPO_1_DIA_EN_MILISEGUNDOS = 86400000;
        public const int TIEMPO_1_SEMANA_EN_SEGUNDOS = 604800;
        public const int TIEMPO_1_MES_EN_SEGUNDOS = 2629743;
        public const int TIEMPO_1_ANIO_EN_SEGUNDOS = 31556926;
        
        public const string DETALLE_COM = "com";
        public const string DETALLE_REF = "ref";
        public const string DETALLE_NAM = "v_nam";
        public const string DETALLE_ACC = "v_acc";
        public const string DETALLE_BAN = "v_ban";
        public const string DETALLE_TYC = "v_tyc";
        public const string DETALLE_TYP = "pt_typ";
        public const string DETALLE_ALIAS = "c_alias";
        public const string DETALLE_DV = "c_dv";
        public const string DETALLE_KEYSOURCE = "c_keysource";
        public const string DETALLE_RESPONSE_TYPE = "c_responseType";
        public const string DETALLE_QR_SIZE = "c_qrSize";
        
        //Conexiones a Bases de datos
        public const string BD_SOFT = "ConexionBD";
        public const string BD_CORP = "ConexionBDCorporativo";
        public const int ENTERO_CERO = 0;
        public const int ENTERO_UNO = 1;

    }
}
