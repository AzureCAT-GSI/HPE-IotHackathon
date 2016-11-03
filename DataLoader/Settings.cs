using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
	public class Settings
	{
		public static string REST_ACCESSPOINT = "/api/v1/access_point";
		public static string REST_PRESENCE = "/api/v1/presence";
		public static string REST_CAMPUS = "/api/v1/campus";
		public static string REST_BUILIDNG = "/api/v1/building";
		public static string REST_FLOOR = "/api/v1/floor";
		public static string REST_GEOFENCE = "/api/v1/geo_fence";
		public static string APPKEY_RESTENDPOINT_URL = "REST_ENDPOINT_URL";
		public static string APPKEY_SQL_CONN_STRING = "SQL_CONN_STRING";

		public static string APPKEY_RESTENDPOINT_UID = "RESTENDPOINT_UID";
		public static string APPKEY_RESTENDPOINT_PWD = "RESTENDPOINT_PWD";
	}
}
