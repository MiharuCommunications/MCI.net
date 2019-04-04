namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public class AsyncTcpListenerFactory : IAsyncTcpListenerFactory
    {
        public IAsyncTcpListener CreateWithOptions(IPAddress localAddress, int localPort, AsyncTcpListenerOptions options)
        {
            return new AsyncTcpListener(localAddress, localPort, options);
        }

        public IAsyncTcpListener CreateWithOptions(int localPort, AsyncTcpListenerOptions options)
        {
            return new AsyncTcpListener(IPAddress.Any, localPort, options);
        }
    }
}
