using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Gcm.Client;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        public static MainActivity instance;
        protected override void OnCreate(Bundle bundle)
        {
            instance = this;

            base.OnCreate(bundle);

            // Set your view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get your button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Click += Button_Click;
            RegisterWithGCM();
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Text = "You clicked me";
        }

        private void RegisterWithGCM()
        {
            // Check to ensure everything's set up right
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            // Register for push notifications
            Log.Info("MainActivity", "Registering...");
            GcmClient.Register(this, Constants.SenderID);
        }
    }
}

