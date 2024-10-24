namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class EitherTaskHelper
    {
        public static Task<Either<IFailedReason, T>> FromTask<T>(Task<T> source, TimeSpan timeout)
        {
            var sync = new object();
            Either<IFailedReason, T> result = new Left<IFailedReason, T>(new NotImplementedError());
            var dest = new Task<Either<IFailedReason, T>>(() => result);

            source.ContinueWith(s =>
            {
                lock (sync)
                {
                    if (dest.IsCompleted)
                    {
                        return;
                    }

                    result = new Right<IFailedReason, T>(s.Result);
                    dest.RunSynchronously();
                }
            });

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (sync)
                {
                    if (dest.IsCompleted)
                    {
                        return;
                    }

                    result = new Left<IFailedReason, T>(new TimeoutError(timeout));
                    dest.RunSynchronously();
                }
            });

            return dest;
        }


        public static Task<Either<IFailedReason, TEventArgs>> FromEvent<THandler, TEventArgs>(Func<Action<TEventArgs>, THandler> taker, Action<THandler> bind, Action<THandler> unbind, TimeSpan timeout)
        {
            var sync = new object();
            Either<IFailedReason, TEventArgs> result = new Left<IFailedReason, TEventArgs>(new NotImplementedError());
            var task = new Task<Either<IFailedReason, TEventArgs>>(() => result);

            THandler handler = default(THandler);
            handler = taker(args =>
            {
                lock (sync)
                {
                    if (task.IsCompleted)
                    {
                        return;
                    }

                    unbind(handler);
                    result = new Right<IFailedReason, TEventArgs>(args);
                    task.RunSynchronously();
                }
            });

            bind(handler);

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (sync)
                {
                    if (task.IsCompleted)
                    {
                        return;
                    }

                    unbind(handler);
                    result = new Left<IFailedReason, TEventArgs>(new TimeoutError(timeout));
                    task.RunSynchronously();
                }
            });

            return task;
        }
    }
}
