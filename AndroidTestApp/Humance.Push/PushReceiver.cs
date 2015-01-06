using System;
using Humance.Push.AeroGear;

namespace Humance.Push
{
    public class PushReceiver
    {
        public event EventHandler<PushMsgEventArgs> MessageReceived;

        private static PushReceiver _instance;
        public static PushReceiver Instance { get { if (_instance == null) _instance = new PushReceiver(); return _instance; } }

        private PushReceiver() {}

        public void OnMessageReceived(string message)
        {
            var handler = MessageReceived;
            if (handler != null)
            {
                var args = new PushMsgEventArgs(message);
                handler(this, args);
            }
        }

        public void OnMessageDelete(string message)
        {
        }

        public void OnMessageError()
        {
        }
    }
}

