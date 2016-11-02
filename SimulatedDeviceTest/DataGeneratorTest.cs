using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimulatedDevice;
using SimulatedDevice.Models;

namespace SimulatedDeviceTest
{
	[TestClass]
	public class DataGeneratorTest
	{
		[TestMethod]
		public void ShouldGenerateMacAddress()
		{
			//
			string macAddr = DataGenerator.GenerateMACAddress();
			Debug.WriteLine(macAddr);
			Assert.IsNotNull(macAddr);
		}

		[TestMethod]
		public void ShouldGenerateDevices()
		{
			int maxdevices = 10;
			List<Device> devices = DataGenerator.GenerateDevices(maxdevices);
			foreach(Device d in devices)
			{
				Debug.WriteLine($"Device has address {d.MacAddress}");
			}
		}

		[TestMethod]
		public void ShouldGenerateRoomNames()
		{
			int maxRooms = DataGenerator.RoomNames.Count;
			List<Room> rooms = DataGenerator.GenerateRooms(maxRooms);
			foreach(Room r in rooms)
			{ 
				Debug.WriteLine($"Room id [{r.Id}] name is [{r.Name}]");
			}
			Assert.AreEqual(maxRooms, DataGenerator.RoomNames.Count);
		}

		[TestMethod]
		public void ShouldHashMacaddress()
		{
			string mac = DataGenerator.GenerateMACAddress();
			string hash = DataGenerator.HashMacaddress(mac);
			Debug.WriteLine($"MAC address is {mac}");
			Debug.WriteLine($"Hash is {hash}");
			Assert.AreNotEqual(mac, hash);
		}

	}
}
