namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public interface IAsyncTcpListener : IDisposable
    {
        IPAddress LocalAddress { get; }
        int LocalPort { get; }

        void Start();
        void Stop();
    }
}
