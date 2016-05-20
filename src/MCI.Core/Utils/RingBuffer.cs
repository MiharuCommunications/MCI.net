//-----------------------------------------------------------------------
// <copyright file="RingBuffer.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Miharu.Maths;

    // http://ufcpp.net/study/algorithm/col_circular.html
    // Buffer Size を 2 の累乗にすれば、高速にできるらしい
    // 求めるサイズより大きなサイズのバッファにする？


    public class RingBuffer<T> : IEnumerable<T>
    {
        private T[] buffer;
        private int top;
        private int bottom;
        private int mask;


        public int Capacity { get; private set; }

        public RingBuffer(int size)
        {
            var len = RingBuffer.GetCapacity(size + 1);

            this.buffer = new T[len];

            this.top = 0;
            this.bottom = 0;

            this.mask = len - 1;
            this.Capacity = len - 1;
        }


        public T this[int index]
        {
            get
            {
                return this.buffer[(index + this.top) & this.mask];
            }

            set
            {
                this.buffer[(index + this.top) & this.mask] = value;
            }
        }


        public int Count
        {
            get
            {
                var c = this.bottom - this.top;

                if (c < 0)
                {
                    return c + this.buffer.Length;
                }
                else
                {
                    return c;
                }
            }
        }

        public bool IsFull
        {
            get
            {
                return this.Count == this.Capacity;
            }
        }


        public void InsertFirst(T item)
        {
            if (this.IsFull)
            {
                throw new OverflowException();
            }

            this.top = (this.top - 1) & this.mask;
            this.buffer[this.top] = item;
        }

        public void RemoveFirst()
        {
            this.top = (this.top + 1) & this.mask;
        }


        public void InsertLast(T item)
        {
            if (this.IsFull)
            {
                throw new OverflowException();
            }

            this.buffer[this.bottom] = item;
            this.bottom = (this.bottom + 1) & this.mask;
        }


        public void RemoveLast()
        {
            this.bottom = (this.bottom - 1) & this.mask;
        }



        public void Clear()
        {
            this.top = 0;
            this.bottom = 0;
        }



        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < this.Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }




    internal static class RingBuffer
    {
        public static int GetCapacity(int size)
        {
            foreach (var c in Sequences.Pow2s)
            {
                if (size <= c)
                {
                    return c;
                }
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
