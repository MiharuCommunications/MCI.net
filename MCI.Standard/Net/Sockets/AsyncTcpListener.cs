namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    public class AsyncTcpListener : IAsyncTcpListener
    {
        private bool disposed = false;

        private TcpListener listener;
        private Thread listenerThread;


        public IPAddress LocalAddress { get; private set; }
        public int LocalPort { get; private set; }

        public AsyncTcpListenerOptions Options { get; private set; }


        public AsyncTcpListener(IPAddress localAddress, int localPort, AsyncTcpListenerOptions options)
        {
            this.LocalAddress = localAddress;
            this.LocalPort = localPort;
            this.Options = options;

            this.listener = new TcpListener(this.LocalAddress, this.LocalPort);
            this.listenerThread = new Thread(new ThreadStart(this.listenForClients));
            this.listenerThread.Name = "AsyncTcpListener";
        }


        public void Start()
        {
            this.listenerThread.Start();
        }

        public void Stop()
        {
            this.listener.Stop();
        }


        private void listenForClients()
        {
            this.listener.Start();

            while (!this.disposed)
            {
                try
                {
                    if (this.listener.Pending())
                    {
                        TcpClient client = this.listener.AcceptTcpClient();

                        //create a thread to handle communication
                        //with connected client
                        Thread clientThread = new Thread(new ParameterizedThreadStart(this.handleClientComm));
                        clientThread.Start(client);
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch (SocketException ex)
                {
                    if (ex.ErrorCode == 10004)
                    {
                        return;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (InvalidOperationException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }


        private void handleClientComm(object client)
        {
            try
            {
                var tcpClient = (TcpClient)client;
                var remoteEndPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;

                var ns = tcpClient.GetStream();
                var buffer = new byte[256];
                var ms = new MemoryStream();
                var disconnected = false;

                do
                {
                    var size = ns.Read(buffer, 0, buffer.Length);
                    if (size == 0)
                    {
                        // Clientが切断された
                        break;
                    }

                    ms.Write(buffer, 0, size);
                } while (ns.DataAvailable);

                var received = ms.ToArray();
                ms.Close();

                var responce = this.Options.OnReceive(received, remoteEndPoint);

                if (!disconnected)
                {
                    ns.Write(responce, 0, responce.Length);
                }


                tcpClient.Close();
                ns.Close();
            }
            catch (Exception)
            {
            }
        }



        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                try
                {
                    this.listener.Stop();
                    this.listener.Server.Dispose();
                }
                catch (Exception ex)
                {
                    var i = 0;
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
