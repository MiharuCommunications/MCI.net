//-----------------------------------------------------------------------
// <copyright file="FutureAwaiter.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Runtime.CompilerServices;

    public class FutureAwaiter : INotifyCompletion
    {
        private Future future;

        internal FutureAwaiter(Future future)
        {
            this.future = future;
        }

        public bool IsCompleted
        {
            get
            {
                return this.future.FutureTask.IsCompleted;
            }
        }

        public void OnCompleted(Action continuation)
        {
            this.future.FutureTask.ContinueWith(task =>
            {
                continuation();
            });
        }

        public Try GetResult()
        {
            return this.future.FutureTask.Result;
        }
    }

    public class FutureAwaiter<A> : INotifyCompletion
    {
        private Future<A> future;

        internal FutureAwaiter(Future<A> future)
        {
            this.future = future;
        }

        public bool IsCompleted
        {
            get
            {
                return this.future.FutureTask.IsCompleted;
            }
        }

        public void OnCompleted(Action continuation)
        {
            this.future.FutureTask.ContinueWith(task =>
            {
                continuation();
            });
        }

        public Try<A> GetResult()
        {
            return this.future.FutureTask.Result;
        }
    }
}
