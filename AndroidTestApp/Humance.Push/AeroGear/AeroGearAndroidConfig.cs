using Android.Content;
using Android.Gms.Gcm;
using System;
using System.Collections.Generic;

namespace Humance.Push.AeroGear
{
    /// <summary>
    /// Aero gear Android config.
    /// </summary>
    public class AeroGearAndroidConfig : IAeroGearConfig
    {
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
            VariantId = "eb95d9db-2ae2-4c9a-8bcd-b603660f432d";
            VariantSecret = "3123c516-6218-453a-aa80-eb1c63ef57c0";
            DeviceType = "ANDROID";
            Platform = "android";
            OsVersion = Android.OS.Build.VERSION.Release;
            Alias = "XamarinAndroidTestApp";
            DeviceToken = GetDeviceToken(context);
        }

        private string GetDeviceToken(Context context)
        {
            // Try to load token from settings
            var settings = context.GetSharedPreferences("AerogearPushConfig", FileCreationMode.Private);
            var token = settings.GetString(TOKEN_KEY, String.Empty);

            // If the token is empty, send a new token request to GCM
            if (String.IsNullOrWhiteSpace(token))
            {
                var gcm = GoogleCloudMessaging.GetInstance(context);
                token = gcm.Register(GCM_SENDER_ID);

                // Store token to settings.
                var editor = settings.Edit();
                editor.PutString(TOKEN_KEY, token);
                //@ todo in AeroGearGCMPushRegistrar:302 the appversion is also saved?
                //@ todo Set expiration time
                editor.Commit();
            }
            return token;

            //TODO - Please, check if actually you need to store
            // the device token inside settings. With other platforms,
            // this is not recommended.

            // check if app was updated; if so, it must clear registration id to
            // avoid a race condition if a GCM sends a message
            // @TODO TBD. See AeroGearGCMPushRegistrar.java:246
        }
    }
}