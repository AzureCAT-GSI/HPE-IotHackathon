using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimulatedDevice.Models
{
	public class Device
	{
        private long dwellTime;
        private Room currentRoom;

		public Device()
		{
			MacAddress = DataGenerator.GenerateMACAddress();
		}
		
		public Device(string macAddr)
		{
			MacAddress = macAddr;
		}

		[JsonProperty("StaMac")]
		public string MacAddress { get; set; }

        public Room CurrentRoom
        {
            get { return this.currentRoom; }
            set
            {
                currentRoom = value;
                if (value == null)  //moving out
                {
                    this.dwellTime = DateTime.UtcNow.Ticks - dwellTime;
                }
                else
                {
                    this.dwellTime = DateTime.UtcNow.Ticks;
                }
            }
        }

		[JsonProperty("StaMac")]
        public string HashedMac { get { return DataGenerator.HashMacaddress(MacAddress); } }

        public uint DwellTime { get { return currentRoom == null ? (uint)TimeSpan.FromTicks(this.dwellTime).TotalSeconds : 0 ; } }

    }
}
