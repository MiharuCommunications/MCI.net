namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAsyncSocketReceiver : IAsyncSocket
    {
        event EventHandler<ReceivedEventArgs> Received;
        event EventHandler Disconnected;



        Task<Either<IFailedReason, Unit>> StartReceivingAsync();
        Task<Either<IFailedReason, Unit>> StopReceivingAsync();
    }
}
