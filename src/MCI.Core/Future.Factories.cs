//-----------------------------------------------------------------------
// <copyright file="Future.Factories.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using System.Threading.Tasks;

    public partial class Future
    {
        public static Future<A> FromResult<A>(Try<A> result)
        {
            return new Future<A>(Task.FromResult(result));
        }

        public static Future FromResult(Try result)
        {
            return new Future(Task.FromResult(result));
        }


        public static Future<A> FromTask<A>(Task<Try<A>> source)
        {
            return new Future<A>(source);
        }



        public static Future<A> WithDelay<A>(TimeSpan delay, A value)
        {
            return new Future<A>(Task.Delay(delay).ContinueWith(t =>
            {
                return Try<A>.Success(value);
            }));
        }


        public static Future<A> FromExecute<A>(Func<A> f)
        {
            var result = Try<A>.Execute(f);

            return new Future<A>(Task.FromResult(result));
        }



        public static Future<A> FromTask<A>(Task<A> source, TimeSpan timeout)
        {
            var reseult = Try<A>.Fail(new NotImplementedException());
            var dest = new Task<Try<A>>(() => reseult);

            source.ContinueWith(s =>
            {
                lock (dest)
                {
                    if (dest.IsCompleted)
                    {
                        return;
                    }

                    reseult = Try<A>.Success(s.Result);
                    dest.RunSynchronously();
                }

            });

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (dest)
                {
                    if (dest.IsCompleted)
                    {
                        return;
                    }

                    reseult = Try<A>.Fail(new TimeoutException());
                    dest.RunSynchronously();
                }

            });

            return new Future<A>(dest);
        }



        public static Future<TEventArgs> FromEvent<THandler, TEventArgs>(Func<Action<TEventArgs>, THandler> taker, Action<THandler> bind, Action<THandler> unbind, TimeSpan timeout)
        {
            var result = Try<TEventArgs>.Fail(new NotImplementedException());
            var task = new Task<Try<TEventArgs>>(() => result);

            THandler handler = default(THandler);
            handler = taker(args =>
            {
                lock (task)
                {
                    if (task.IsCompleted)
                    {
                        return;
                    }

                    unbind(handler);
                    result = Try<TEventArgs>.Success(args);
                    task.RunSynchronously();
                }
            });

            bind(handler);

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (task)
                {
                    if (task.IsCompleted)
                    {
                        return;
                    }

                    unbind(handler);
                    result = Try<TEventArgs>.Fail(new TimeoutException());
                    task.RunSynchronously();
                }
            });

            return new Future<TEventArgs>(task);
        }

    }
}
