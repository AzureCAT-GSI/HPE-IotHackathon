using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.Storage;
using System.Net.Http.Headers;
using Windows.Networking.PushNotifications;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Messaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KFAppNotificationDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void InitNotificationsAsync(string tag)
        {
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var hub = new NotificationHub("kfnotificationhub", "Endpoint=sb://kfarubaiotdemo.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=VL9FZEckq+lIt3ejtgj7lI9h0b0hOCB9kc5nf4I5fKE=");
            string[] userTag = new string[1];
            userTag[0] = tag;

            var result = await hub.RegisterNativeAsync(channel.Uri, userTag);

            // Displays the registration ID so you know it was successful
            if (result.RegistrationId != null)
            {
                var dialog = new MessageDialog("Registration successful: " + result.RegistrationId);
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
            }

        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            InitNotificationsAsync(tag.Text);
        }
    }
}
