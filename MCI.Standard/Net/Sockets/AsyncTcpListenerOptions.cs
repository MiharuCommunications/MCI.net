namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public class AsyncTcpListenerOptions
    {
        public Func<byte[], IPEndPoint, byte[]> OnReceive { get; set; }
    }
}
