using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader.Models.campus
{
	public class Msg
	{
		public string campus_id { get; set; }
		public string campus_name { get; set; }
	}

	public class CampusResult
	{
		public Msg msg { get; set; }
		public int ts { get; set; }
	}

	public class CampusResults
	{
		public List<CampusResult> Campus_result { get; set; }
	}
}
