using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Exceptions;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SimulatedDevice.Models
{
	public class DeviceTracker
	{
		public List<Device> Devices = new List<Device>();
		public List<Room> Rooms = new List<Room>();
		static DeviceClient deviceClient;

		static Random randDevices = new Random((int)DateTime.Now.TimeOfDay.TotalMilliseconds);
		static Random randRooms = new Random((int)DateTime.Now.TimeOfDay.TotalMilliseconds);

        static Random waitTimerRandomizer = new Random((int)DateTime.Now.TimeOfDay.TotalMilliseconds);

        int numberMessages;            
        //        static string connectionString = "HostName=kfIoThub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=3rQBsxaPSLG05944GfZpMkDchq9IjCyP76Kayub7mK4=";
        //static void Main(string[] args)
		//{
		//	//registryManager = RegistryManager.CreateFromConnectionString(connectionString);
		//	AddDeviceAsync().Wait();
		//	Console.ReadLine();
		//}

		//Create sample devices

        public DeviceTracker(string connectionstring, int maxDevices, int rooms)
        {
            Devices = DataGenerator.GenerateDevices(maxDevices);
            Console.WriteLine("Setting up devices:");
            foreach (Device device in Devices)
            {
                Console.WriteLine($"New Device {device.MacAddress}");
            }

            Rooms = DataGenerator.GenerateRooms(rooms);
            Console.WriteLine("Setting up rooms:");
            foreach (Room room in Rooms)
            {
                Console.WriteLine($"Selected Room {room.Name}");
                room.OnDeviceMoved += Room_OnDeviceMoved;
            }
            Console.WriteLine("Setting up connecting IoTHub:");

            deviceClient = DeviceClient.CreateFromConnectionString(connectionstring);
            deviceClient.OpenAsync().Wait();
        }

        private async void Room_OnDeviceMoved(object sender, DeviceMoveEventArgs e)
        {
            //Send the event to IoTHub
            if (null != deviceClient)
            {
                try
                {

                GeofenceEvent geoEvent = new Models.GeofenceEvent() {
                    geofence_event = e.Event,
                    geofence_id = e.geofence_id,
                    geofence_name = e.RoomName,
                    hashed_sta_mac = e.DeviceId};
                Message message = new Message(Encoding.UTF8.GetBytes(await JsonConvert.SerializeObjectAsync(geoEvent)));
                await deviceClient.SendEventAsync(message);
                Console.WriteLine($"Wow, I've already sent {++numberMessages} messages during this session ");
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Something unforeseen happend and I can't continue. See details below:");
                    Console.WriteLine(exception.Message);
                    Console.Write(exception.StackTrace);
                    Console.ReadLine();
                }
            }
        }

		public async Task ShuffleDevicesAsync(CancellationToken token)
		{

            //run in a loop
            //at randomized interval
            //pick a random device
            //move it IN a room or OUT

            Device currentDevice = null;

            List<Device> devices = new List<Device>();
			//Move them around
			while (!token.IsCancellationRequested)
			{

				//Pick a device
				currentDevice = Devices[randDevices.Next(0, Devices.Count)];
				Console.WriteLine($"Picking device {currentDevice.MacAddress}");

                if (null != currentDevice.CurrentRoom)
                {
                    Console.WriteLine($"Moving device out of room {currentDevice.CurrentRoom.Name}");
                    currentDevice.CurrentRoom.MoveOut(currentDevice);
                }
                else
                {
                    //Pick a room
                    Room room = Rooms[randRooms.Next(0, Rooms.Count)];
                    Console.WriteLine($"Moving device into room {room.Name}");
                    room.MoveIn(currentDevice);
                }

				//Sleep
				int waitTime = waitTimerRandomizer.Next(0, 10);
				Console.WriteLine($"Waiting for {waitTime.ToString()} seconds");
				await Task.Delay(waitTime * 1000);
			}
		}

		void OnDeviceMove(DeviceMoveEventArgs args)
		{
			//Create the message and send it

			//await deviceClient.SendEventAsync(message);

		}

	}
}
