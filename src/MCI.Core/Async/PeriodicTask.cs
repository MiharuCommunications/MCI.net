//-----------------------------------------------------------------------
// <copyright file="PeriodicTask.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PeriodicTask : IDisposable
    {
        private bool disposed;
        private bool canceled;

        private Func<Task> task;


        private object locker;



        public TimeSpan Period { get; private set; }


        public event EventHandler Finish;

        public PeriodicTask(TimeSpan period, Func<Task> task)
        {
            this.disposed = false;
            this.canceled = false;

            this.task = task;
            this.locker = new object();

            this.Period = period;
        }


        public async Task StartAsync()
        {
            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    lock (this.locker)
                    {
                        if (this.canceled)
                        {
                            this.OnFinish();
                            return;
                        }
                    }

                    try
                    {
                        await this.task();
                    }
                    catch { }

                    lock (this.locker)
                    {
                        if (this.canceled)
                        {
                            this.OnFinish();
                            return;
                        }
                    }

                    await Task.Delay(this.Period);
                }
            });
        }


        public void Stop()
        {
            this.canceled = true;
        }


        public Task StopAndWaitForFinishAsync()
        {
            var task = new Task(() => { });

            EventHandler callback = null;
            callback = (sender, e) =>
            {
                if (!task.IsCompleted)
                {
                    this.Finish -= callback;
                    task.RunSynchronously();
                }
            };

            this.Finish += callback;
            this.canceled = true;

            return task;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose 処理
                if (!this.canceled)
                {
                    this.Stop();
                }

            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }



        protected virtual void OnFinish()
        {
            if (this.Finish != null)
            {
                this.Finish(this, new EventArgs());
            }
        }
    }
}
