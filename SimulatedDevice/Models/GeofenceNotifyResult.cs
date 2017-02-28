using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimulatedDevice.Models
{
	//{ "Geofence_result": [ {  "geofence_notify": {  "hashed_sta_mac": "b158a026e9416aa2a307541bfe205ae44b4ca660",  "geofence_id": "6e8fd73a21c53c45808629251b5b0d76",  "dwell_time": 0,  "geofence_event": "ZONE_IN",  "geofence_name": "Bath room",  "sta_mac": {   "addr": "502e5c321bd7"  }  },  "seq": "44773",  "topic_seq": "812",  "timestamp": 1476714980,  "source_id": "000c29309b0b" }, {  "geofence_notify": {  "hashed_sta_mac": "2cd7f7ec44075b45bd266d4cf9f9cf36e23e5baa",  "geofence_id": "753f5a57eb8a30fb882da5340daddaf3",  "dwell_time": 0,  "geofence_event": "ZONE_IN",  "geofence_name": "Dinning Room",  "sta_mac": {   "addr": "0024d45b385c"  }  },  "seq": "44782",  "topic_seq": "813",  "timestamp": 1476715007,  "source_id": "000c29309b0b" }, {  "geofence_notify": {  "hashed_sta_mac": "f74e61f89b78eb09679828de59bc8c559cd98687",  "geofence_id": "753f5a57eb8a30fb882da5340daddaf3",  "dwell_time": 0,  "geofence_event": "ZONE_IN",  "geofence_name": "Dining Room",  "sta_mac": {   "addr": "0024d45b385e"  }  },  "seq": "44794",  "topic_seq": "814",  "timestamp": 1476715060,  "source_id": "000c29309b0b" }, {  "geofence_notify": {  "hashed_sta_mac": "2cd7f7ec44075b45bd266d4cf9f9cf36e23e5baa",  "geofence_id": "753f5a57eb8a30fb882da5340daddaf3",  "dwell_time": 1033,  "geofence_event": "ZONE_OUT",  "geofence_name": "Dinning Room",  "sta_mac": {   "addr": "0024d45b385c"  }  },  "seq": "45004",  "topic_seq": "815",  "timestamp": 1476716040,  "source_id": "000c29309b0b" }, {  "geofence_notify": {  "hashed_sta_mac": "f74e61f89b78eb09679828de59bc8c559cd98687",  "geofence_id": "753f5a57eb8a30fb882da5340daddaf3",  "dwell_time": 991,  "geofence_event": "ZONE_OUT",  "geofence_name": "Dinning Room",  "sta_mac": {   "addr": "0024d45b385e"  }  },  "seq": "45009",  "topic_seq": "816",  "timestamp": 1476716051,  "source_id": "000c29309b0b" } ]} 

	[JsonObject("Geofence_result")]
	public class GeofenceNotify
	{
		[JsonProperty("sta_mac")]
		public string Id { get; set; }
		[JsonProperty("hashed_sta_mac")]
		public string HashedMac { get; set; }
		[JsonProperty("dwell_time ")]
		public int DwellTime { get; set; }
		[JsonProperty("geofence_event")]
		public string Event { get; set; }
		[JsonProperty("geofence_name ")]
		public string Name{ get; set; }
		[JsonProperty("sta_mac")]
		public StaticMac StaticMacAddr { get; set; }
	}

	public class GeofenceResult
	{
		[JsonProperty("geofence_notify")]
		public GeofenceNotify geofence_notify { get; set; }
		[JsonProperty("seq")]
		public string seq { get; set; }
		[JsonProperty("topic_seq")]
		public string topic_seq { get; set; }
		[JsonProperty("timestamp")]
		public int timestamp { get; set; }
		[JsonProperty("source_id")]
		public string source_id { get; set; }
	}

	public class StaticMac
	{
		[JsonProperty("sta_mac")]
		public string Address { get; set; }
	}
}
