using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace commandNotifier
{
    class Program
    {
        private static async void SendNotificationAsync(string tag)
        {
            NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://kfarubaiotdemo.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=swt17mfcWP9zh8pOwJ5zAzzJTwnNfDRpKG5aDv07SjM=", "kfnotificationhub");
            var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">Hello from a .NET App!</text></binding></visual></toast>";
            //await hub.SendWindowsNativeNotificationAsync(toast,"myTag");
            await hub.SendWindowsNativeNotificationAsync(toast,tag);
        }
        static void Main(string[] args)
        {
            SendNotificationAsync(args[0]);
            Console.ReadLine();
        }
    }
}
