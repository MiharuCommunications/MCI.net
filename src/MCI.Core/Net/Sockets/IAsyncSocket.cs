namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAsyncSocket : IDisposable
    {
        ProtocolType Protocol { get; }
        IPEndPoint RemoteEndPoint { get; }

        Task<Either<IFailedReason, Unit>> ConnectAsync();
        Task<Either<IFailedReason, Unit>> DisconnectAsync();

        Task<Either<IFailedReason, Unit>> SendAsync(byte[] packet);
        Task<Either<IFailedReason, byte[]>> ReceiveAsync();
    }
}
