using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimulatedDevice.Models
{
    public delegate void DeviceMovedEventHandler(object sender, DeviceMoveEventArgs e);

    public class Room
	{
		List<Device> devicesInRoom = null;

		[JsonProperty("geofence_name")]
		public string Name { get; set; }

		[JsonProperty("geofence_id")]
		public string Id { get; set; }

        List<DwellTimeIndex> deviceDwellTimeIndex = null;

        public event DeviceMovedEventHandler OnDeviceMoved;

        public Room()
		{
			Id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
			devicesInRoom = new List<Device>();
			deviceDwellTimeIndex = new List<DwellTimeIndex>();
		}

		public List<Device> DevicesInRoom()
		{
			return devicesInRoom;
		}

		public void MoveIn(Device deviceToAdd)
		{
			if (!devicesInRoom.Contains(deviceToAdd))
			{
				devicesInRoom.Add(deviceToAdd);
                deviceToAdd.CurrentRoom = this;
				//DwellTimeIndex ds = new DwellTimeIndex();
				//ds.DeviceToIndex = deviceToAdd;
				//deviceDwellTimeIndex.Add(ds);
				RaiseMoveEvent(deviceToAdd, "ZONE_IN");
			}
		}

		public void MoveOut(Device deviceToRemove)
		{
			if (devicesInRoom.Contains(deviceToRemove))
			{
				devicesInRoom.Remove(deviceToRemove);
                deviceToRemove.CurrentRoom = null;
				//DwellTimeIndex ds = deviceDwellTimeIndex.Find(;
				//ds.DeviceId = deviceToAdd.addr;
				//int dwellTime = deviceDwellTimeIndex[deviceToRemove.Id].CalculateDwellTime();
				//deviceDwellTimeIndex.Remove(deviceToRemove);

				RaiseMoveEvent(deviceToRemove, "ZONE_OUT");
			}
		}


		public void RaiseMoveEvent(Device deviceToAdd, string zoneEventType)
		{
			DeviceMoveEventArgs args = new DeviceMoveEventArgs();
			args.RoomName = this.Name;
			args.geofence_id = this.Id;
			args.DeviceId = deviceToAdd.MacAddress;
			args.Event = zoneEventType;
            args.dwell_time = deviceToAdd.DwellTime;
            OnDeviceMoved?.Invoke(this, args);
        }

        internal class DwellTimeIndex
		{
			public DwellTimeIndex()
			{
				AddedTimeStamp = DateTime.UtcNow;
			}

			public Device DeviceToIndex { get; set; }
			public DateTime AddedTimeStamp { get; internal set; }

			public int CalculateDwellTime()
			{
				DateTime utcNow = DateTime.UtcNow;
				TimeSpan ts = new TimeSpan(AddedTimeStamp.Ticks);
				int dwellTime = ts.CompareTo(utcNow);
				return dwellTime;
			}
		}
	}
}
