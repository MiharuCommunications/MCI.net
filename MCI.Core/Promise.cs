namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class Promise<T>
    {
        private object _sync;
        private Either<IFailedReason, T> _result;
        private Task<Either<IFailedReason, T>> _task;

        public Promise()
        {
            this._sync = new object();
            this._result = Either<IFailedReason, T>.ToLeft(new NotImplementedError());
            this._task = new Task<Either<IFailedReason, T>>(() => this._result);
        }


        public Future<T> GetFuture()
        {
            return new Future<T>(this._task);
        }

        public bool IsCompleted
        {
            get
            {
                return this._task.IsCompleted;
            }
        }

        public bool TryComplete(Either<IFailedReason, T> result)
        {
            lock (this._sync)
            {
                if (_task.IsCompleted)
                {
                    return false;
                }

                this._result = result;
                this._task.Start();
                // this._task.RunSynchronously();

                return true;
            }
        }

        public bool TryFailure(IFailedReason reason)
        {
            lock (this._sync)
            {
                if (this._task.IsCompleted)
                {
                    return false;
                }

                this._result = Either.ToLeft<IFailedReason, T>(reason);
                this._task.Start();

                return true;
            }
        }

        public bool TrySuccess(T value)
        {
            lock (this._sync)
            {
                if (this._task.IsCompleted)
                {
                    return false;
                }

                this._result = Either.ToRight<IFailedReason, T>(value);
                this._task.Start();

                return true;
            }
        }
    }
}
