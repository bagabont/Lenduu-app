using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Humance.Push;
using Humance.Push.AeroGear;

namespace AndroidTestApp
{
    [Activity(Label = "AndroidTestApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IMessageHandler
    {
        Button button;
        int count = 1;

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += (s, e) =>
                button.Text = string.Format("{0} clicks!", count++);

            await RegisterPushNotificationsAsync();
        }

        private async Task RegisterPushNotificationsAsync()
        {
            GCMBroadcastReceiver.MessageHandler = this;
            var config = new AeroGearAndroidConfig(Android.App.Application.Context);
            var aerogear = new AeroGearClient(config);
            try
            {
                var aerogearServer = new Uri("https://pitajnas.rs:8443/ag-push/rest/registry/device");
                await aerogear.RegisterAsync(aerogearServer);
                Toast.MakeText(Android.App.Application.Context, "Woohoo! succeeded.", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, "You suck at C# coding." + ex.Message, ToastLength.Short).Show();
            }
        }

        public void OnMessage(Context context, Bundle message)
        {
            button.Text = message.GetString("alert");
        }

        public void OnDeleteMessage(Context context, Bundle message)
        {
        }

        public void OnError()
        {
        }
    }
}


