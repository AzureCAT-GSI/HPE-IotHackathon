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

        public Room CurrentRoom { get; set;  }

		[JsonProperty("StaMac")]
		public string HashedMac
		{
			get
			{
				return DataGenerator.HashMacaddress(MacAddress);
			}
		}
	}
}
