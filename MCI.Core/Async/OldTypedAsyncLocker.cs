namespace Miharu.Async
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class OldTypedAsyncLocker : IDisposable
    {
        private object sync;

        private bool disposed;

        private Queue<Func<Task>> tasks;

        private bool isExecuting;

        public OldTypedAsyncLocker()
        {
            this.sync = new object();
            this.disposed = false;
            this.tasks = new Queue<Func<Task>>();
            this.isExecuting = false;
        }

        public Task<Try<T>> WithLock<T>(Func<Task<Try<T>>> action)
        {
            var result = Try<T>.Fail(new TimeoutException());
            var task = new Task<Try<T>>(() => result);

            lock (this.sync)
            {
                this.tasks.Enqueue(async () =>
                {
                    try
                    {
                        result = await action();
                    }
                    catch (Exception ex)
                    {
                        result = Try<T>.Fail(ex);
                    }
                    finally
                    {
                        task.RunSynchronously();
                    }
                });

                if (!this.isExecuting)
                {
                    this.isExecuting = true;

                    Task.Factory.StartNew(async () =>
                    {
                        while (true)
                        {
                            var target = (Func<Task>)null;

                            lock (this.sync)
                            {
                                if (this.tasks.Count == 0)
                                {
                                    this.isExecuting = false;
                                    return;
                                }

                                target = this.tasks.Dequeue();
                            }

                            try
                            {
                                await target();
                            }
                            catch
                            {
                            }
                        }
                    });
                }
            }

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
                lock (this.sync)
                {
                    this.tasks.Clear();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OldTypedAsyncLocker()
        {
            this.Dispose();
        }
    }
}
