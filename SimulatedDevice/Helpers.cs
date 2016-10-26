using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedDevice
{
    public class StaEthMac
    {
        public string addr { get; set; }
    }

    public class Bssid
    {
        public string addr { get; set; }
    }

    public class StaIpAddress
    {
        public string addr { get; set; }
        public string af { get; set; }
    }

    public class StationMsg
    {
        public string hashed_sta_eth_mac { get; set; }
        public StaEthMac sta_eth_mac { get; set; }
        public Bssid bssid { get; set; }
        public string ap_name { get; set; }
        public string username { get; set; }
        public string hashed_sta_ip_address { get; set; }
        public string device_type { get; set; }
        public string role { get; set; }
        public StaIpAddress sta_ip_address { get; set; }
    }

    public class StationResult
    {
        [System.Xml.Serialization.XmlElementAttribute("msg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public StationMsg msg { get; set; }
        public int ts { get; set; }
    }

    public class StationRootObject
    {
        public List<StationResult> Station_result { get; set; }
    }
}

public class CampusMsg
{
    public string campus_name { get; set; }
    public string campus_id { get; set; }
}

public class CampusResult
{
    public int ts { get; set; }
    [System.Xml.Serialization.XmlElementAttribute("msg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public CampusMsg campus { get; set; }
}

public class CampusRootObject
{
    public List<CampusResult> Campus_result { get; set; }
}

public class BuildingMsg
{
    public string building_id { get; set; }
    public string campus_id { get; set; }
    public string building_name { get; set; }
}

public class BuildingResult
{
    [System.Xml.Serialization.XmlElementAttribute("msg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BuildingMsg msg { get; set; }
    public int ts { get; set; }
}

public class BuildingRootObject
{
    public List<BuildingResult> Building_result { get; set; }
}

public class FloorMsg
{
    public string floor_name { get; set; }
    public string floor_img_path { get; set; }
    public double floor_img_width { get; set; }
    public double floor_latitude { get; set; }
    public double floor_longitude { get; set; }
    public string floor_id { get; set; }
    public string building_id { get; set; }
    public double floor_img_length { get; set; }
    public double floor_level { get; set; }
    public string units { get; set; }
}

public class FloorResult
{
    [System.Xml.Serialization.XmlElementAttribute("msg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public FloorMsg msg { get; set; }
    public int ts { get; set; }
}

public class FloorRootObject
{
    public List<FloorResult> Floor_result { get; set; }
}