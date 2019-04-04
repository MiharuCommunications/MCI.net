namespace Miharu.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class AsyncSocket : IAsyncSocket
    {
        private bool disposed;
        private Socket socket;
        private byte[] receiveBuffer;

        public ProtocolType Protocol { get; private set; }
        public IPEndPoint RemoteEndPoint { get; private set; }


        public int BufferSize { get; private set; }


        public AsyncSocket(IPAddress remoteIP, int remotePort, ProtocolType protocolType, int bufferSize, int sendTimeout, int receiveTimeout)
        {
            this.disposed = false;

            this.receiveBuffer = new byte[bufferSize];
            this.BufferSize = bufferSize;

            this.RemoteEndPoint = new IPEndPoint(remoteIP, remotePort);
            this.Protocol = protocolType;

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, this.Protocol);
            this.socket.SendTimeout = sendTimeout;
            this.socket.ReceiveTimeout = receiveTimeout;
        }

        /// <summary>
        /// 送信時のタイムアウト時間
        /// </summary>
        public int SendTimeout
        {
            get
            {
                return this.socket.SendTimeout;
            }

            set
            {
                this.socket.SendTimeout = value;
            }
        }

        /// <summary>
        /// 受信時のタイムアウト時間
        /// </summary>
        public int ReceiveTimeout
        {
            get
            {
                return this.socket.ReceiveTimeout;
            }

            set
            {
                this.socket.ReceiveTimeout = value;
            }
        }

        /// <summary>
        /// 非同期的に接続します
        /// </summary>
        /// <returns></returns>
        public Task<Either<IFailedReason, Unit>> ConnectAsync()
        {
            Either<IFailedReason, Unit> result = new Left<IFailedReason, Unit>(new NotImplementedError());
            var task = new Task<Either<IFailedReason, Unit>>(() => result);

            try
            {
                Action<IAsyncResult> callback = ar =>
                {
                    lock (task)
                    {
                        if (!task.IsCompleted)
                        {
                            try
                            {
                                this.socket.EndConnect(ar);
                                result = new Right<IFailedReason, Unit>(Unit.Instance);
                            }
                            catch (Exception ex)
                            {
                                result = new Left<IFailedReason, Unit>(new UnresolvedError(ex));
                            }
                            finally
                            {
                                task.RunSynchronously();
                            }
                        }
                    }
                };

                this.socket.BeginConnect(this.RemoteEndPoint, new AsyncCallback(callback), new object());
            }
            catch (Exception ex)
            {
                lock (task)
                {
                    result = new Left<IFailedReason, Unit>(new UnresolvedError(ex));
                    task.RunSynchronously();
                }
            }

            return task;
        }

        /// <summary>
        /// 非同期的に接続を解除します
        /// </summary>
        /// <returns></returns>
        public Task<Either<IFailedReason, Unit>> DisconnectAsync()
        {
            var result = Either.ToLeft<IFailedReason, Unit>(new NotImplementedError());
            var task = new Task<Either<IFailedReason, Unit>>(() => result);

            try
            {
                Action<IAsyncResult> callback = ar =>
                {
                    lock (task)
                    {
                        if (!task.IsCompleted)
                        {
                            try
                            {
                                this.socket.EndDisconnect(ar);
                                result = Either.ToRight<IFailedReason, Unit>(Unit.Instance);
                            }
                            catch (Exception ex)
                            {
                                result = Either.ToLeft<IFailedReason, Unit>(new UnresolvedError(ex));
                            }
                            finally
                            {
                                task.RunSynchronously();
                            }
                        }
                    }
                };

                this.socket.BeginDisconnect(true, new AsyncCallback(callback), new object());
            }
            catch (Exception ex)
            {
                lock (task)
                {
                    result = Either.ToLeft<IFailedReason, Unit>(new UnresolvedError(ex));
                    task.RunSynchronously();
                }
            }

            return task;
        }

        /// <summary>
        /// 非同期でデータを送信します
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public Task<Either<IFailedReason, Unit>> SendAsync(byte[] packet)
        {
            var result = Either.ToLeft<IFailedReason, Unit>(new NotImplementedError());
            var task = new Task<Either<IFailedReason, Unit>>(() => result);

            try
            {
                Action<IAsyncResult> callback = ar =>
                {
                    lock (task)
                    {
                        if (task.IsCompleted)
                        {
                            return;
                        }

                        try
                        {
                            var sended = this.socket.EndSend(ar);

                            if (sended == packet.Length)
                            {
                                result = Either.ToRight<IFailedReason, Unit>(Unit.Instance);
                            }
                            else
                            {
                                result = Either.ToLeft<IFailedReason, Unit>(new UnresolvedError(new Exception("AsyncSocketClient.SendAsync : 送信できたパケットの長さと送信しようとしたパケットの長さが異なります。")));
                            }
                        }
                        catch (Exception ex)
                        {
                            result = Either.ToLeft<IFailedReason, Unit>(new UnresolvedError(ex));
                        }
                        finally
                        {
                            task.RunSynchronously();
                        }
                    }
                };

                this.socket.BeginSend(packet, 0, packet.Length, SocketFlags.None, new AsyncCallback(callback), new object());
            }
            catch (Exception ex)
            {
                lock (task)
                {
                    result = Either.ToLeft<IFailedReason, Unit>(new UnresolvedError(ex));
                    task.RunSynchronously();
                }
            }

            return task;
        }

        /// <summary>
        /// 非同期でデータを受信します
        /// </summary>
        /// <returns></returns>
        public Task<Either<IFailedReason, byte[]>> ReceiveAsync()
        {
            var result = Either.ToLeft<IFailedReason, byte[]>(new NotImplementedError());
            var task = new Task<Either<IFailedReason, byte[]>>(() => result);

            try
            {
                Action<IAsyncResult> callback = ar =>
                {
                    lock (task)
                    {
                        if (task.IsCompleted)
                        {
                            return;
                        }

                        try
                        {
                            int bytesRead = this.socket.EndReceive(ar);

                            var packet = new byte[bytesRead];
                            Array.Copy(this.receiveBuffer, packet, bytesRead);

                            result = Either.ToRight<IFailedReason, byte[]>(packet);
                        }
                        catch (Exception ex)
                        {
                            result = Either.ToLeft<IFailedReason, byte[]>(new UnresolvedError(ex));
                        }
                        finally
                        {
                            task.RunSynchronously();
                        }
                    }
                };

                this.socket.BeginReceive(this.receiveBuffer, 0, this.BufferSize, SocketFlags.None, new AsyncCallback(callback), new object());
            }
            catch (ObjectDisposedException ex)
            {
                lock (task)
                {
                    // Socket が閉じられている
                    result = Either.ToLeft<IFailedReason, byte[]>(new UnresolvedError(ex));
                    task.RunSynchronously();
                }
            }
            catch (Exception ex)
            {
                lock (task)
                {
                    result = Either.ToLeft<IFailedReason, byte[]>(new UnresolvedError(ex));
                    task.RunSynchronously();
                }
            }

            return task;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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
                    this.socket.Close();
                }
                catch
                {
                }

                this.socket.Dispose();
            }

            this.disposed = true;
        }
    }
}
