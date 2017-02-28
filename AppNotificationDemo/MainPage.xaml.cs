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
    }
}
