using Android.Content;
using Android.Gms.Gcm;
using System;
using System.Collections.Generic;
using Android.Util;
using System.Threading.Tasks;
using System.Threading;

namespace Humance.Push.AeroGear
{
    /// <summary>
    /// Aero gear Android config.
    /// </summary>
    public class AeroGearAndroidConfig : IAeroGearConfig
    {
        private const string VARIANT_ID = "eb95d9db-2ae2-4c9a-8bcd-b603660f432d";
        private const string VARIANT_SECRET = "3123c516-6218-453a-aa80-eb1c63ef57c0";
        private const string DEVICE_TYPE = "ANDROID";
        private const string PLATFORM = "android";
        private const string DEFAULT_ALIAS = "";
        private const string GCM_SENDER_ID = "754965245212";
        private const string TOKEN_KEY = "token_id";

        public string VariantId { get; private set; }

        public string VariantSecret { get; private set; }

        public string DeviceType { get; private set; }

        public string Platform { get; private set; }

        public string OsVersion { get; private set; }

        public IList<string> Categories { get; private set; }

        public string Alias { get; private set; }

        public string DeviceToken { get; private set; }

        public AeroGearAndroidConfig(Context context)
        {
            // Set device parameters
            VariantId = VARIANT_ID;
            VariantSecret = VARIANT_SECRET;
            DeviceType = DEVICE_TYPE;
            Platform = PLATFORM;
            OsVersion = Android.OS.Build.VERSION.Release;
            Alias = DEFAULT_ALIAS;
            Categories = new List<string>();

            // Obtain the device token in a background thread
            ThreadPool.QueueUserWorkItem(o => GetDeviceToken(context));
        }

        private void GetDeviceToken(Context context)
        {
            // Load token from settings
            var settings = context.GetSharedPreferences("AerogearPushConfig", FileCreationMode.Private);
            var token = settings.GetString(TOKEN_KEY, String.Empty);

            // If the token obtained from seetings is empty, send a new token request to GCM
            if (String.IsNullOrWhiteSpace(token))
            {
                var gcm = GoogleCloudMessaging.GetInstance(context);
                try {
                    token = gcm.Register(GCM_SENDER_ID);
                } catch(Exception e) {
                    // TODO An exception might be rarely thrown here, if the
                    // GCM service is not responding. Handle this case here.
                }

                // Store the token in SharedPreferences
                var editor = settings.Edit();
                editor.PutString(TOKEN_KEY, token);

                //@ todo in AeroGearGCMPushRegistrar:302 the appversion is also saved?
                //@ todo Set expiration time
                editor.Commit();
            }
            DeviceToken = token;

            // check if app was updated; if so, it must clear registration id to
            // avoid a race condition if a GCM sends a message
            // @TODO TBD. See AeroGearGCMPushRegistrar.java:246
        }
    }
}