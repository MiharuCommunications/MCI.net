//-----------------------------------------------------------------------
// <copyright file="AsyncLockerTaskItem.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async
{
    using System;
    using System.Threading.Tasks;

    internal class AsyncLockerTaskItem : IDisposable
    {
        private bool disposed;

        private Func<Task> action;

        private Task callback;

        public AsyncLockerTaskItem(Func<Task> action, Task callback)
        {
            // callback ?

            this.action = action;
            this.callback = callback;
        }



        public Task ExecuteAsync()
        {
            var result = new Task(() => { });

            try
            {
                this.action().ContinueWith(t =>
                {
                    lock (this.callback)
                    {
                        if (!this.disposed && !result.IsCompleted)
                        {
                            this.callback.RunSynchronously();

                            result.RunSynchronously();
                        }
                    }
                });
            }
            catch
            {
                lock (this.callback)
                {
                    if (!this.disposed && !result.IsCompleted)
                    {
                        this.callback.RunSynchronously();

                        result.RunSynchronously();
                    }
                }
            }

            return result;
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
            }

            lock (this.callback)
            {
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
