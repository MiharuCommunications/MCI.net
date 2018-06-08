//-----------------------------------------------------------------------
// <copyright file="FutureAwaiter.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Runtime.CompilerServices;

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

        public Either<IFailedReason, A> GetResult()
        {
            return this.future.FutureTask.Result;
        }
    }
}
