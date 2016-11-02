using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedDevice.Models
{
    public class GeofenceEvent
    {
        public string geofence_name { get; set; }
        public string geofence_event { get; set; }
        public string geofence_id { get; set; }
        public string hashed_sta_mac { get; set; }
    }
}
