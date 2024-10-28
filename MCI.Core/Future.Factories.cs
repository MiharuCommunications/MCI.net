//-----------------------------------------------------------------------
// <copyright file="Future.Factories.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Threading.Tasks;

    public partial class Future
    {
        public static Future<A> FromFailedReason<A>(IFailedReason reason)
        {
            return new Future<A>(Task.FromResult(Either.ToLeft<IFailedReason, A>(reason)));
        }

        public static Future<A> FromResult<A>(A result)
        {
            return new Future<A>(Task.FromResult(Either.ToRight<IFailedReason, A>(result)));
        }


        public static Future<A> FromTask<A>(Task<Either<IFailedReason, A>> source)
        {
            return new Future<A>(source);
        }



        public static Future<A> WithDelay<A>(TimeSpan delay, A value)
        {
            return new Future<A>(Task.Delay(delay).ContinueWith(t =>
            {
                return (Either<IFailedReason, A>)new Right<IFailedReason, A>(value);
            }));
        }


        public static Future<A> FromExecute<A>(Func<A> f)
        {
            var result = Either.ToLeft<IFailedReason, A>(new NotImplementedError());

            try
            {
                result = Either.ToRight<IFailedReason, A>(f());
            }
            catch (Exception ex)
            {
                result = Either.ToLeft<IFailedReason, A>(new UnresolvedError(ex));
            }

            return new Future<A>(Task.FromResult(result));
        }



        public static Future<A> FromTask<A>(Task<A> source, TimeSpan timeout)
        {
            Either<IFailedReason, A> reseult = new Left<IFailedReason, A>(new NotImplementedError());
            var dest = new Task<Either<IFailedReason, A>>(() => reseult);

            source.ContinueWith(s =>
            {
                lock (dest)
                {
                    if (!dest.IsCompleted)
                    {
                        reseult = new Right<IFailedReason, A>(s.Result);
                        dest.RunSynchronously();
                    }
                }

            });

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (dest)
                {
                    if (!dest.IsCompleted)
                    {
                        reseult = new Left<IFailedReason, A>(new TimeoutError(timeout));
                        dest.RunSynchronously();
                    }
                }

            });

            return new Future<A>(dest);
        }



        public static Future<TEventArgs> FromEvent<THandler, TEventArgs>(Func<Action<TEventArgs>, THandler> taker, Action<THandler> bind, Action<THandler> unbind, TimeSpan timeout)
        {
            var result = Either<IFailedReason, TEventArgs>.ToLeft(new NotImplementedError());
            var task = new Task<Either<IFailedReason, TEventArgs>>(() => result);

            THandler handler = default(THandler);
            handler = taker(args =>
            {
                lock (task)
                {
                    if (!task.IsCompleted)
                    {
                        unbind(handler);
                        result = Either.ToRight<IFailedReason, TEventArgs>(args);
                        task.RunSynchronously();
                    }
                }
            });

            bind(handler);

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (task)
                {
                    if (!task.IsCompleted)
                    {
                        unbind(handler);
                        result = Either.ToLeft<IFailedReason, TEventArgs>(new TimeoutError(timeout));
                        task.RunSynchronously();
                    }
                }
            });

            return new Future<TEventArgs>(task);
        }

    }
}
