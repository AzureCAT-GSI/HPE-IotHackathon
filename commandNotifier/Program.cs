using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace commandNotifier
{
    class Program
    {

        private static async void SendTemplateNotificationAsync()
        {
            // Define the notification hub.
            NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://kfarubaiotdemo.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=swt17mfcWP9zh8pOwJ5zAzzJTwnNfDRpKG5aDv07SjM=", "kfnotificationhub");

            // Sending the notification as a template notification. All template registrations that contain 
            // "messageParam" or "News_<local selected>" and the proper tags will receive the notifications. 
            // This includes APNS, GCM, WNS, and MPNS template registrations.
            Dictionary<string, string> templateParams = new Dictionary<string, string>();

            // Create an array of breaking news categories.
            var categories = new string[] { "World", "Politics", "Business", "Technology", "Science", "Sports" };
            var locales = new string[] { "English", "French", "Mandarin", "kimforssmac" };

            foreach (var category in categories)
            {
                templateParams["messageParam"] = "Breaking " + category + " News!";

                // Sending localized News for each tag too...
                foreach (var locale in locales)
                {
                    string key = "News_" + locale;

                    // Your real localized news content would go here.
                    templateParams[key] = "Breaking " + category + " News in " + locale + "!";
                }

                await hub.SendTemplateNotificationAsync(templateParams, category);
            }
        }

        private static async void aSendNotificationAsync(string tag)
        {
            string conf = ConfigurationManager.AppSettings["Microsoft.Azure.NotificationHubs.ConnectionString"];
            NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://hmihub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=YiwRli3oMZ8kPnd/U6PranONOG/uXaQSF0aqnRZncPM=", "emeahminofificationhub");
            var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">Hello from a .NET App!</text></binding></visual></toast>";
            //await hub.SendWindowsNativeNotificationAsync(toast,"myTag");
            await hub.SendWindowsNativeNotificationAsync(toast,tag);
        }

        private static async void gSendNotificationAsync(string tag)
        {
            string conf = ConfigurationManager.AppSettings["Microsoft.Azure.NotificationHubs.ConnectionString"];
            NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://hmihub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=3kr9bLYgGwFWAUPn6boBYw8IRiYoAkPoPbPRA849o7A=", "emeahminofificationhub");
            var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">Hello from a .NET App!</text></binding></visual></toast>";
            //await hub.SendWindowsNativeNotificationAsync(toast,"myTag");
            GcmNotification gcm = new GcmNotification("{\"data\":{\"message\":\"Hej Robin\"}}");
            gcm.Headers.Add("tag", tag);
            
            await hub.SendNotificationAsync (gcm,tag);
            //await hub.SendGcmNativeNotificationAsync(", tag);
        }


        static void Main(string[] args)
        {

            SendTemplateNotificationAsync();
            //   gSendNotificationAsync("hej");
            try
            {
                aSendNotificationAsync("Node1");
            }
            catch(System.UnauthorizedAccessException uEx)
            {
                Console.WriteLine(uEx.Message);
                if(uEx.InnerException != null)
                {
                    Console.WriteLine(uEx.InnerException.Message);
                }
                Console.ReadLine();
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                if(Ex.InnerException != null)
                {
                    Console.WriteLine(Ex.InnerException.Message);
                }
                Console.ReadLine();
            }
            
        }
    }
}
