namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class AsyncSocketReceiverFactory : IAsyncSocketReceiverFactory
    {
        public IAsyncSocketReceiver Create(IPAddress remoteIP, int remotePort, ProtocolType protocolType, int bufferSize, int sendTimeout, int receiveTimeout)
        {
            return new AsyncSocketReceiver(remoteIP, remotePort, protocolType, bufferSize, sendTimeout, receiveTimeout);
        }
    }
}
