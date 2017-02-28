using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SimulatedDevice.Models;
using System.Collections;
using System.Collections.Specialized;

namespace SimulatedDevice
{
    public class DataGenerator
    {
        static Random randNum = new Random();

        public static NameValueCollection Rooms = new NameValueCollection() {
            {"68BFE7491289366D859E3FBC53DA4004","Reception Desk"},
{"862BF10E4430396BA2537BEC1E409EB0","Elevator"},
{"8B06C18E1E833755BD29E7B27AB85C4F","Cafeteria"},
{"B6B8E9D723A93159A58B66D38276619F","Jungrau"},
{"C071BA7A398C35179A451A4B95EC2380","Mont Blanc 1"},
{"3328B8774A113858878E396460E9AD5E","Mont Blanc 2"},
{"76E1CD0F7E3735589B6DC4534F479466","Innovation Zone"},
{"0C281B450C8138A6AE43F26C863F5AF4","Transformation Zone"},
{"E3246EF58BF939A79A6E1364A7F30000","Transformation Journey"},
{"6ABFCCE64685387D8D593B7F29D51683","Speak Easy Lunch"},
{"4349A9C6F38035F3851145A0824552AA","Restroom"},
{"76E93EC1A93D30AEA1F7FF668A15993B","Jura 3"},
{"6BACD5E48E703C45B67380D81E0B2E2F","Industry Zone"},
{"88A6BDFA12D53DF6AA08FD9F282CA0D0","Stairs"},
{"10203B1C25063819B909FC5842E0A9ED","Mail And Shipping"},
{"23868BA522533B59B0B0C359EF12F020","Tabarly"},
{"851C81A307143CE7A6CA122CEF357A13","Men toilets L1"},
{"E2C4EB65DED132CA9A5120658979A913","Women Toilets L1"},
{"CC60237D09B438698A00D9587D724D23","Cofee Corner L1"},
{"5336CFA99DA83F71BC21A07C026E0B7F","Milla"},
{"D3811F491DEF3413B4727726DB074BCC","Witt"},
{"23868BA522533B59B0B0C359EF12F020","Tabarly"},
{"018C07E70B7C34A4B1F679FA2968E13C","Cofee Corner L1"},
{"C73AADCD41A63F6985910A5E40FA70A0","Lobby"},
{"48C94EB0D8973B69B32E440396F0CB2C","DC"}
        };

        //{
			//"ballroom",
			//"basement",
			//"bathroom",
			//"bedroom",
			//"boardroom",
			//"boiler room",
			//"stateroom",
			//"stockroom",
			//"storeroom",
			//"studio",
			//"study",
			//"suite",
			//"sunroom",
			//"laundry room",
			//"library",
			//"living room",
			//"lobby",
			//"locker room",
			//"loft",
			//"lounge",
			//"lunchroom",
			//"changing room",
			//"chapel",
			//"classroom",
			//"clean room",
			//"cloakroom",
			//"cold room",
			//"common room",
			//"conference room",
			//"conservatory",
			//"control room",
			//"courtroom",
			//"cubby",
          //  };

		public DataGenerator()
		{

		}

		public static Room GetRandomRoom()
		{			
			int roomIndex = randNum.Next(0, Rooms.Count);
			return new Room() { Id = Rooms.GetKey(roomIndex), Name = Rooms[roomIndex] };
		}

		public static List<Device> GenerateDevices(int MAX_DEVICES)
		{
			List<Device> devices = new List<Device>();
			for (int i = 0; i < MAX_DEVICES; i++)
			{
				Device newDevice = new Device(DataGenerator.GenerateMACAddress());
				devices.Add(newDevice);
			}
			return devices;
		}

		public static List<Room> GenerateRooms(int MAX_ROOMS)
		{
			List<Room> result = new List<Room>();
            Room room = null;
            while (result.Count < MAX_ROOMS)
            {
                while ((room == null) || result.Exists( item => item.Id == room.Id))
                {
                    room = GetRandomRoom();
                }
                result.Add(room);
            }
			return result;
		}

		public void MoveDevice(Device deviceToMove)
		{
			//Get the current room

			//Move it
		}

		public static string GetGeofenceId()
		{
			List<string> geoFenceIds = new List<string>();
			string randVal = string.Empty;

			return randVal;
		}

		public static string GenerateMACAddress()
		{
			var sBuilder = new StringBuilder();
			int number;
			byte b;
			for (int i = 0; i < 6; i++)
			{
				number = randNum.Next(0, 255);
				b = Convert.ToByte(number);
				if (i == 0)
				{
					b = setBit(b, 6); //--> set locally administered
					b = unsetBit(b, 7); // --> set unicast 
				}
				sBuilder.Append(number.ToString("X2"));
			}
			return sBuilder.ToString().ToUpper();
		}

		public static string HashMacaddress(string MacAddress)
		{
			string hash = string.Empty;
			using (MD5 md5Hash = MD5.Create())
			{
				hash = GetMd5Hash(md5Hash, MacAddress);

			}
			return hash;
		}

			static string GetMd5Hash(MD5 md5Hash, string input)
		{

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		// Verify a hash against a string.
		static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
		{
			// Hash the input.
			string hashOfInput = GetMd5Hash(md5Hash, input);

			// Create a StringComparer an compare the hashes.
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			if (0 == comparer.Compare(hashOfInput, hash))
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		private static byte setBit(byte b, int BitNumber)
		{
			if (BitNumber < 8 && BitNumber > -1)
			{
				return (byte)(b | (byte)(0x01 << BitNumber));
			}
			else
			{
				throw new InvalidOperationException(
				"Invalid BitNumber " + BitNumber.ToString());
			}
		}

		private static byte unsetBit(byte b, int BitNumber)
		{
			if (BitNumber < 8 && BitNumber > -1)
			{
				return (byte)(b | (byte)(0x00 << BitNumber));
			}
			else
			{
				throw new InvalidOperationException(
				"Invalid BitNumber " + BitNumber.ToString());
			}
		}
	}
}