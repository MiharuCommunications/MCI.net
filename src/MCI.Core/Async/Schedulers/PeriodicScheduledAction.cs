//-----------------------------------------------------------------------
// <copyright file="PeriodicScheduledAction.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async.Schedulers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class PeriodicScheduledAction : IDisposable
    {
        internal protected PeriodicScheduledAction()
        {
            this.disposed = false;
        }

        protected void Start(Action<DateTime> action)
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var now = DateTime.Now;
                    var next = this.GetNext(now);

                    if (this.HasBecameTime(now, next))
                    {
                        if (this.disposed)
                        {
                            return;
                        }

                        try
                        {
                            action(now);
                        }
                        catch { }

                        await this.Skip();
                    }
                    else
                    {
                        await this.DelayToNext(now, next);
                    }
                }
            });
        }


        protected abstract DateTime GetNext(DateTime now);


        protected abstract bool HasBecameTime(DateTime now, DateTime next);


        protected abstract TimeSpan GetDelay(DateTime now, DateTime next);

        protected abstract Task Skip();

        protected abstract Task DelayToNext(DateTime now, DateTime next);



        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose 処理
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
