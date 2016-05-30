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

        private Queue<AsyncLockerTaskItem> tasks;

        private object locker = new object();

        public AsyncLocker()
        {
            this.disposed = false;
            this.isExecuting = false;
            this.tasks = new Queue<AsyncLockerTaskItem>();
            this.locker = new object();
        }


        public Task WithLock(Func<Task> action)
        {
            var task = new Task(() => { });


            lock (this.locker)
            {
                // キューに入れる
                this.tasks.Enqueue(new AsyncLockerTaskItem(action, task));

                if (!this.isExecuting)
                {
                    this.isExecuting = true;

                    // キューが処理中でなければ、実行する
                    Task.Factory.StartNew(async () =>
                    {
                        while (true)
                        {
                            var item = this.tasks.Dequeue();

                            try
                            {
                                await item.ExecuteAsync();
                            }
                            catch
                            {
                            }

                            lock (this.locker)
                            {
                                if (this.tasks.Count == 0)
                                {
                                    this.isExecuting = false;
                                    return;
                                }
                            }
                        }
                    }).Wait();
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
