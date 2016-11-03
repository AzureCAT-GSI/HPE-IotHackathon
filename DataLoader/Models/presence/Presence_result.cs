using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader.Models.presence
{
	public class StaEthMac
	{
		public string addr { get; set; }
	}

	public class RadioMac
	{
		public string addr { get; set; }
	}

	public class Msg
	{
		public StaEthMac sta_eth_mac { get; set; }
		public bool associated { get; set; }
		public string hashed_sta_eth_mac { get; set; }
		public string ap_name { get; set; }
		public RadioMac radio_mac { get; set; }
	}

	public class PresenceResult
	{
		public Msg msg { get; set; }
		public int ts { get; set; }
	}

	public class PresenceResults
	{
		public List<PresenceResult> Presence_result { get; set; }
	}
}
