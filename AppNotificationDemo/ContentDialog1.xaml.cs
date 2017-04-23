using Microsoft.WindowsAzure.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.UI.Popups;
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
            string registrationID = String.Empty;
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
            try
            {
                PushNotificationChannel channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

                NotificationHub hub = new NotificationHub(txtNotification.Text, txtConnection.Text);
                
                //userTag[0] = tag;
                if(!String.IsNullOrWhiteSpace(tag))
                {
                    string[] userTag = tag.Split(";".ToCharArray());
                    var result = await hub.RegisterNativeAsync(channel.Uri, userTag); //

                    // Displays the registration ID so you know it was successful
                    if (result.RegistrationId != null)
                    {
                        //registrationID = result.RegistrationId;
                        // txtResult.Text = ;
                        var dialog = new MessageDialog("Registration successful: " + result.RegistrationId + ". Channel Uri = " + channel.Uri);
                        await dialog.ShowAsync();
                    }
                    else
                    {
                        var dialog = new MessageDialog("Registration failed");
                        await dialog.ShowAsync();
                    }
                }
                else
                {
                    Registration result = await hub.RegisterNativeAsync(channel.Uri, null); //

                    // Displays the registration ID so you know it was successful
                    if (result.RegistrationId != null)
                    {
                        // txtResult.Text = ;
                        var dialog = new MessageDialog("Registration successful: " + result.RegistrationId);
                        await dialog.ShowAsync();
                    }
                }
                
            }
            catch (Exception Ex)
            {
                if (null != Ex.InnerException)
                {
                    var dialog = new MessageDialog("Error when registering: " + Ex.InnerException.Message);
                    await dialog.ShowAsync();
                }

            }


        }


        private async void Unregister(string tag)
        {
            try
            {
                PushNotificationChannel channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

                NotificationHub hub = new NotificationHub(txtNotification.Text, txtConnection.Text);

                if (!String.IsNullOrWhiteSpace(tag))
                {


                    string[] userTag = tag.Split(";".ToCharArray());
                    Registration reg = new Registration(channel.Uri, userTag);
                    await hub.UnregisterAsync(reg);
                }
                else
                {
                    Registration reg = new Registration(channel.Uri);
                    await hub.UnregisterAsync(reg);
                }
            }
            catch(Exception Ex)
            { }

        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {


        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
        private void btn_UnRegister_Click(object sender, RoutedEventArgs e)
        {

            Unregister(txtTag.Text);
        }


        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            InitNotificationsAsync(txtTag.Text);

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["NotificationHub"] = txtNotification.Text;
            localSettings.Values["ConnectionString"] = txtConnection.Text;
            localSettings.Values["Tag"] = txtTag.Text;

        }
    }
}
