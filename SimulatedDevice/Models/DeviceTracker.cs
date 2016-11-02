using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Exceptions;

namespace SimulatedDevice.Models
{
	public class DeviceTracker
	{
		public List<Device> Devices = new List<Device>();
		public List<Room> Rooms = new List<Room>();
		static DeviceClient deviceClient;
		static string iotHubUri = "kfIoThub.azure-devices.net";
		static string deviceKey = "8oiJKeEljWDdTPrSkDTaEmqKvGGlS2BPtjFCrgt/F6U=";

		static Random randDevices = new Random();
		static Random randRooms = new Random();

		//static RegistryManager registryManager;
		static string connectionString = "HostName=kfIoThub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=3rQBsxaPSLG05944GfZpMkDchq9IjCyP76Kayub7mK4=";
		//static void Main(string[] args)
		//{
		//	//registryManager = RegistryManager.CreateFromConnectionString(connectionString);
		//	AddDeviceAsync().Wait();
		//	Console.ReadLine();
		//}

		//Create sample devices

		public DeviceTracker()
		{
			
		}

		public DeviceTracker(int NumDevices, int NumRooms)
		{
			Devices = DataGenerator.GenerateDevices(NumDevices);
			registerDevicesWithHub();
			Rooms = DataGenerator.GenerateRooms(NumRooms);
		}

		private void registerDevicesWithHub()
		{
			if (Devices != null)
			{
				foreach (Device d in Devices)
				{
					//deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(d.MacAddress, deviceKey));
					//string deviceId = "Device_1";
					//Device device;
					//try
					//{
					//	device = await registryManager.AddDeviceAsync(new Device(deviceId));
					//}
					//catch (DeviceAlreadyExistsException)
					//{
					//	device = await registryManager.GetDeviceAsync(deviceId);
					//}
					//Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
				}
			}
		}

		public void ShuffleDevices()
		{

			Device currentDevice = null;
			Device lastDevice = null;
			Room currentRoom = null;
			Room lastRoom = null;
			//Move them around
			while (true)
			{
				//Pick a device
				currentDevice = Devices[randDevices.Next(0, Devices.Count)];
				Console.WriteLine($"Picking device {currentDevice.MacAddress}");
				if (lastDevice != null && lastDevice != currentDevice)
				{
					lastDevice = currentDevice;
				}

				//Pick a room
				currentRoom = Rooms[randRooms.Next(0, Rooms.Count)];
				Console.WriteLine($"Picking room {currentRoom.Name}");

				if (lastRoom != null && lastRoom != currentRoom)
				{
					lastRoom = currentRoom;
				}

				//Move in
				currentRoom.MoveIn(currentDevice);
				Console.WriteLine($"Moving device into room");

				//Sleep
				int WaitTime = new Random(10).Next(0, 10);
				Console.WriteLine($"Waiting for {WaitTime.ToString()} seconds");
				Task.Delay(WaitTime);
				//Move the last device out of the room
				lastRoom.MoveOut(lastDevice);
				Console.WriteLine($"Moving device out of room");

			}
		}

		void OnDeviceMove(DeviceMoveEventArgs args)
		{
			//Create the message and send it

			//await deviceClient.SendEventAsync(message);

		}

	}
}
