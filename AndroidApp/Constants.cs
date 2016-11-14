using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidApp
{
    public  class Constants
    {
        public const string SenderID = "1016049643626"; // Google API Project Number
        public static string ListenConnectionString = "Endpoint=sb://kfarubaiotdemo.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=VL9FZEckq+lIt3ejtgj7lI9h0b0hOCB9kc5nf4I5fKE=";
        public static string NotificationHubName = "kfnotificationhub";
        public static string Tag = "";

    }
}