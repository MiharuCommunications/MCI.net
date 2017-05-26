namespace Miharu
{
    using System;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static Task<Try<A>> WithTimeout<A>(this Task<Try<A>> task, TimeSpan timeout)
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
                    resultTask.Start();
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
                    resultTask.Start();
                }
            });

            return resultTask;
        }




        public static Task<Try<A>> WithTimeout<A>(this Task<A> task, TimeSpan timeout)
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
                    resultTask.Start();
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
                    resultTask.Start();
                }
            });

            return resultTask;
        }
    }
}
