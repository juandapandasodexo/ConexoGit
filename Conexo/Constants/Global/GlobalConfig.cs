using System;

namespace Global.Constants
{
	public static class GlobalConfig
	{
		// BASE DE DATOS
		public static string DATABASE_NAME = "APP.db";

        //DESARROLLO
        //public static string BASE_URL = "http://172.27.70.7:8087/";
        //public static string BASE_URL = "http://190.144.220.204:8087/";

        //PRODUCCION
        //public static string BASE_URL = "https://sodexoservicios.com.co/";

        //PRUEBAS
        //public static string API = "/ReembolsoEnLineaWS";
        public static string BASE_URL = "https://10.57.34.5";
        public static string API = "";
        //public static string BASE_URL = "http://190.143.125.132"; --PUBLICA INGRESO EXTERNO

        // CACHE KEYS
        public static string TOKEN_KEY = "AccessToken";

	}
}

