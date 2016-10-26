using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SimulatedDevice
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "kfIoThub.azure-devices.net";
        static string deviceKey = "8oiJKeEljWDdTPrSkDTaEmqKvGGlS2BPtjFCrgt/F6U=";
        static void Main(string[] args)
        {
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("Device_1", deviceKey));

            SendDeviceToCloudMessagesAsync();

            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {

            string data = System.IO.File.ReadAllText(@"D:\HPE\0MQ_geofence_notify.json");
            JToken twitterObject = JToken.Parse(data);
            var trendsArray = twitterObject.Children<JProperty>().FirstOrDefault(x => x.Name == "Geofence_result").Value;
            while (true)
            {
                foreach (var item in trendsArray.Children())
                {
                    JToken jt = JToken.Parse(item.ToString());
                    var notificationArray = jt.Children<JProperty>().FirstOrDefault(x => x.Name == "geofence_notify").Value;
                    var message = new Message(Encoding.ASCII.GetBytes(notificationArray.ToString()));
                    Console.WriteLine(notificationArray.ToString());
                    Task.Delay(60000).Wait();
                    await deviceClient.SendEventAsync(message);
                    //    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);


                }

                Task.Delay(600000).Wait();
            }

            //for (int i = 0; i < G3.Count; i++)
            //{
            //    JToken j = G3[i];
            //    var message = new Message(Encoding.ASCII.GetBytes(j));

            //    await deviceClient.SendEventAsync(message);
            //    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

            //}

            //Random rand = new Random();

            //while (true)
            //{
            //    string room = "RRR-0001";
            //    if (rand.NextDouble() >= 0.5)
            //    {
            //        room = "RRR-0002";
            //    }


            //    var telemetryDataPoint = new
            //    {
            //        time = DateTime.Now,
            //        deviceId = "Device_1",
            //        roomId = room
            //    };

            //    var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
            //    var message = new Message(Encoding.ASCII.GetBytes(messageString));

            //    await deviceClient.SendEventAsync(message);
            //    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

            //    Task.Delay(60000).Wait();
            //}
        }
    }
}
