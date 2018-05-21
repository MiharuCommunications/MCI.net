//-----------------------------------------------------------------------
// <copyright file="AsyncLockerQueueItem.cs" company="Miharu Communications Inc.">
//     © 2017 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async
{
    using System;
    using System.Threading.Tasks;

    internal class AsyncLockerQueueItem<T> : IAsyncLockerQueueItem
    {
        private object _sync;

        private bool _hasFinished;

        private Either<IFailedReason, T> _result;

        private Task<Either<IFailedReason, T>> _task;

        private Func<Task<Either<IFailedReason, T>>> _f;

        public AsyncLockerQueueItem(Func<Task<Either<IFailedReason, T>>> f)
        {
            _sync = new object();
            _hasFinished = false;

            _result = new Left<IFailedReason, T>(new TaskHasCanceledError());
            _task = new Task<Either<IFailedReason, T>>(() => _result);

            _f = f;
        }


        public Task<Either<IFailedReason, T>> GetTask()
        {
            return _task;
        }


        public Task ExecuteAsync()
        {
            var task = new Task(() => { });

            _f().ContinueWith(t =>
            {
                lock (_sync)
                {
                    if (_hasFinished)
                    {
                        return;
                    }

                    _hasFinished = true;
                }

                try
                {
                    _result = t.Result;
                }
                catch (Exception ex)
                {
                    _result = new Left<IFailedReason, T>(new UnresolvedError(ex));
                }

                _task.RunSynchronously();

                task.RunSynchronously();
            });

            return task;
        }

        public void Timeout(TimeSpan timeout)
        {
            lock (_sync)
            {
                if (_hasFinished)
                {
                    return;
                }

                _hasFinished = true;
            }

            _result = new Left<IFailedReason, T>(new TimeoutError(timeout));

            _task.RunSynchronously();
        }
    }
}
