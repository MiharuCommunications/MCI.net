//-----------------------------------------------------------------------
// <copyright file="AsyncLocker.cs" company="Miharu Communications Inc.">
//     © 2017 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async
{
    using Miharu.Errors;
    using Miharu.Errors.Async;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AsyncLocker : IDisposable
    {
        private object _sync;

        private bool _disposed;

        private TimeSpan _timeout;

        private bool _isExecuting;

        private Queue<IAsyncLockerQueueItem> _tasks;

        public AsyncLocker(TimeSpan timeout, int capacity)
        {
            _sync = new object();
            _disposed = false;

            _timeout = timeout;
            _isExecuting = false;
            _tasks = new Queue<IAsyncLockerQueueItem>(capacity);
        }


        public Task<Either<Error, T>> LockAsync<T>(Func<Task<Either<Error, T>>> f)
        {
            Either<Error, T> result = new Left<Error, T>(new TaskHasCanceledError());
            var task = new Task<Either<Error, T>>(() => result);

            var item = new AsyncLockerQueueItem<T>(f);

            lock (_sync)
            {
                _tasks.Enqueue(item);

                if (!_isExecuting)
                {
                    _isExecuting = true;

                    Task.Factory.StartNew(async () =>
                    {
                        while (true)
                        {
                            if (_disposed)
                            {
                                return;
                            }

                            IAsyncLockerQueueItem i;

                            lock (_sync)
                            {
                                if (_tasks.Count == 0)
                                {
                                    _isExecuting = false;
                                    return;
                                }

                                i = _tasks.Dequeue();
                            }

                            await i.ExecuteAsync();
                        }
                    });
                }
            }

            Task.Delay(_timeout).ContinueWith(t =>
            {
                item.Timeout(_timeout);
            });

            return item.GetTask();
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

        ~AsyncLocker()
        {
            Dispose();
        }
    }
}
