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
    using Miharu.Errors;
    using Miharu.Maths;

    // http://ufcpp.net/study/algorithm/col_circular.html
    // Buffer Size を 2 の累乗にすれば、高速にできるらしい
    // 求めるサイズより大きなサイズのバッファにする？

    public class RingBuffer<T> : IEnumerable<T>
    {
        private object sync;

        /// <summary>
        /// バッファーの実体
        /// </summary>
        private T[] buffer;

        /// <summary>
        /// バッファー利用領域の開始インデックス
        /// </summary>
        private int top;

        /// <summary>
        /// バッファー利用領域の終了インデックス
        /// </summary>
        private int bottom;

        /// <summary>
        /// 高速処理用のマスク
        /// </summary>
        private int mask;

        /// <summary>
        /// get capacity of this ring buffer
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// <para>バッファーのサイズを与えて RingBuffer を初期化します。</para>
        /// <para>実際には size より大きな容量になります。</para>
        /// </summary>
        /// <param name="size"></param>
        public RingBuffer(int size)
        {
            this.sync = new object();

            var len = RingBuffer.GetCapacity(size + 1);

            this.buffer = new T[len];

            this.top = 0;
            this.bottom = 0;

            this.mask = len - 1;
            this.Capacity = len - 1;
        }

        /// <summary>
        /// <para>指定されたサイズで作成したバッファを指定されたコレクションで初期化して返します。</para>
        /// </summary>
        /// <param name="size"></param>
        /// <param name="source"></param>
        public RingBuffer(int size, T[] source)
        {
            this.sync = new object();

            var len = RingBuffer.GetCapacity(size + 1);

            this.buffer = new T[len];
            Array.Copy(source, this.buffer, source.Length);

            this.top = 0;
            this.bottom = source.Length;

            this.mask = len - 1;
            this.Capacity = len - 1;
        }

        /// <summary>
        /// index を指定して要素を取り出し上書きします。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return this.buffer[(index + this.top) & this.mask];
            }

            set
            {
                // value の範囲を検証すべき
                this.buffer[(index + this.top) & this.mask] = value;
            }
        }

        /// <summary>
        /// 現在のバッファーサイズを返します。
        /// </summary>
        public int Count
        {
            get
            {
                lock (this.sync)
                {
                    var len = this.buffer.Length;

                    return (this.bottom - this.top + len) % len;
                }
            }
        }

        /// <summary>
        ///  バッファーを使い切っているかどうか
        /// </summary>
        public bool IsFull
        {
            get
            {
                lock (this.sync)
                {
                    return this.Count == this.Capacity;
                }
            }
        }


        public Either<IError, Unit> InsertFirst(T item)
        {
            lock (this.sync)
            {
                if (this.IsFull)
                {
                    return new Left<IError, Unit>(new BufferOverflowError(this.Capacity));
                }

                this.top = (this.top - 1) & this.mask;
                this.buffer[this.top] = item;

                return new Right<IError, Unit>(Unit.Instance);
            }
        }

        public Either<IError, Unit> RemoveFirst()
        {
            lock (this.sync)
            {
                this.top = (this.top + 1) & this.mask;

                return new Right<IError, Unit>(Unit.Instance);
            }
        }

        public Either<IError, Unit> RemoveFirst(int length)
        {
            lock (this.sync)
            {
                var count = this.Count;

                if (length == 0)
                {
                    return new Right<IError, Unit>(Unit.Instance);
                }

                if (count < length)
                {
                    return new Left<IError, Unit>(new ArgumentOutOfRangeError("length"));
                }

                if (count == length)
                {
                    this.Clear();

                    return new Right<IError, Unit>(Unit.Instance);
                }

                // 通常の削除
                if (length <= this.buffer.Length - this.top)
                {
                    this.top += length;
                }
                else
                {
                    this.top = length - (this.buffer.Length - this.top);
                }

                return new Right<IError, Unit>(Unit.Instance);
            }
        }


        public Either<IError, Unit> InsertLast(T item)
        {
            lock (this.sync)
            {
                if (this.IsFull)
                {
                    return new Left<IError, Unit>(new BufferOverflowError(this.Capacity));
                }

                this.buffer[this.bottom] = item;
                this.bottom = (this.bottom + 1) & this.mask;

                return new Right<IError, Unit>(Unit.Instance);
            }
        }

        public Either<IError, Unit> InsertLast(T[] items)
        {
            lock (this.sync)
            {
                // TODO : もう少しちゃんとしたい
                for (var i = 0; i < items.Length; i++)
                {
                    var result = this.InsertLast(items[i]);

                    if (result.IsLeft)
                    {
                        return result;
                    }
                }

                return new Right<IError, Unit>(Unit.Instance);
            }
        }


        public Either<IError, Unit> RemoveLast()
        {
            lock (this.sync)
            {
                if (this.Count < 1)
                {
                    return new Left<IError, Unit>(new UnkownError(new NotImplementedException()));
                }

                this.bottom = (this.bottom - 1) & this.mask;

                return new Right<IError, Unit>(Unit.Instance);
            }
        }

        public Either<IError, Unit> RemoveLast(int length)
        {
            lock (this.sync)
            {
                var count = this.Count;

                if (length == 0)
                {
                    return new Right<IError, Unit>(Unit.Instance);
                }

                if (count < length)
                {
                    return new Left<IError, Unit>(new ArgumentOutOfRangeError("length"));
                }

                if (count == length)
                {
                    this.Clear();

                    return new Right<IError, Unit>(Unit.Instance);
                }

                // 通常の削除
                if (length <= this.bottom)
                {
                    this.bottom = this.bottom - length;
                }
                else
                {
                    this.bottom = this.buffer.Length - (length - this.bottom);
                }

                return new Right<IError, Unit>(Unit.Instance);
            }
        }



        public void Clear()
        {
            lock (this.sync)
            {
                this.top = 0;
                this.bottom = 0;
            }
        }



        public IEnumerator<T> GetEnumerator()
        {
            lock (this.sync)
            {
                for (var i = 0; i < this.Count; i++)
                {
                    yield return this[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (this.sync)
            {
                return this.GetEnumerator();
            }
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
