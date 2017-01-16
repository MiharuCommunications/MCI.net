//-----------------------------------------------------------------------
// <copyright file="AsyncLocker.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// async/await を用いてノンブロッキングな lock を提供するクラス
    /// </summary>
    public class AsyncLocker : IDisposable
    {
        private bool isExecuting = false;

        private bool disposed = false;

        private Queue<Func<Task>> tasks = new Queue<Func<Task>>();

        private object locker = new object();

        public AsyncLocker()
        {
        }


        public Task<Try> WithLock(Func<Task<Try>> action)
        {
            var result = Try.Fail(new TimeoutException());
            var task = new Task<Try>(() => result);


            lock (this.locker)
            {
                // キューに入れる
                this.tasks.Enqueue(async () =>
                {
                    try
                    {
                        result = await action();
                    }
                    catch(Exception ex)
                    {
                        result = Try.Fail(ex);
                    }
                    finally
                    {
                        task.RunSynchronously();
                    }
                });

                if (!this.isExecuting)
                {
                    this.isExecuting = true;

                    // キューが処理中でなければ、実行する
                    Task.Factory.StartNew(async () =>
                    {
                        while (true)
                        {
                            var target = (Func<Task>)null;

                            lock (this.locker)
                            {
                                if (this.tasks.Count == 0)
                                {
                                    this.isExecuting = false;
                                    return;
                                }

                                this.tasks.Dequeue();
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
                lock (this.locker)
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
    }
}
