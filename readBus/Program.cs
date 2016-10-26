using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.NotificationHubs;

namespace readBus
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Endpoint=sb://kfservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=k9JPYOQzRmPb60c4xGrN94Uy6nGOa3uqrBtRh5z3PgU=";
            var queueName = "notifications";

            QueueClient client = QueueClient.CreateFromConnectionString(connectionString, queueName);
            client.OnMessage(message =>
            {
                //try
                //{
                    
                    JObject token = JObject.Parse(message.GetBody<String>());
                string key = token["hashed_sta_mac"].Value<String>();
                string room = token["geofence_name"].Value<String>();
                NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://kfarubaiotdemo.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=swt17mfcWP9zh8pOwJ5zAzzJTwnNfDRpKG5aDv07SjM=", "kfnotificationhub");
                var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">Hello from "+room+"!</text></binding></visual></toast>";
                //await hub.SendWindowsNativeNotificationAsync(toast,"myTag");
                hub.SendWindowsNativeNotificationAsync(toast,key);

                Console.WriteLine(String.Format("Message id: {0}", message.MessageId));
                //}
                //catch(Exception Ex)
                //{

                //}
            });

            Console.ReadLine();
            //string eventHubConnectionString = "Endpoint=sb://kfservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=k9JPYOQzRmPb60c4xGrN94Uy6nGOa3uqrBtRh5z3PgU=";
            //string eventHubName = "kfeventns.servicebus.windows.net";
            //string storageAccountName = "kfiotdemo.blob.core.windows.net";
            //string storageAccountKey = "L9agDaYkt2kg5IdVAvgk24n037ZkxmRn6FB2MkNesjXlq7gS0c8fUgUQFWIVuft4s4mkPWDWS4CRKEmI4MSHCA==";
            //string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);

            //string eventProcessorHostName = Guid.NewGuid().ToString();
            //EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
            //Console.WriteLine("Registering EventProcessor...");
            //var options = new EventProcessorOptions();
            //options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
            //eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(options).Wait();

            //Console.WriteLine("Receiving. Press enter key to stop worker.");
            //Console.ReadLine();
            //eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
