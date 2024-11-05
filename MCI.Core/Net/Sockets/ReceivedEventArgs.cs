namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReceivedEventArgs : EventArgs
    {
        public ReceivedEventArgs(byte[] packet)
        {
            this.Packet = packet;
        }

        public byte[] Packet { get; private set; }
    }
}
