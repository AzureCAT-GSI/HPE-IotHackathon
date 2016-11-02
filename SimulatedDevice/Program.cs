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

            


            string data = "{ \"Geofence_result\": [ {  \"geofence_notify\": {  \"hashed_sta_mac\": \"b158a026e9416aa2a307541bfe205ae44b4ca660\",  \"geofence_id\": \"6e8fd73a21c53c45808629251b5b0d76\",  \"dwell_time\": 0,  \"geofence_event\": \"ZONE_IN\",  \"geofence_name\": \"Bath room\",  \"sta_mac\": {   \"addr\": \"502e5c321bd7\"  }  },  \"seq\": \"44773\",  \"topic_seq\": \"812\",  \"timestamp\": 1476714980,  \"source_id\": \"000c29309b0b\" }, {  \"geofence_notify\": {  \"hashed_sta_mac\": \"2cd7f7ec44075b45bd266d4cf9f9cf36e23e5baa\",  \"geofence_id\": \"753f5a57eb8a30fb882da5340daddaf3\",  \"dwell_time\": 0,  \"geofence_event\": \"ZONE_IN\",  \"geofence_name\": \"Dinning Room\",  \"sta_mac\": {   \"addr\": \"0024d45b385c\"  }  },  \"seq\": \"44782\",  \"topic_seq\": \"813\",  \"timestamp\": 1476715007,  \"source_id\": \"000c29309b0b\" }, {  \"geofence_notify\": {  \"hashed_sta_mac\": \"f74e61f89b78eb09679828de59bc8c559cd98687\",  \"geofence_id\": \"753f5a57eb8a30fb882da5340daddaf3\",  \"dwell_time\": 0,  \"geofence_event\": \"ZONE_IN\",  \"geofence_name\": \"Dining Room\",  \"sta_mac\": {   \"addr\": \"0024d45b385e\"  }  },  \"seq\": \"44794\",  \"topic_seq\": \"814\",  \"timestamp\": 1476715060,  \"source_id\": \"000c29309b0b\" }, {  \"geofence_notify\": {  \"hashed_sta_mac\": \"2cd7f7ec44075b45bd266d4cf9f9cf36e23e5baa\",  \"geofence_id\": \"753f5a57eb8a30fb882da5340daddaf3\",  \"dwell_time\": 1033,  \"geofence_event\": \"ZONE_OUT\",  \"geofence_name\": \"Dinning Room\",  \"sta_mac\": {   \"addr\": \"0024d45b385c\"  }  },  \"seq\": \"45004\",  \"topic_seq\": \"815\",  \"timestamp\": 1476716040,  \"source_id\": \"000c29309b0b\" }, {  \"geofence_notify\": {  \"hashed_sta_mac\": \"f74e61f89b78eb09679828de59bc8c559cd98687\",  \"geofence_id\": \"753f5a57eb8a30fb882da5340daddaf3\",  \"dwell_time\": 991,  \"geofence_event\": \"ZONE_OUT\",  \"geofence_name\": \"Dinning Room\",  \"sta_mac\": {   \"addr\": \"0024d45b385e\"  }  },  \"seq\": \"45009\",  \"topic_seq\": \"816\",  \"timestamp\": 1476716051,  \"source_id\": \"000c29309b0b\" } ]} ";
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
                    Task.Delay(10000).Wait();
                    await deviceClient.SendEventAsync(message);
                    //    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);


                }

                Task.Delay(60000).Wait();
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
