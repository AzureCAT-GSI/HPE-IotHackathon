using System;

namespace SimulatedDevice.Models
{
	public class DeviceMoveEventArgs : EventArgs
	{
		public string DeviceId { get; internal set; }
		public string Event { get; internal set; }
		public string RoomName { get; set; }

		public string hashed_sta_mac { get; set; }
		public string geofence_id { get; set; }
		public int dwell_time { get; set; }
		public string geofence_event { get; set; }
		public string geofence_name { get; set; }
		//public StaMac sta_mac { get; set; }
	}
}