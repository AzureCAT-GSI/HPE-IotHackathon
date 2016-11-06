using Microsoft.WindowsAzure.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GSIAppNotificationDemo
{
    public sealed partial class ContentDialog1 : ContentDialog
    {
        public ContentDialog1()
        {
            this.InitializeComponent();
            try
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                txtNotification.Text = (string)localSettings.Values["NotificationHub"];
                txtConnection.Text = (string)localSettings.Values["ConnectionString"];
                txtTag.Text = (string)localSettings.Values["Tag"];
            }
            catch(Exception Ex)
            { }
        }
        private async void InitNotificationsAsync(string tag)
        {
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var hub = new NotificationHub(txtNotification.Text, txtConnection.Text);
            string[] userTag = new string[1];
            userTag[0] = tag;

            var result = await hub.RegisterNativeAsync(channel.Uri, userTag);

            // Displays the registration ID so you know it was successful
            if (result.RegistrationId != null)
            {
               // txtResult.Text = "Registration successful: " + result.RegistrationId;
            }


        }


        private async void Unregister()
        {
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var hub = new NotificationHub(txtNotification.Text, txtConnection.Text);
            await hub.UnregisterAllAsync(channel.Uri);
            

        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            InitNotificationsAsync(txtTag.Text);

            Windows.Storage.ApplicationDataContainer localSettings =    Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["NotificationHub"] = txtNotification.Text;
            localSettings.Values["ConnectionString"] = txtConnection.Text;
            localSettings.Values["Tag"] = txtTag.Text;


        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Unregister();
        }
    }
}
