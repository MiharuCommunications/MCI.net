//-----------------------------------------------------------------------
// <copyright file="ThreadSafeCounter.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Async
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class ThreadSafeCounter
    {
        private int counter;

        public ThreadSafeCounter()
        {
            this.counter = 0;
        }

        public int Counter
        {
            get
            {
                return this.counter;
            }
        }

        public void Increment()
        {
            Interlocked.Increment(ref this.counter);
        }


        public void Decrement()
        {
            Interlocked.Decrement(ref this.counter);
        }
    }
}
