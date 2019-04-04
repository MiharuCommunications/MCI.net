namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class AsyncSocketReceiver : AsyncSocket, IAsyncSocketReceiver
    {
        public bool IsReceiving { get; private set; }

        public event EventHandler<ReceivedEventArgs> Received;
        public event EventHandler Disconnected;


        public AsyncSocketReceiver(IPAddress remoteIP, int remotePort, ProtocolType protocolType, int bufferSize, int sendTimeout, int receiveTimeout)
            : base(remoteIP, remotePort, protocolType, bufferSize, sendTimeout, receiveTimeout)
        {
            this.IsReceiving = false;
        }




        public async Task<Either<IFailedReason, Unit>> StartReceivingAsync()
        {
            if (this.IsReceiving)
            {
                return Either.ToRight<IFailedReason, Unit>(Unit.Instance);
            }

            var result = await this.ConnectAsync();
            if (result.IsLeft)
            {
                return result;
            }

            this.IsReceiving = true;

            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if (!this.IsReceiving)
                    {
                        return;
                    }

                    var received = await this.ReceiveAsync();

                    if (received.IsRight)
                    {
                        var packet = received.Get();

                        if (0 < packet.Length)
                        {
                            await Task.Factory.StartNew(() =>
                            {
                                this.OnReceived(packet);
                            });

                            continue;
                        }
                    }

                    await Task.Delay(500);
                }
            });

            return Either.ToRight<IFailedReason, Unit>(Unit.Instance);
        }

        public async Task<Either<IFailedReason, Unit>> StopReceivingAsync()
        {
            if (!this.IsReceiving)
            {
                return Either.ToRight<IFailedReason, Unit>(Unit.Instance);
            }

            this.IsReceiving = false;

            var result = await this.DisconnectAsync();

            this.OnDisconnected();

            return result;
        }

        protected virtual void OnReceived(byte[] packet)
        {
            if (this.Received != null)
            {
                this.Received(this, new ReceivedEventArgs(packet));
            }
        }



        protected virtual void OnDisconnected()
        {
            if (this.Disconnected != null)
            {
                this.Disconnected(this, new EventArgs());
            }
        }

    }
}
