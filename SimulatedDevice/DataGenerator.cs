using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SimulatedDevice.Models;

namespace SimulatedDevice
{
	public class DataGenerator
	{
		static Random randNum = new Random();

		public static List<string> RoomNames = new List<string>()
		{
			"ballroom",
			"basement",
			"bathroom",
			"bedroom",
			"boardroom",
			"boiler room",
			"stateroom",
			"stockroom",
			"storeroom",
			"studio",
			"study",
			"suite",
			"sunroom",
			"laundry room",
			"library",
			"living room",
			"lobby",
			"locker room",
			"loft",
			"lounge",
			"lunchroom",
			"changing room",
			"chapel",
			"classroom",
			"clean room",
			"cloakroom",
			"cold room",
			"common room",
			"conference room",
			"conservatory",
			"control room",
			"courtroom",
			"cubby"
			};

		public DataGenerator()
		{

		}

		public static string GetRandomRoom()
		{			
			int roomIndex = randNum.Next(0, RoomNames.Count);
			return RoomNames[roomIndex];
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
            while (result.Count < MAX_ROOMS)
            {
                string roomName = string.Empty;
                while (string.IsNullOrEmpty(roomName) || result.Exists(room => room.Name == roomName))
                {
                    roomName = GetRandomRoom();
                }
                Room newRoom = new Room();
                newRoom.Id = Guid.NewGuid().ToString();
                newRoom.Name = roomName;
                result.Add(newRoom);
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