using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Azure;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;
using DataLoader.Models;
using DataLoader.access_point;
using DataLoader.Models.presence;
using DataLoader.Models.campus;
using DataLoader.Models.building;
using DataLoader.Models.floor;
using DataLoader.Models.geo_fence;

namespace DataLoader
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("Begin dataload or enter 'EXIT' to end?");
				string userResponse = Console.ReadLine();

				if (!string.IsNullOrEmpty(userResponse))
				{
					if (string.Compare(userResponse, "y", true) == 0)
					{
						#region "ACCESS POINTS"
						string currentRestUrl = string.Format("{0}{1}", CloudConfigurationManager.GetSetting(Settings.APPKEY_RESTENDPOINT_URL), Settings.REST_ACCESSPOINT);
						string currentData = getJsonData(currentRestUrl);
						Access_point_results apData = JsonConvert.DeserializeObject<Access_point_results>(currentData);

						foreach (Access_point_result result in apData.Access_point_result)
						{
							//Write the data
							writeAPData(result);
						}
						#endregion

						#region "PRESENCE"
						currentRestUrl = string.Format("{0}{1}", CloudConfigurationManager.GetSetting(Settings.APPKEY_RESTENDPOINT_URL), Settings.REST_PRESENCE);
						currentData = getJsonData(currentRestUrl);
						PresenceResults presenceData = JsonConvert.DeserializeObject<PresenceResults>(currentData);

						foreach (PresenceResult result in presenceData.Presence_result)
						{
							//Write the data
							writePresenceData(result);
						}
						#endregion

						#region "CAMPUS"
						currentRestUrl = string.Format("{0}{1}", CloudConfigurationManager.GetSetting(Settings.APPKEY_RESTENDPOINT_URL), Settings.REST_CAMPUS);
						currentData = getJsonData(currentRestUrl);
						CampusResults campusData = JsonConvert.DeserializeObject<CampusResults>(currentData);

						foreach (CampusResult result in campusData.Campus_result)
						{
							//Write the data
							writeCampusData(result);
						}
						#endregion

						#region "BUILDING"
						currentRestUrl = string.Format("{0}{1}", CloudConfigurationManager.GetSetting(Settings.APPKEY_RESTENDPOINT_URL), Settings.REST_BUILIDNG);
						currentData = getJsonData(currentRestUrl);
						BuildingResults buildingData = JsonConvert.DeserializeObject<BuildingResults>(currentData);

						foreach (BuildingResult result in buildingData.Building_result)
						{
							//Write the data
							writeBuldingData(result);
						}
						#endregion

						#region "FLOOR"
						currentRestUrl = string.Format("{0}{1}", CloudConfigurationManager.GetSetting(Settings.APPKEY_RESTENDPOINT_URL), Settings.REST_FLOOR);
						currentData = getJsonData(currentRestUrl);
						FloorResults floorData = JsonConvert.DeserializeObject<FloorResults>(currentData);

						foreach (FloorResult result in floorData.Floor_result)
						{
							//Write the data
							writeFloorData(result);
						}
						#endregion

						#region "GEOFENCE"
						currentRestUrl = string.Format("{0}{1}", CloudConfigurationManager.GetSetting(Settings.APPKEY_RESTENDPOINT_URL), Settings.REST_GEOFENCE);
						currentData = getJsonData(currentRestUrl);
						GeofenceResults geofenceData = JsonConvert.DeserializeObject<GeofenceResults>(currentData);

						foreach (GeofenceResult result in geofenceData.Geofence_result)
						{
							//Write the data
							writeGeofenceData(result);
						}
						#endregion

					}
					else if (string.Compare(userResponse, "exit", true) == 0)
					{
						break;
					}
				}
			}
		}

		private static void writeGeofenceData(GeofenceResult geofenceResultData)
		{
			string sqlCommandText = $@"
					INSERT INTO geo_fence  
						(
							floor_id,  
							geofence_id,  
							geofence_name
						)  
					VALUES  
						(  
							'{geofenceResultData.msg.floor_id}',
							'{geofenceResultData.msg.geofence_id}',
							'{geofenceResultData.msg.geofence_name}'
						);";
			writeSqlData(sqlCommandText);

			//Now write the points
			foreach(PointList p in geofenceResultData.msg.point_list)
			{
				 sqlCommandText = $@"
					INSERT INTO point_list  
						(
							geofence_id,  
							x,  
							y
						)  
					VALUES  
						(  
							'{geofenceResultData.msg.geofence_id}',
							'{p.x}',
							'{p.y}'
						);";
				writeSqlData(sqlCommandText);
			}

			Console.WriteLine("Added geofenceResultData");
		}

		private static void writeFloorData(FloorResult floorResultData)
		{
			string sqlCommandText = $@"  
					INSERT INTO floor  
						(
							floor_id,  
							floor_name,  
							floor_latitude,  
							floor_longitude,
							floor_img_path,
							floor_img_length,
							building_id,
							floor_level,
							units
					)  
					VALUES  
						(  
							'{floorResultData.msg.floor_id}',
							'{floorResultData.msg.floor_name}',
							'{floorResultData.msg.floor_latitude}',
							'{floorResultData.msg.floor_longitude}',
							'{floorResultData.msg.floor_img_path}',
							'{floorResultData.msg.floor_img_length}',
							'{floorResultData.msg.building_id}',
							'{floorResultData.msg.floor_level}',
							'{floorResultData.msg.units}'
						);";
			writeSqlData(sqlCommandText);
			Console.WriteLine("Added floorResultData");
		}

		private static void writeCampusData(CampusResult campusResultData)
		{
			string sqlCommandText = $@"  
					INSERT INTO campus  
						(
							campus_id,  
							campus_name
						)  
					VALUES  
						(  
							'{campusResultData.msg.campus_id}',
							'{campusResultData.msg.campus_name}'
						);";
			writeSqlData(sqlCommandText);
			Console.WriteLine("Added campusResultData");
		}

		private static void writeBuldingData(BuildingResult buildingResultData)
		{
			string sqlCommandText = $@"  
					INSERT INTO building  
						(
							building_id,  
							building_name,  
							campus_id
						)  
					VALUES  
						(  
							'{buildingResultData.msg.building_id}',
							'{buildingResultData.msg.building_name}',
							'{buildingResultData.msg.campus_id}'
						);";
			writeSqlData(sqlCommandText);
			Console.WriteLine("Added buildingResultData");
		}

		private static void writePresenceData(PresenceResult presenceResultData)
		{
			string sqlCommandText = $@"  
					INSERT INTO presence  
						(
							sta_eth_mac_addr,  
							associated,  
							hashed_sta_eth_mac,  
							ap_name,
							radio_mac_addr,
							ts
						)  
					VALUES  
						(  
							'{presenceResultData.msg.sta_eth_mac.addr}',
							'{presenceResultData.msg.associated}',
							'{presenceResultData.msg.hashed_sta_eth_mac}',
							'{presenceResultData.msg.ap_name}',
							'{presenceResultData.msg.radio_mac.addr}',
							'{presenceResultData.ts}'
						);";
			writeSqlData(sqlCommandText);
			Console.WriteLine("Added presenceResultData");
		}

		static void writeAPData(Access_point_result accessPointData)
		{
			string sqlCommandText = $@"  
					INSERT INTO access_point  
						(
							ap_eth_mac_addr,  
							ap_name,  
							ap_group,  
							ap_model,
							depl_mode,
							ap_ip_address
						)  
					VALUES  
						(  
							'{accessPointData.msg.ap_eth_mac.addr}',
							'{accessPointData.msg.ap_name}',
							'{accessPointData.msg.ap_group}',
							'{accessPointData.msg.ap_model}',
							'{accessPointData.msg.depl_mode}',
							'{accessPointData.msg.ap_ip_address.addr}'
						);";
			writeSqlData(sqlCommandText);
			Console.WriteLine("Added accessPointData");
		}

		static string getJsonData(string Url)
		{
			string result = string.Empty;
			HttpWebRequest http = (HttpWebRequest)WebRequest.Create(Url);
			if (true)
			{
				http.ServerCertificateValidationCallback +=
				delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
										System.Security.Cryptography.X509Certificates.X509Chain chain,
										System.Net.Security.SslPolicyErrors sslPolicyErrors)
				{
					return true; // **** Always accept
				};
			}
			var byteArray = Encoding.ASCII.GetBytes($"{CloudConfigurationManager.GetSetting(Settings.APPKEY_RESTENDPOINT_UID)}:{CloudConfigurationManager.GetSetting(Settings.APPKEY_RESTENDPOINT_PWD)}");
			string basicAuthValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray)).ToString();
			http.Headers.Add(HttpRequestHeader.Authorization, basicAuthValue);

			using (WebResponse response = http.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				{
					StreamReader sr = new StreamReader(stream);
					result = sr.ReadToEnd();
					Console.WriteLine(result + "...");
				}
			}

			return result;
		}

		static void writeSqlData(string SqlCommandText)
		{
			string sqlConnString = ConfigurationManager.ConnectionStrings[Settings.APPKEY_SQL_CONN_STRING].ConnectionString;
			using (SqlConnection sqlConn = new SqlConnection(sqlConnString))
			{
				sqlConn.Open();
				Console.WriteLine("Connected successfully.");

				using (var command = new SqlCommand())
				{
					command.Connection = sqlConn;
					command.CommandType = CommandType.Text;
					command.CommandText = SqlCommandText;
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
