//-----------------------------------------------------------------------
// <copyright file="FutureExtensions.cs" company="Miharu Communications Inc.">
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

    public static class FutureExtensions
    {
        public static Future<B> Select<A, B>(this Future<A> source, Func<A, B> f)
        {
            return new Future<B>(source.FutureTask.ContinueWith(t =>
            {
                return t.Result.Select(f);
            }));
        }

        public static Future<C> SelectMany<A, B, C>(this Future<A> source, Func<A, Future<B>> f, Func<A, B, C> g)
        {
            var result = Try<C>.Fail(new NotImplementedException());
            var resultTask = new Task<Try<C>>(() => result);

            source.FutureTask.ContinueWith(t =>
            {
                if (t.Result.IsSuccess)
                {
                    var x = t.Result.Get();
                    f(x).FutureTask.ContinueWith(t2 =>
                    {
                        if (t2.Result.IsSuccess)
                        {
                            result = Try<C>.Success(g(x, t2.Result.Get()));
                        }
                        else
                        {
                            result = Try<C>.Fail(t2.Result.GetException());
                        }

                        resultTask.RunSynchronously();
                    });
                }
                else
                {
                    result = Try<C>.Fail(t.Result.GetException());
                    resultTask.RunSynchronously();
                }
            });

            return new Future<C>(resultTask);
        }

        public static Future<A> Where<A>(this Future<A> source, Func<A, bool> f)
        {
            var result = Try<A>.Fail(new NotImplementedException());
            var task = new Task<Try<A>>(() => result);

            source.FutureTask.ContinueWith(t =>
            {
                result = t.Result.Where(f);
                task.RunSynchronously();
            });

            return new Future<A>(task);
        }

        public static FutureAwaiter<A> GetAwaiter<A>(this Future<A> future)
        {
            return new FutureAwaiter<A>(future);
        }

        public static FutureAwaiter GetAwaiter(this Future future)
        {
            return new FutureAwaiter(future);
        }

        /*
         * 欲しい機能
         *
         *
         *
         *
         *
         *
         *
         *
         *
        **/

        public static Future ForEachInOrderToFailure<A>(this IEnumerable<A> collection, Func<A, Future> f)
        {


            throw new NotImplementedException();
        }


        public static Future ForEach<A>(this IEnumerable<A> collection, Func<A, Future> f)
        {
            throw new NotImplementedException();
        }


        public static Future ForEachToFailure<A>(this IEnumerable<A> collection, Func<A, Future> f)
        {
            throw new NotImplementedException();
        }
    }
}
