using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Gcm.Client;
using Android.Content;
using System;
using Android.Views;

namespace AndroidApp
{

    public class Configuration
    {
        public string NotificationHub;
        public string ConnectionString;
        public string Tag;
    }

    [Activity(Label = "Proximity App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        public static MainActivity instance;
        public string notificationHub;

        public Configuration configuration;

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void configure(View v)
        {
            SetContentView(Resource.Layout.Main);

            EditText edit = FindViewById<EditText>(Resource.Id.txtNotificationHub);
            if (null != edit)
            {
                edit.Text = configuration.NotificationHub;
            }
            edit = FindViewById<EditText>(Resource.Id.txtPolicy);
            if (null != edit)
            {
                edit.Text = configuration.ConnectionString;
            }

            edit = FindViewById<EditText>(Resource.Id.txtTag);
            if (null != edit)
            {
                edit.Text = configuration.Tag;
            }

        }
        //Logic implement 




    protected override void OnStart()
        {

            Title = "Proximity App";
            // Get your button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btnRegister);
            if (null != button)
            {
                button.Click += btnRegister_Click;
            }

            button = FindViewById<Button>(Resource.Id.bnSave);
            if (null != button)
            {
                button.Click += btnSave_Click;
            }


            button = FindViewById<Button>(Resource.Id.btnConfigure);
            if (null != button)
            {
                button.Click += Next_Click;
            }
            button = FindViewById<Button>(Resource.Id.btnClose);
            if (null != button)
            {
                button.Click += Close_Click;
            }



            EditText edit = FindViewById<EditText>(Resource.Id.txtNotificationHub);
            if (null != edit)
            {
                edit.Text = configuration.NotificationHub;
            }
            edit = FindViewById<EditText>(Resource.Id.txtPolicy);
            if (null != edit)
            {
                edit.Text = configuration.ConnectionString;
            }

            edit = FindViewById<EditText>(Resource.Id.txtTag);
            if (null != edit)
            {
                edit.Text = configuration.Tag;
            }

            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnCreate(Bundle bundle)
        {
            instance = this;
            configuration = new Configuration();
            

            base.OnCreate(bundle);

        
            try
            {
                var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
                configuration.NotificationHub = prefs.GetString("NotificationHub", "<notificationhubname>"); ;
                configuration.ConnectionString = prefs.GetString("ListenConnectionString", "Endpoint=sb://<namespace>.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=<key>");
                configuration.Tag = prefs.GetString("Tag", null);
            }
            catch(Exception Ex)
            {

            }

            SetContentView(Resource.Layout.home);
            Button button = FindViewById<Button>(Resource.Id.btnConfigure);
            if (null != button)
            {
                button.Click += Next_Click;
            }
            button = FindViewById<Button>(Resource.Id.btnClose);
            if (null != button)
            {
                button.Click += Close_Click;
            }



        }

        private  void Register()
        {

            RegisterWithGCM();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //store
            try
            {
                var prefs = Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
                var prefEditor = prefs.Edit();

                EditText edit = FindViewById<EditText>(Resource.Id.txtNotificationHub);
                if (null != edit)
                {
                    Constants.NotificationHubName = edit.Text;
                    configuration.NotificationHub = edit.Text;
                    prefEditor.PutString("NotificationHub", edit.Text);
                }

                edit = FindViewById<EditText>(Resource.Id.txtPolicy);
                if (null != edit)
                {
                    Constants.ListenConnectionString = edit.Text;
                    configuration.ConnectionString = edit.Text;
                    prefEditor.PutString("ListenConnectionString", edit.Text);
                }

                edit = FindViewById<EditText>(Resource.Id.txtTag);
                if (null != edit)
                {
                    Constants.Tag = edit.Text;
                    configuration.Tag = edit.Text;
                    prefEditor.PutString("Tag", edit.Text);
                }

                prefEditor.Commit();
            }
            catch(Exception Ex)
            { }

            SetContentView(Resource.Layout.home);
            Button button = FindViewById<Button>(Resource.Id.btnConfigure);
            if (null != button)
            {
                button.Click += Next_Click;
            }

            button = FindViewById<Button>(Resource.Id.btnClose);
            if (null != button)
            {
                button.Click += Close_Click;
            }

        }

        private void Close_Click(object sender, System.EventArgs e)
        {
            Finish();
        }


        private void Next_Click(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.Main);

            EditText edit = FindViewById<EditText>(Resource.Id.txtNotificationHub);
            if (null != edit)
            {
                edit.Text = configuration.NotificationHub;
            }
            edit = FindViewById<EditText>(Resource.Id.txtPolicy);
            if (null != edit)
            {
                edit.Text = configuration.ConnectionString;
            }

            edit = FindViewById<EditText>(Resource.Id.txtTag);
            if (null != edit)
            {
                edit.Text = configuration.Tag;
            }

            // Get your button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btnRegister);
            if (null != button)
            {
                button.Click += btnRegister_Click;
            }

            button = FindViewById<Button>(Resource.Id.bnSave);
            if (null != button)
            {
                button.Click += btnSave_Click;
            }

            button = FindViewById<Button>(Resource.Id.btnUnregister);
            if (null != button)
            {
                button.Click += btnUnRegister_Click;
            }
        }

        private void btnUnRegister_Click(object sender, System.EventArgs e)
        {
            UnRegisterWithGCM();
        }

        private void btnRegister_Click(object sender, System.EventArgs e)
        {
            EditText edit = FindViewById<EditText>(Resource.Id.txtNotificationHub);
            if (null != edit)
            {
                Constants.NotificationHubName = edit.Text;
            }
            edit = FindViewById<EditText>(Resource.Id.txtPolicy);
            if (null != edit)
            {
                Constants.ListenConnectionString = edit.Text;
            }

            edit = FindViewById<EditText>(Resource.Id.txtTag);
            if (null != edit)
            {
                Constants.Tag = edit.Text;
            }

            Register();
            
        }


        private void UnRegisterWithGCM()
        {
            // Check to ensure everything's set up right
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);


            // Register for push notifications
            Log.Info("MainActivity", "UnRegistering...");
            GcmClient.UnRegister(this);
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

