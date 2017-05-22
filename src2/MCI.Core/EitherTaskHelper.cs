namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Miharu.Errors;

    public static class EitherTaskHelper
    {
        public static Task<Either<Error, T>> FromTask<T>(Task<T> source, TimeSpan timeout)
        {
            var sync = new object();
            Either<Error, T> result = new Left<Error, T>(new NotImplementedError());
            var dest = new Task<Either<Error, T>>(() => result);

            source.ContinueWith(s =>
            {
                lock (sync)
                {
                    if (dest.IsCompleted)
                    {
                        return;
                    }

                    result = new Right<Error, T>(s.Result);
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

                    result = new Left<Error, T>(new TimeoutError(timeout));
                    dest.RunSynchronously();
                }
            });

            return dest;
        }


        public static Task<Either<Error, TEventArgs>> FromEvent<THandler, TEventArgs>(Func<Action<TEventArgs>, THandler> taker, Action<THandler> bind, Action<THandler> unbind, TimeSpan timeout)
        {
            var sync = new object();
            Either<Error, TEventArgs> result = new Left<Error, TEventArgs>(new NotImplementedError());
            var task = new Task<Either<Error, TEventArgs>>(() => result);

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
                    result = new Right<Error, TEventArgs>(args);
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
                    result = new Left<Error, TEventArgs>(new TimeoutError(timeout));
                    task.RunSynchronously();
                }
            });

            return task;
        }
    }
}
