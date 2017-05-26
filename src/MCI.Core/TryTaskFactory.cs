//-----------------------------------------------------------------------
// <copyright file="TryTaskFactory.cs" company="Miharu Communications Inc.">
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

    public static class TryTaskFactory
    {
        public static Task<Try<A>> FromTask<A>(Task<A> source, TimeSpan timeout)
        {
            var reseult = TryHelper.ReturnNotImplementedException<A>();
            var dest = new Task<Try<A>>(() => reseult);

            source.ContinueWith(s =>
            {
                lock (dest)
                {
                    if (!dest.IsCompleted)
                    {
                        reseult = Try<A>.Success(s.Result);
                        dest.Start();
                    }
                }
            });

            Task.Delay(timeout).ContinueWith(t =>
            {
                lock (dest)
                {
                    if (!dest.IsCompleted)
                    {
                        reseult = TryHelper.ReturnTimeoutException<A>("TryTaskFactory.FromTask");
                        dest.Start();
                    }
                }
            });

            return dest;
        }


        public static Task<Try<TEventArgs>> FromEvent<THandler, TEventArgs>(Func<Action<TEventArgs>, THandler> taker, Action<THandler> bind, Action<THandler> unbind, TimeSpan timeout)
        {
            var result = TryHelper.ReturnNotImplementedException<TEventArgs>();
            var task = new Task<Try<TEventArgs>>(() => result);

            THandler handler = default(THandler);
            handler = taker(args =>
            {
                lock (task)
                {
                    if (!task.IsCompleted)
                    {
                        unbind(handler);
                        result = Try<TEventArgs>.Success(args);
                        task.Start();
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
                        result = TryHelper.ReturnTimeoutException<TEventArgs>("TryTaskFactory.FromEvent");
                        task.Start();
                    }
                }
            });

            return task;
        }
    }
}
