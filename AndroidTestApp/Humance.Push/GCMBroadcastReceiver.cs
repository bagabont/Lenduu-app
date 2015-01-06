using System;
using Android.Gms.Gcm;
using Android.App;
using Android.Content;
using Android.Util;
using Humance.Push.AeroGear;

namespace Humance.Push
{
    [BroadcastReceiver(Permission = "com.google.android.c2dm.permission.SEND")]
    [IntentFilter(new string[] { "com.google.android.c2dm.intent.RECEIVE" }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class GCMBroadcastReceiver : BroadcastReceiver
    {
        public const String ERROR = "org.jboss.aerogear.android.unifiedpush.ERROR";
        public const String MESSAGE = "org.jboss.aerogear.android.unifiedpush.MESSAGE";
        public const String DELETED = "org.jboss.aerogear.android.unifiedpush.DELETED";

        private readonly String TAG = typeof(GCMBroadcastReceiver).Name;

        public GCMBroadcastReceiver()
        {
            Log.Info("ctor", Environment.StackTrace);
        }

        public override void OnReceive(Context context, Intent intent)
        {
            GoogleCloudMessaging gcm = GoogleCloudMessaging.GetInstance(context);
            String messageType = gcm.GetMessageType(intent);
            if (GoogleCloudMessaging.MessageTypeSendError.Equals(messageType))
            {
                PushReceiver.Instance.OnMessageError();
            }
            else if (GoogleCloudMessaging.MessageTypeDeleted.Equals(messageType))
            {
                var message = intent.Extras.GetString("alert");
                PushReceiver.Instance.OnMessageDelete(message);
            }
            else
            {
                var message = intent.Extras.GetString("alert");
                PushReceiver.Instance.OnMessageReceived(message);
            }
        }
    }
}