using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader.Models.floor
{
	public class Msg
	{
		public string floor_id { get; set; }
		public string floor_name { get; set; }
		public string floor_latitude { get; set; }
		public string floor_longitude { get; set; }
		public string floor_img_path { get; set; }
		public string floor_img_width { get; set; }
		public string floor_img_length { get; set; }
		public string building_id { get; set; }
		public string floor_level { get; set; }
		public string units { get; set; }
	}

	public class FloorResult
	{
		public Msg msg { get; set; }
		public int ts { get; set; }
	}

	public class FloorResults
	{
		public List<FloorResult> Floor_result { get; set; }
	}
}
