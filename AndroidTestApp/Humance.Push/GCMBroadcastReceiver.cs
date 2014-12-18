using System;
using Android.Gms.Gcm;
using Android.App;
using Android.Content;

namespace Humance.Push
{
    [BroadcastReceiver(Permission = "com.google.android.c2dm.permission.SEND")]
    [IntentFilter(new string[] { "com.google.android.c2dm.intent.RECEIVE" }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class GCMBroadcastReceiver : BroadcastReceiver
    {
        public const int NOTIFICATION_ID = 1;
        private static IMessageHandler DefaultHandler;
        private static Boolean CheckDefaultHandler { get; set; }

        public const String ERROR = "org.jboss.aerogear.android.unifiedpush.ERROR";
        public const String MESSAGE = "org.jboss.aerogear.android.unifiedpush.MESSAGE";
        public const String DELETED = "org.jboss.aerogear.android.unifiedpush.DELETED";

        public static IMessageHandler MessageHandler { get; set; }

        private readonly String TAG = typeof(GCMBroadcastReceiver).Name;

        public GCMBroadcastReceiver()
        {
            CheckDefaultHandler = true;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            //@todo checkdefaulthandler

            GoogleCloudMessaging gcm = GoogleCloudMessaging.GetInstance(context);
            String messageType = gcm.GetMessageType(intent);
            if (GoogleCloudMessaging.MessageTypeSendError.Equals(messageType))
            {
                intent.PutExtra(ERROR, true);
            }
            else if (GoogleCloudMessaging.MessageTypeDeleted.Equals(messageType))
            {
                intent.PutExtra(DELETED, true);
            }
            else
            {
                intent.PutExtra(MESSAGE, true);
            }

            if (MessageHandler != null)
            {
                MessageHandler.OnMessage(context, intent.Extras);
            }
            //@TODO notify all messagehandler implementations, not just this one.
        }
    }
}

