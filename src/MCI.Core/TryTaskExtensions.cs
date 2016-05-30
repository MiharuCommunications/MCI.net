//-----------------------------------------------------------------------
// <copyright file="TryTaskExtensions.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class TryTaskExtensions
    {
        public static Task<Try<B>> Map<A, B>(this Task<Try<A>> source, Func<A, B> f)
        {
            return source.ContinueWith(task =>
            {
                return task.Result.Select(f);
            });
        }


        public static Task<Try<B>> FlatMap<A, B>(this Task<Try<A>> source, Func<A, Task<Try<B>>> f)
        {
            var result = Try<B>.Fail(new NotImplementedException());
            var resultTask = new Task<Try<B>>(() => result);

            source.ContinueWith(t =>
            {
                if (t.Result.IsSuccess)
                {
                    f(t.Result.Get()).ContinueWith(tb =>
                    {
                        result = tb.Result;
                        resultTask.RunSynchronously();
                    });
                }
                else
                {
                    result = Try<B>.Fail(t.Result.GetException());
                    resultTask.RunSynchronously();
                }
            });

            return resultTask;
        }


        public static Task<Try<B>> Select<A, B>(this Task<Try<A>> source, Func<A, B> f)
        {
            return source.ContinueWith(task =>
            {
                return task.Result.Select(f);
            });
        }


        public static Task<Try<C>> SelectMany<A, B, C>(this Task<Try<A>> source, Func<A, Task<Try<B>>> f, Func<A, B, C> g)
        {
            var result = Try<C>.Fail(new NotImplementedException());
            var resultTask = new Task<Try<C>>(() => result);

            source.ContinueWith(t =>
            {
                if (t.Result.IsSuccess)
                {
                    var x = t.Result.Get();
                    f(x).ContinueWith(tb =>
                    {
                        if (tb.Result.IsSuccess)
                        {
                            result = Try<C>.Success(g(x, tb.Result.Get()));
                        }
                        else
                        {
                            result = Try<C>.Fail(tb.Result.GetException());
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

            return resultTask;
        }


        public static Task<Try<A>> Where<A>(this Task<Try<A>> source, Func<A, bool> f)
        {
            var result = Try<A>.Fail(new NotImplementedException());
            var resultTask = new Task<Try<A>>(() => result);

            source.ContinueWith(t =>
            {
                result = t.Result.Where(f);
                resultTask.RunSynchronously();
            });

            return resultTask;
        }
    }
}
