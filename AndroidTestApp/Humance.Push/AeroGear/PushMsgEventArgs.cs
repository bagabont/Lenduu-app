using System;

namespace Humance.Push.AeroGear
{
    public class PushMsgEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public PushMsgEventArgs(string msg)
        {
            Message = msg;
        }
    }
}

