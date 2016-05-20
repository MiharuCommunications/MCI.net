//-----------------------------------------------------------------------
// <copyright file="IObservableExtensions.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class IObservableExtensions
    {
        public static Task<Try<IEnumerable<A>>> AsTask<A>(this IObservable<A> source)
        {
            var result = Try<IEnumerable<A>>.Fail(new NotImplementedException());
            var task = new Task<Try<IEnumerable<A>>>(() => result);
            var collection = new List<A>();

            IDisposable killer = null;

            killer = source.Subscribe(
                value =>
                {
                    lock (task)
                    {
                        if (!task.IsCompleted)
                        {
                            collection.Add(value);
                        }
                    }
                },
                ex =>
                {
                    lock (task)
                    {
                        if (!task.IsCompleted)
                        {
                            result = Try<IEnumerable<A>>.Fail(ex);
                            task.RunSynchronously();
                        }
                    }
                },
                () =>
                {
                    lock (task)
                    {
                        if (!task.IsCompleted)
                        {
                            result = Try<IEnumerable<A>>.Success(collection);
                            task.RunSynchronously();
                        }
                    }
                });

            return task;
        }


        public static Task<Try<IEnumerable<A>>> AsTask<A>(this IObservable<A> source, TimeSpan timeout)
        {
            var result = Try<IEnumerable<A>>.Fail(new NotImplementedException());
            var task = new Task<Try<IEnumerable<A>>>(() => result);
            var collection = new List<A>();

            IDisposable killer = null;

            killer = source.Subscribe(
                value =>
                {
                    lock (task)
                    {
                        if (!task.IsCompleted)
                        {
                            collection.Add(value);
                        }
                    }
                },
                ex =>
                {
                    lock (task)
                    {
                        if (!task.IsCompleted)
                        {
                            result = Try<IEnumerable<A>>.Fail(ex);
                            task.RunSynchronously();
                        }
                    }
                },
                () =>
                {
                    lock (task)
                    {
                        if (!task.IsCompleted)
                        {
                            result = Try<IEnumerable<A>>.Success(collection);
                            task.RunSynchronously();
                        }
                    }
                });

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (task)
                {
                    if (!task.IsCompleted)
                    {
                        killer.Dispose();

                        result = Try<IEnumerable<A>>.Fail(new TimeoutException());
                        task.RunSynchronously();
                    }
                }
            });

            return task;
        }

    }
}
