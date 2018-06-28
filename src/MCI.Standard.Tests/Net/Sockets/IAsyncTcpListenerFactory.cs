namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public interface IAsyncTcpListenerFactory
    {
        IAsyncTcpListener CreateWithOptions(IPAddress localAddress, int localPort, AsyncTcpListenerOptions options);

        IAsyncTcpListener CreateWithOptions(int localPort, AsyncTcpListenerOptions options);
    }
}
