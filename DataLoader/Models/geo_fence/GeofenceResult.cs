using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader.Models.geo_fence
{
	public class PointList
	{
		public double x { get; set; }
		public double y { get; set; }
	}

	public class Msg
	{
		public string floor_id { get; set; }
		public string geofence_id { get; set; }
		public string geofence_name { get; set; }
		public List<PointList> point_list { get; set; }
	}

	public class GeofenceResult
	{
		public Msg msg { get; set; }
	}

	public class GeofenceResults
	{
		public List<GeofenceResult> Geofence_result { get; set; }
	}
}
