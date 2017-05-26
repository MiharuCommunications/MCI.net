namespace Miharu.Async
{
    using Miharu.Errors;
    using Miharu.Errors.Async;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class AsyncLockerQueueItem<T> : IAsyncLockerQueueItem
    {
        private object _sync;

        private bool _hasFinished;

        private Either<Error, T> _result;

        private Task<Either<Error, T>> _task;

        private Func<Task<Either<Error, T>>> _f;

        public AsyncLockerQueueItem(Func<Task<Either<Error, T>>> f)
        {
            _sync = new object();
            _hasFinished = false;

            _result = new Left<Error, T>(new TaskHasCanceledError());
            _task = new Task<Either<Error, T>>(() => _result);

            _f = f;
        }


        public Task<Either<Error, T>> GetTask()
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
                    _result = new Left<Error, T>(new UnresolvedError(ex));
                }

                _task.Start();

                task.Start();
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

            _result = new Left<Error, T>(new TimeoutError(timeout));

            _task.Start();
        }
    }
}
