namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static Task<Try<A>> WithTimeOut<A>(this Task<Try<A>> task, TimeSpan timeout)
        {
            var result = Try<A>.Fail(new NotImplementedException());
            var resultTask = new Task<Try<A>>(() => result);

            task.ContinueWith(t =>
            {
                lock (resultTask)
                {
                    if (resultTask.IsCompleted)
                    {
                        return;
                    }

                    result = t.Result;
                    resultTask.RunSynchronously();
                }
            });

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (resultTask)
                {
                    if (resultTask.IsCompleted)
                    {
                        return;
                    }

                    result = Try<A>.Fail(new TimeoutException());
                    resultTask.RunSynchronously();
                }
            });

            return resultTask;
        }




        public static Task<Try<A>> WithTimeOut<A>(this Task<A> task, TimeSpan timeout)
        {
            var result = Try<A>.Fail(new NotImplementedException());
            var resultTask = new Task<Try<A>>(() => result);

            task.ContinueWith(t =>
            {
                lock (resultTask)
                {
                    if (resultTask.IsCompleted)
                    {
                        return;
                    }

                    result = Try<A>.Success(t.Result);
                    resultTask.RunSynchronously();
                }
            });

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (resultTask)
                {
                    if (resultTask.IsCompleted)
                    {
                        return;
                    }

                    result = Try<A>.Fail(new TimeoutException());
                    resultTask.RunSynchronously();
                }
            });

            return resultTask;
        }




    }
}
