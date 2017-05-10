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
using GSIAppNotificationDemo;
using Windows.UI.Core;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KFAppNotificationDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page 
    {
        private async void Init()
        {
            PushNotificationChannel channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            channel.PushNotificationReceived += OnPushNotification;

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string Notification= (string)localSettings.Values["NotificationHub"];
            string Connection = (string)localSettings.Values["ConnectionString"];
            string Tag = (string)localSettings.Values["Tag"];

            try
            {

                NotificationHub hub = new NotificationHub(Notification, Connection);

                //userTag[0] = tag;
                if (!String.IsNullOrWhiteSpace(Tag))
                {
                    string[] userTag = Tag.Split(";".ToCharArray());
                    var result = await hub.RegisterNativeAsync(channel.Uri, userTag); //

                    // Displays the registration ID so you know it was successful
                    if (result.RegistrationId != null)
                    {
                    }
                    else
                    {
                        var dialog = new MessageDialog("Registration failed. Please check the configuration");
                        await dialog.ShowAsync();
                    }
                }
                else
                {
                    Registration result = await hub.RegisterNativeAsync(channel.Uri, null); //

                    // Displays the registration ID so you know it was successful
                    if (result.RegistrationId != null)
                    {
                        //// txtResult.Text = ;
                        //var dialog = new MessageDialog("Registration successful: " + result.RegistrationId);
                        //await dialog.ShowAsync();
                    }
                    else
                    {
                        var dialog = new MessageDialog("Registration failed. Please check the configuration");
                        await dialog.ShowAsync();
                    }
                }
            }
            catch(Exception Ex)
            {
                ContentDialog1 dialog = new ContentDialog1()
                {
                    Title = "Configure connection",
                };

                await dialog.ShowAsync();

            }

        }
        public MainPage()
        {
            this.InitializeComponent();
            Init();

        }

        private async void ShowConf()
        {
            ContentDialog1 dialog = new ContentDialog1()
            {
                Title = "Configure connection",
            };

            await dialog.ShowAsync();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            ShowConf();
        }

        private void button_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();


        }


        private async void OnPushNotification(PushNotificationChannel sender, PushNotificationReceivedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (e.NotificationType != PushNotificationType.Toast) return;
                e.Cancel = true;

                try
                {

                    txtNotifications.Text += DateTime.Now.ToString() + " Notification: " + (e.ToastNotification.Content.InnerText.Trim()) + "\r\n";
                    // Use ServiceStack.Text.WinRT to get this
                    //                var jsonObj = JsonSerializer.DeserializeFromString<JsonObject>(e.RawNotification.Content);

                    int i = 0;
                    // TODO: Do something with this value now that it works
                }
                catch (Exception err)
                {
                    //Debug.WriteLine(err.Message);
                }
            });
        }
        public void update_size(object sender, RoutedEventArgs e)
        {
            MainPage mp = (MainPage)sender;
            int i = 0;

        }
    }
}
