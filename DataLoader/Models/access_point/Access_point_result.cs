using System.Collections.Generic;

namespace DataLoader.access_point
{
	public class ApEthMac
	{
		public string addr { get; set; }
	}

	public class ApIpAddress
	{
		public string af { get; set; }
		public string addr { get; set; }
	}

	public class ManagedBy
	{
		public string af { get; set; }
		public string addr { get; set; }
	}

	public class Msg
	{
		public ApEthMac ap_eth_mac { get; set; }
		public string ap_name { get; set; }
		public string ap_group { get; set; }
		public string ap_model { get; set; }
		public string depl_mode { get; set; }
		public ApIpAddress ap_ip_address { get; set; }
		public ManagedBy managed_by { get; set; }
		public string managed_by_key { get; set; }
	}

	public class Access_point_result
	{
		public Msg msg { get; set; }
		public int ts { get; set; }
	}

	public class Access_point_results
	{
		public List<Access_point_result> Access_point_result { get; set; }
	}
}