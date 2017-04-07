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
        public static Task<Either<IError, T>> FromTask<T>(Task<T> source, TimeSpan timeout)
        {
            var sync = new object();
            Either<IError, T> result = new Left<IError, T>(new NotImplementedError());
            var dest = new Task<Either<IError, T>>(() => result);

            source.ContinueWith(s =>
            {
                lock (sync)
                {
                    if (dest.IsCompleted)
                    {
                        return;
                    }

                    result = new Right<IError, T>(s.Result);
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

                    result = new Left<IError, T>(new TimeoutError(timeout));
                    dest.RunSynchronously();
                }
            });

            return dest;
        }


        public static Task<Either<IError, TEventArgs>> FromEvent<THandler, TEventArgs>(Func<Action<TEventArgs>, THandler> taker, Action<THandler> bind, Action<THandler> unbind, TimeSpan timeout)
        {
            var sync = new object();
            Either<IError, TEventArgs> result = new Left<IError, TEventArgs>(new NotImplementedError());
            var task = new Task<Either<IError, TEventArgs>>(() => result);

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
                    result = new Right<IError, TEventArgs>(args);
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
                    result = new Left<IError, TEventArgs>(new TimeoutError(timeout));
                    task.RunSynchronously();
                }
            });

            return task;
        }
    }
}
