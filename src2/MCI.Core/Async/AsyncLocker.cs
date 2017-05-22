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
        private bool _isExecuting;

        private bool _disposed;

        private readonly Queue<Func<Task>> _tasks;

        private readonly object _sync;

        public AsyncLocker()
        {
            _disposed = false;
            _tasks = new Queue<Func<Task>>();
            _isExecuting = false;
            _sync = new object();
        }


        public Task<Try> WithLock(Func<Task<Try>> action)
        {
            var result = Try.Fail(new TimeoutException());
            var task = new Task<Try>(() => result);


            lock (_sync)
            {
                // キューに入れる
                _tasks.Enqueue(async () =>
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

                if (!_isExecuting)
                {
                    _isExecuting = true;

                    // キューが処理中でなければ、実行する
                    Task.Factory.StartNew(async () =>
                    {
                        while (true)
                        {
                            Func<Task> target;

                            lock (_sync)
                            {
                                if (_tasks.Count == 0)
                                {
                                    _isExecuting = false;
                                    return;
                                }

                                target = _tasks.Dequeue();
                            }

                            try
                            {
                                await target();
                            }
                            catch
                            {
                                // ignored
                            }
                        }
                    });
                }
            }

            return task;
        }




        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose 処理
                lock (_sync)
                {
                    _tasks.Clear();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
