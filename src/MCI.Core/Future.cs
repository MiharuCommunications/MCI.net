//-----------------------------------------------------------------------
// <copyright file="Future.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Threading.Tasks;

    public sealed class Future<A>
    {
        internal Task<Either<IFailedReason, A>> FutureTask { get; private set; }


        internal Future(Task<Either<IFailedReason, A>> task)
        {
            this.FutureTask = task;
        }


        public Future<B> Select<B>(Func<A, B> f)
        {
            return new Future<B>(this.FutureTask.ContinueWith(t =>
            {
                return t.Result.Select(f);
            }));
        }


        public Future<B> SelectMany<B>(Func<A, Future<B>> f)
        {
            // 自身の task で ContinueWith
            // その後 出来た task でも ContinueWith
            Either<IFailedReason, B> result = new Left<IFailedReason, B>(new NotImplementedError());
            var resultTask = new Task<Either<IFailedReason, B>>(() => result);


            this.FutureTask.ContinueWith(t =>
            {
                if (t.Result.IsRight)
                {
                    f(t.Result.Get()).FutureTask.ContinueWith(tb =>
                    {
                        result = tb.Result;
                        resultTask.RunSynchronously();
                    });
                }
                else
                {
                    result = new Left<IFailedReason, B>(t.Result.Left.Get());
                    resultTask.RunSynchronously();
                }
            });

            return new Future<B>(resultTask);
        }

        public Future<C> SelectMany<B, C>(Func<A, Future<B>> f, Func<A, B, C> g)
        {
            Either<IFailedReason, C> result = new Left<IFailedReason, C>(new NotImplementedError());
            var resultTask = new Task<Either<IFailedReason, C>>(() => result);

            this.FutureTask.ContinueWith(t =>
            {
                if (t.Result.IsRight)
                {
                    var x = t.Result.Get();
                    f(x).FutureTask.ContinueWith(t2 =>
                    {
                        if (t2.Result.IsRight)
                        {
                            result = new Right<IFailedReason, C>(g(x, t2.Result.Get()));
                        }
                        else
                        {
                            result = new Left<IFailedReason, C>(t2.Result.Left.Get());
                        }

                        resultTask.RunSynchronously();
                    });
                }
                else
                {
                    result = new Left<IFailedReason, C>(t.Result.Left.Get());
                    resultTask.RunSynchronously();
                }
            });

            return new Future<C>(resultTask);
        }


        public Task<Either<IFailedReason, A>> AsTask()
        {
            return this.FutureTask;
        }


        public Either<IFailedReason, A> Wait()
        {
            return this.FutureTask.Result;
        }

        public A Get()
        {
            return this.FutureTask.Result.Get();
        }

        public bool IsCompleted
        {
            get
            {
                return this.FutureTask.IsCompleted;
            }
        }
    }
}
