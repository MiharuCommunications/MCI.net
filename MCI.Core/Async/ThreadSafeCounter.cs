//-----------------------------------------------------------------------
// <copyright file="ThreadSafeCounter.cs" company="Miharu Communications Inc.">
//     © 2024 Miharu Communications Inc.
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
        private readonly object syncRoot;

        private int counter;

        public ThreadSafeCounter()
        {
            this.syncRoot = new object();
            this.counter = 0;
        }

        public int Counter
        {
            get
            {
                lock (this.syncRoot)
                {
                    return this.counter;
                }
            }
        }

        public int Increment()
        {
            lock (this.syncRoot)
            {
                return ++this.counter;
            }
        }


        public int Decrement()
        {
            lock (this.syncRoot)
            {
                return --this.counter;
            }
        }
    }
}
