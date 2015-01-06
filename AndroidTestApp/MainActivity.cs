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
using Android.Util;

namespace AndroidTestApp
{
    [Activity(Label = "AndroidTestApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button button;
        int count = 1;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            button = FindViewById<Button>(Resource.Id.myButton);

            // button.Click += (s, e) =>
                // button.Text = string.Format("{0} clicks!", count++);

            await RegisterPushNotificationsAsync();
        }

        private async Task RegisterPushNotificationsAsync()
        {
            PushReceiver.Instance.MessageReceived += MessageReceived;
          
            var config = new AeroGearAndroidConfig(Android.App.Application.Context);
            var aerogear = new AeroGearClient(config);
            try
            {
                var aerogearServer = new Uri("https://pitajnas.rs:8443/ag-push/rest/registry/device");
                await aerogear.RegisterAsync(aerogearServer);
            }
            catch (Exception ex)
            {
                // TODO What happens in the case of no connectivity or server downtime?
            }
        }

        private void MessageReceived(object sender, PushMsgEventArgs e) 
        {
            button.Text = e.Message;
        }
    }
}


