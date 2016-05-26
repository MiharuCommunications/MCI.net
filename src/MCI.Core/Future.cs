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
        internal Task<Try<A>> FutureTask { get; private set; }


        internal Future(Task<Try<A>> task)
        {
            this.FutureTask = task;
        }


        public Future<B> Map<B>(Func<A, B> f)
        {
            return new Future<B>(this.FutureTask.ContinueWith(t =>
            {
                return t.Result.Select(f);
            }));
        }


        public Future<B> FlatMap<B>(Func<A, Future<B>> f)
        {
            // 自身の task で ContinueWith
            // その後 出来た task でも ContinueWith
            var result = Try<B>.Fail(new NotImplementedException());
            var resultTask = new Task<Try<B>>(() => result);


            this.FutureTask.ContinueWith(t =>
            {
                if (t.Result.IsSuccess)
                {
                    // Map した Task
                    f(t.Result.Get()).FutureTask.ContinueWith(tb =>
                    {
                        result = tb.Result;
                        resultTask.RunSynchronously();
                    });
                }
                else
                {
                    // そのまま？
                    result = Try<B>.Fail(t.Result.GetException());
                    resultTask.RunSynchronously();
                }
            });

            return new Future<B>(resultTask);
        }

        public Task<Try<A>> AsTask()
        {
            return this.FutureTask;
        }


        public Try<A> Wait()
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


    public partial class Future
    {
        internal Task<Try> FutureTask { get; private set; }

        internal Future(Task<Try> task)
        {
            this.FutureTask = task;
        }


        public Future Map(Action f)
        {
            return new Future(this.FutureTask.ContinueWith(task =>
            {
                return task.Result.Select(f);
            }));
        }


        public Future FlatMap(Func<Future> f)
        {
            var result = Try.Fail(new NotImplementedException());
            var resultTask = new Task<Try>(() => result);


            this.FutureTask.ContinueWith(t =>
            {
                if (t.Result.IsSuccess)
                {
                    f().FutureTask.ContinueWith(tb =>
                    {
                        result = tb.Result;
                        resultTask.RunSynchronously();
                    });
                }
                else
                {
                    result = Try.Fail(t.Result.GetException());
                    resultTask.RunSynchronously();
                }
            });

            return new Future(resultTask);
        }

        public Task<Try> AsTask()
        {
            return this.FutureTask;
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
