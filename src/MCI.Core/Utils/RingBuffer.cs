//-----------------------------------------------------------------------
// <copyright file="RingBuffer.cs" company="Miharu Communications Inc.">
//     © 2024 Miharu Communications Inc.
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
        private readonly object _sync;

        /// <summary>
        /// バッファーの実体
        /// </summary>
        private readonly T[] _buffer;

        /// <summary>
        /// バッファー利用領域の開始インデックス
        /// </summary>
        private int _top;

        /// <summary>
        /// バッファー利用領域の終了インデックス
        /// </summary>
        private int _bottom;

        /// <summary>
        /// 高速処理用のマスク
        /// </summary>
        private readonly int _mask;

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
            _sync = new object();

            var len = RingBuffer.GetCapacity(size + 1);

            _buffer = new T[len];

            _top = 0;
            _bottom = 0;

            _mask = len - 1;
            Capacity = len - 1;
        }

        /// <summary>
        /// <para>指定されたサイズで作成したバッファを指定されたコレクションで初期化して返します。</para>
        /// </summary>
        /// <param name="size"></param>
        /// <param name="source"></param>
        public RingBuffer(int size, T[] source)
        {
            _sync = new object();

            var len = RingBuffer.GetCapacity(size + 1);

            _buffer = new T[len];
            Array.Copy(source, _buffer, source.Length);

            _top = 0;
            _bottom = source.Length;

            _mask = len - 1;
            Capacity = len - 1;
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
                return _buffer[(index + _top) & _mask];
            }

            set
            {
                // value の範囲を検証すべき
                _buffer[(index + _top) & _mask] = value;
            }
        }

        /// <summary>
        /// 現在のバッファーサイズを返します。
        /// </summary>
        public int Count
        {
            get
            {
                lock (_sync)
                {
                    var len = _buffer.Length;

                    return (_bottom - _top + len) % len;
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
                lock (_sync)
                {
                    return Count == Capacity;
                }
            }
        }


        public Either<IFailedReason, Unit> InsertFirst(T item)
        {
            lock (_sync)
            {
                if (IsFull)
                {
                    return new Left<IFailedReason, Unit>(new BufferOverflowError(Capacity));
                }

                _top = (_top - 1) & _mask;
                _buffer[_top] = item;

                return new Right<IFailedReason, Unit>(Unit.Instance);
            }
        }

        public Either<IFailedReason, Unit> RemoveFirst()
        {
            lock (_sync)
            {
                _top = (_top + 1) & _mask;

                return new Right<IFailedReason, Unit>(Unit.Instance);
            }
        }

        public Either<IFailedReason, Unit> RemoveFirst(int length)
        {
            lock (_sync)
            {
                var count = Count;

                if (length == 0)
                {
                    return new Right<IFailedReason, Unit>(Unit.Instance);
                }

                if (count < length)
                {
                    return new Left<IFailedReason, Unit>(new ArgumentOutOfRangeError("length"));
                }

                if (count == length)
                {
                    Clear();

                    return new Right<IFailedReason, Unit>(Unit.Instance);
                }

                // 通常の削除
                if (length <= _buffer.Length - _top)
                {
                    _top += length;
                }
                else
                {
                    _top = length - (_buffer.Length - _top);
                }

                return new Right<IFailedReason, Unit>(Unit.Instance);
            }
        }


        public Either<IFailedReason, Unit> InsertLast(T item)
        {
            lock (_sync)
            {
                if (IsFull)
                {
                    return new Left<IFailedReason, Unit>(new BufferOverflowError(Capacity));
                }

                _buffer[_bottom] = item;
                _bottom = (_bottom + 1) & _mask;

                return new Right<IFailedReason, Unit>(Unit.Instance);
            }
        }

        public Either<IFailedReason, Unit> InsertLast(T[] items)
        {
            lock (_sync)
            {
                // TODO : もう少しちゃんとしたい
                for (var i = 0; i < items.Length; i++)
                {
                    var result = InsertLast(items[i]);

                    if (result.IsLeft)
                    {
                        return result;
                    }
                }

                return new Right<IFailedReason, Unit>(Unit.Instance);
            }
        }


        public Either<IFailedReason, Unit> RemoveLast()
        {
            lock (_sync)
            {
                if (Count < 1)
                {
                    return new Left<IFailedReason, Unit>(new UnresolvedError(new NotImplementedException()));
                }

                _bottom = (_bottom - 1) & _mask;

                return new Right<IFailedReason, Unit>(Unit.Instance);
            }
        }

        public Either<IFailedReason, Unit> RemoveLast(int length)
        {
            lock (_sync)
            {
                var count = Count;

                if (length == 0)
                {
                    return new Right<IFailedReason, Unit>(Unit.Instance);
                }

                if (count < length)
                {
                    return new Left<IFailedReason, Unit>(new ArgumentOutOfRangeError("length"));
                }

                if (count == length)
                {
                    Clear();

                    return new Right<IFailedReason, Unit>(Unit.Instance);
                }

                // 通常の削除
                if (length <= _bottom)
                {
                    _bottom = _bottom - length;
                }
                else
                {
                    _bottom = _buffer.Length - (length - _bottom);
                }

                return new Right<IFailedReason, Unit>(Unit.Instance);
            }
        }



        public void Clear()
        {
            lock (_sync)
            {
                _top = 0;
                _bottom = 0;
            }
        }



        public IEnumerator<T> GetEnumerator()
        {
            lock (_sync)
            {
                for (var i = 0; i < Count; i++)
                {
                    yield return this[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_sync)
            {
                return GetEnumerator();
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
