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
            var result = TryHelper.ReturnNotImplementedException<IEnumerable<A>>();
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
                            task.Start();
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
                            task.Start();
                        }
                    }
                });

            return task;
        }


        public static Task<Try<IEnumerable<A>>> AsTask<A>(this IObservable<A> source, TimeSpan timeout)
        {
            var result = TryHelper.ReturnNotImplementedException<IEnumerable<A>>();
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
                            task.Start();
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
                            task.Start();
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

                        result = TryHelper.ReturnTimeoutException<IEnumerable<A>>("IObservableExtensions.AsTask");
                        task.Start();
                    }
                }
            });

            return task;
        }

    }
}
