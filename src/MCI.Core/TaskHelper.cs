namespace Miharu
{
    using Miharu.Errors;
    using System;
    using System.Threading.Tasks;

    public static partial class TaskHelper
    {
        public static Task<Tuple<T1, T2>> AwaitAll<T1, T2>(Task<T1> task1, Task<T2> task2)
        {
            var sync = new object();
            Tuple<T1, T2> result = null;
            var task = new Task<Tuple<T1, T2>>(() => result);
            var hasFinished = false;

            task1.ContinueWith(t1 =>
            {
                lock (sync)
                {
                    if (task1.IsCompleted && task2.IsCompleted)
                    {
                        if (!hasFinished)
                        {
                            hasFinished = true;

                            result = Tuple.Create(task1.Result, task2.Result);
                            task.Start();
                        }
                    }
                }
            });

            task2.ContinueWith(t2 =>
            {
                lock (sync)
                {
                    if (task1.IsCompleted && task2.IsCompleted)
                    {
                        if (!hasFinished)
                        {
                            hasFinished = true;

                            result = Tuple.Create(task1.Result, task2.Result);
                            task.Start();
                        }
                    }
                }
            });

            return task;
        }


        public static Task<Either<Error, Tuple<T1, T2>>> AwaitAll<T1, T2>(Task<T1> task1, Task<T2> task2, TimeSpan timeout)
        {
            var sync = new object();
            Either<Error, Tuple<T1, T2>> result = null;
            var task = new Task<Either<Error, Tuple<T1, T2>>>(() => result);
            var hasFinished = false;

            task1.ContinueWith(t1 =>
            {
                lock (sync)
                {
                    if (task1.IsCompleted && task2.IsCompleted)
                    {
                        if (!hasFinished)
                        {
                            hasFinished = true;

                            result = new Right<Error, Tuple<T1, T2>>(Tuple.Create(task1.Result, task2.Result));
                            task.Start();
                        }
                    }
                }
            });

            task2.ContinueWith(t2 =>
            {
                lock (sync)
                {
                    if (task1.IsCompleted && task2.IsCompleted)
                    {
                        if (!hasFinished)
                        {
                            hasFinished = true;

                            result = new Right<Error, Tuple<T1, T2>>(Tuple.Create(task1.Result, task2.Result));
                            task.Start();
                        }
                    }
                }
            });

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (sync)
                {
                    if (!hasFinished)
                    {
                        hasFinished = true;

                        result = new Left<Error, Tuple<T1, T2>>(new TimeoutError(timeout));
                        task.Start();
                    }
                }
            });

            return task;
        }
    }
}
