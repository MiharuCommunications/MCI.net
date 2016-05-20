//-----------------------------------------------------------------------
// <copyright file="ArrayExtensions.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;

    // http://hackage.haskell.org/package/base-4.7.0.1/docs/Data-List.html

    /// <summary>
    /// 配列に対する拡張メソッド
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 配列の単射
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="collection"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static B[] Map<A, B>(this A[] collection, Func<A, B> f)
        {
            var array = new B[collection.Length];

            for (var i = 0; i < collection.Length; i++)
            {
                array[i] = f(collection[i]);
            }

            return array;
        }


        public static B[] FlatMap<A, B>(this A[] collection, Func<A, B[]> f)
        {
            var result = new List<B>();

            for (var i = 0; i < collection.Length; i++)
            {
                result.AddRange(f(collection[i]));
            }

            return result.ToArray();
        }

        public static void ForEach<A>(this A[] collection, Action<A> f)
        {
            for (var i = 0; i < collection.Length; i++)
            {
                f(collection[i]);
            }
        }


        public static B FoldLeft<A, B>(this A[] collection, B start, Func<B, A, B> f)
        {
            var temp = start;
            for (var i = 0; i < collection.Length; i++)
            {
                temp = f(temp, collection[i]);
            }

            return temp;
        }

        public static B FoldRight<A, B>(this A[] collection, Func<A, B, B> f, B start)
        {
            var temp = start;
            for (var i = collection.Length - 1; 0 <= i; i--)
            {
                temp = f(collection[i], temp);
            }

            return temp;
        }

        // あまりこのメソッド良くない（null返してるようなもんだし）
        public static int IndexOf<A>(this A[] collection, Func<A, bool> f)
        {
            for (var i = 0; i < collection.Length; i++)
            {
                if (f(collection[i]))
                {
                    return i;
                }
            }

            return -1;
        }



        public static Option<A> Head<A>(this A[] collection)
        {
            if (collection.Length == 0)
            {
                return Option<A>.Fail();
            }
            else
            {
                return Option<A>.Return(collection[0]);
            }
        }

        public static Option<A> Last<A>(this A[] collection)
        {
            if (collection.Length == 0)
            {
                return Option<A>.Fail();
            }
            else
            {
                return Option<A>.Return(collection[collection.Length - 1]);
            }
        }

        public static A[] Tail<A>(this A[] collection)
        {
            if (collection.Length <= 1)
            {
                return new A[0];
            }

            var resultLength = collection.Length - 1;
            var result = new A[resultLength];
            for (var i = 0; i < resultLength; i++)
            {
                result[i] = collection[i + 1];
            }

            return result;
        }


        public static A[] Init<A>(this A[] collection)
        {
            if (collection.Length <= 1)
            {
                return new A[0];
            }

            var resultLength = collection.Length - 1;
            var result = new A[resultLength];
            for (var i = 0; i < resultLength; i++)
            {
                result[i] = collection[i];
            }

            return result;
        }


        public static A[] Drop<A>(this A[] collection, int n)
        {
            if (collection.Length <= n)
            {
                return new A[0];
            }

            var resultLength = collection.Length - n;
            var result = new A[resultLength];

            for (var i = 0; i < resultLength; i++)
            {
                result[i] = collection[n + i];
            }

            return result;
        }

        public static A[] DropRight<A>(this A[] collection, int n)
        {
            if (collection.Length <= n)
            {
                return new A[0];
            }

            var resultLength = collection.Length - n;
            var result = new A[resultLength];

            for (var i = 0; i < resultLength; i++)
            {
                result[i] = collection[i];
            }

            return result;
        }


        public static Tuple<A[], A[]> SplitAt<A>(this A[] collection, int n)
        {
            if (n <= 0)
            {
                return new Tuple<A[], A[]>(new A[0], collection);
            }

            if (collection.Length <= n)
            {
                return new Tuple<A[], A[]>(collection, new A[0]);
            }

            var front = new A[n];
            for (var i = 0; i < n; i++)
            {
                front[i] = collection[i];
            }

            var back = new A[collection.Length - n];
            for (var i = 0; i < back.Length; i++)
            {
                back[i] = collection[n + i - 1];
            }

            return new Tuple<A[], A[]>(front, back);
        }


        public static Tuple<A[], A[]> Span<A>(this A[] collection, Func<A, bool> f)
        {
            for (var i = 0; i < collection.Length; i++)
            {
                if (!f(collection[i]))
                {
                    return collection.SplitAt(i);
                }
            }

            return new Tuple<A[], A[]>(collection, new A[0]);
        }


        public static Dictionary<int, A> ToDictionary<A>(this A[] collection)
        {
            var dict = new Dictionary<int, A>();

            for (var i = 0; i < collection.Length; i++)
            {
                dict[i] = collection[i];
            }

            return dict;
        }

        public static A[][] Divide<A>(this A[] collection, int length)
        {
            var resultLength = collection.Length % length == 0 ? collection.Length / length : collection.Length / length + 1;
            var result = new A[resultLength][];

            for (var i = 0; i < collection.Length; i++)
            {
                if (i % length == 0)
                {
                    if (collection.Length - i < length)
                    {
                        result[i / length] = new A[collection.Length - i];
                    }
                    else
                    {
                        result[i / length] = new A[length];
                    }
                }

                result[i / length][i % length] = collection[i];
            }

            return result;
        }


        public static bool IsSame<A>(this A[] self, A[] other)
        {
            if (self.Length != other.Length)
            {
                return false;
            }

            var len = self.Length;
            for (var i = 0; i < len; i++)
            {
                if (!self[i].Equals(other[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
