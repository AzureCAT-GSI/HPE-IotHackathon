using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Gcm.Client;
using Android.Content;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        public static MainActivity instance;
        public string notificationHub;

        protected override void OnDestroy()
        {
            //store
            var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
            var prefEditor = prefs.Edit();

            EditText edit = FindViewById<EditText>(Resource.Id.txtNotificationHub);
            Constants.NotificationHubName = edit.Text;
            prefEditor.PutString("NotificationHub", edit.Text);

            edit = FindViewById<EditText>(Resource.Id.txtConnectionString);
            Constants.ListenConnectionString = edit.Text;
            prefEditor.PutString("ListenConnectionString", edit.Text);

            edit = FindViewById<EditText>(Resource.Id.txtTag);
            Constants.Tag = edit.Text;
            prefEditor.PutString("Tag", edit.Text);

            prefEditor.Commit();

            base.OnDestroy();
        }
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

          try
            {
                var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);

                EditText edit = FindViewById<EditText>(Resource.Id.txtNotificationHub);
                edit.Text=prefs.GetString("NotificationHub", "kfnotificationhub");

                edit = FindViewById<EditText>(Resource.Id.txtConnectionString);
                edit.Text = prefs.GetString("ListenConnectionString", "Endpoint=sb://kfarubaiotdemo.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=VL9FZEckq+lIt3ejtgj7lI9h0b0hOCB9kc5nf4I5fKE=");

                edit = FindViewById<EditText>(Resource.Id.txtTag);
                edit.Text = prefs.GetString("Tag", null);


            }
            catch
            {

            }

        }

        private  void ShowConf()
        {

            RegisterWithGCM();
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            EditText edit = FindViewById<EditText>(Resource.Id.txtNotificationHub);
            Constants.NotificationHubName = edit.Text;

            edit = FindViewById<EditText>(Resource.Id.txtConnectionString);
            Constants.ListenConnectionString = edit.Text;

            edit = FindViewById<EditText>(Resource.Id.txtTag);
            Constants.Tag= edit.Text;

            ShowConf();
            
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

