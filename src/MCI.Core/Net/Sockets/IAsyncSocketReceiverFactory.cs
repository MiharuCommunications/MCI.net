namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public interface IAsyncSocketReceiverFactory
    {
        IAsyncSocketReceiver Create(IPAddress remoteIP, int remotePort, ProtocolType protocolType, int bufferSize, int sendTimeout, int receiveTimeout);
    }
}
