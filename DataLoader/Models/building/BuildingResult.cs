using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader.Models.building
{
	public class Msg
	{
		public string building_id { get; set; }
		public string building_name { get; set; }
		public string campus_id { get; set; }
	}

	public class BuildingResult
	{
		public Msg msg { get; set; }
		public int ts { get; set; }
	}

	public class BuildingResults
	{
		public List<BuildingResult> Building_result { get; set; }
	}
}
