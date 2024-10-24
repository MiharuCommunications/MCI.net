namespace Miharu.Net
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Text;
    using System.Threading.Tasks;

    public class PingService : IPingService
    {
        public Task<Either<IFailedReason, Unit>> TryPingAsync(IPAddress remoteIP)
        {
            var result = Either.ToLeft<IFailedReason, Unit>(new NotImplementedError());
            var task = new Task<Either<IFailedReason, Unit>>(() => result);

            var pingSender = new Ping();


            PingCompletedEventHandler callback = null;
            callback = (sender, e) =>
            {
                lock (task)
                {
                    if (task.IsCompleted)
                    {
                        return;
                    }

                    if (e.Reply.Status == IPStatus.Success)
                    {
                        result = Either.ToRight<IFailedReason, Unit>(Unit.Instance);
                    }
                    else
                    {
                        result = Either.ToLeft<IFailedReason, Unit>(new UnkownError(""));
                    }

                    pingSender.PingCompleted -= callback;
                    task.RunSynchronously();
                }
            };

            pingSender.PingCompleted += callback;

            try
            {
                pingSender.SendAsync(remoteIP, new object());
            }
            catch (Exception ex)
            {
                pingSender.PingCompleted -= callback;

                result = Either.ToLeft<IFailedReason, Unit>(new UnresolvedError(ex));

                  task.RunSynchronously();
            }

            return task;
        }
    }
}
