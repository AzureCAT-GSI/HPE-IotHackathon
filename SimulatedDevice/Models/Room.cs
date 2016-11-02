using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimulatedDevice.Models
{
	public class Room
	{
		List<Device> devicesInRoom = null;
		[JsonProperty("geofence_name")]
		public string Name { get; set; }
		[JsonProperty("geofence_id")]
		public string Id { get; set; }
		List<DwellTimeIndex> deviceDwellTimeIndex = null;

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
				DwellTimeIndex ds = new DwellTimeIndex();
				ds.DeviceToIndex = deviceToAdd;
				deviceDwellTimeIndex.Add(ds);
				RaiseMoveEvent(deviceToAdd, "ZONE_IN", this);
			}
		}

		public void MoveOut(Device deviceToRemove)
		{
			if (devicesInRoom.Contains(deviceToRemove))
			{
				devicesInRoom.Remove(deviceToRemove);
				//DwellTimeIndex ds = deviceDwellTimeIndex.Find(;
				//ds.DeviceId = deviceToAdd.addr;
				//int dwellTime = deviceDwellTimeIndex[deviceToRemove.Id].CalculateDwellTime();
				//deviceDwellTimeIndex.Remove(deviceToRemove);

				RaiseMoveEvent(deviceToRemove, "ZONE_OUT", this);
			}
		}


		public static EventArgs RaiseMoveEvent(Device deviceToAdd, string ZoneEventType, Room room)
		{
			DeviceMoveEventArgs args = new DeviceMoveEventArgs();
			args.RoomName = room.Name;
			args.geofence_id = room.Id;
			args.DeviceId = deviceToAdd.MacAddress;
			args.Event = ZoneEventType;
			return args;
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
