using System;
using Android.OS;
using Android.Content;

namespace Humance.Push
{
    public interface IMessageHandler
    {
        void OnMessage(Context context, Bundle message);
        void OnDeleteMessage(Context context, Bundle message);
        void OnError();
    }
}