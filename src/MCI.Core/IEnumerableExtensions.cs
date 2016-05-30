//-----------------------------------------------------------------------
// <copyright file="IEnumerableExtensions.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class IEnumerableExtensions
    {
        public static IEnumerable<B> Map<A, B>(this IEnumerable<A> collection, Func<A, B> f)
        {
            foreach (var item in collection)
            {
                yield return f(item);
            }
        }


        public static IEnumerable<B> FlatMap<A, B>(this IEnumerable<A> collection, Func<A, IEnumerable<B>> f)
        {
            foreach (var item in collection)
            {
                foreach (var mitem in f(item))
                {
                    yield return mitem;
                }
            }
        }




        public static void ForEach<A>(this IEnumerable<A> collection, Action<A> f)
        {
            foreach (var item in collection)
            {
                f(item);
            }
        }


        public static int IndexOf<A>(this IEnumerable<A> collection, Func<A, bool> f)
        {
            var i = -1;
            foreach (var item in collection)
            {
                checked { i++; }

                if (f(item))
                {
                    return i;
                }
            }

            return -1;
        }



        /// <summary>
        /// 左側からの畳込み
        /// </summary>
        /// <typeparam name="A">畳み込むコレクションに含まれる要素の型</typeparam>
        /// <typeparam name="B">畳み込んだ後の型</typeparam>
        /// <param name="collection">畳み込むコレクション</param>
        /// <param name="start">畳み込みの初期値</param>
        /// <param name="f">畳込みに使用する関数</param>
        /// <returns>畳み込まれた値</returns>
        public static B FoldLeft<A, B>(this IEnumerable<A> collection, B start, Func<B, A, B> f)
        {
            var temp = start;
            foreach (var item in collection)
            {
                temp = f(temp, item);
            }

            return temp;
        }

        /// <summary>
        /// 右側からの畳込み
        /// </summary>
        /// <typeparam name="A">畳み込むコレクションに含まれる要素の型</typeparam>
        /// <typeparam name="B">畳み込んだ後の型</typeparam>
        /// <param name="collection">畳み込むコレクション</param>
        /// <param name="f">畳込みに使用する関数</param>
        /// <param name="start">畳み込みの初期値</param>
        /// <returns>畳み込まれた値</returns>
        public static B FoldRight<A, B>(this IEnumerable<A> collection, Func<B, A, B> f, B start)
        {
            return collection.Reverse().FoldLeft(start, f);
        }


        /// <summary>
        /// 指定要素だけのコレクションに変換します
        /// </summary>
        /// <typeparam name="A">フィルターをかけるコレクションに含まれる要素の型</typeparam>
        /// <param name="collection">フィルターをかけるコレクション</param>
        /// <param name="p">フィルター関数</param>
        /// <returns>フィルター関数が true だった要素のみで出来たコレクション</returns>
        public static IEnumerable<A> Filter<A>(this IEnumerable<A> collection, Func<A, bool> p)
        {
            foreach (var item in collection)
            {
                if (p(item))
                {
                    yield return item;
                }
            }
        }


        public static Option<A> Head<A>(this IEnumerable<A> collection)
        {
            if (collection.Any())
            {
                return Option<A>.Return(collection.First());
            }
            else
            {
                return Option<A>.Fail();
            }
        }


        /// <summary>
        /// コレクションの最後以外を取り出します
        /// </summary>
        /// <typeparam name="A">コレクションの中身の型</typeparam>
        /// <param name="collection">対象となるコレクション</param>
        /// <returns>最後以外の要素</returns>
        public static IEnumerable<A> Init<A>(this IEnumerable<A> collection)
        {
            var max = collection.Count();
            var i = 0;

            foreach (var item in collection)
            {
                checked { i++; }

                if (i == max)
                {
                    yield break;
                }
                else
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// コレクションの先頭以外を取り出します
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IEnumerable<A> Tail<A>(this IEnumerable<A> collection)
        {
            var skipped = false;

            foreach (var item in collection)
            {
                if (skipped)
                {
                    yield return item;
                }
                else
                {
                    skipped = true;
                }
            }
        }

        /// <summary>
        /// コレクションの先頭以外の要素を順次取り出します？
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Tails<T>(this IEnumerable<T> collection)
        {
            var count = collection.Count();

            for (var i = 1; i < count; i++)
            {
                yield return collection.Skip(i);
            }
        }



        /// <summary>
        /// 先頭 n 個分の要素を削除したコレクションを返します。
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="collection"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IEnumerable<A> Drop<A>(this IEnumerable<A> collection, int n)
        {
            var i = -1;
            foreach (var item in collection)
            {
                checked { i++; }

                if (n <= i)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// 末尾 n 個分の要素を削除したコレクションを返します。
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="collection"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IEnumerable<A> DropRight<A>(this IEnumerable<A> collection, int n)
        {
            // 遅延評価感がないな・・・
            var i = 0;
            var max = collection.Count() - n;

            if (max < 1)
            {
                yield break;
            }

            foreach (var item in collection)
            {
                checked { i++; }

                yield return item;

                if (i == max)
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// 先頭から順に、条件を満たしている要素を削除したコレクションを返します。
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="collection"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IEnumerable<A> DropWhile<A>(this IEnumerable<A> collection, Func<A, bool> p)
        {
            var reached = false;

            foreach (var item in collection)
            {
                if (reached)
                {
                    yield return item;
                }
                else
                {
                    if (!p(item))
                    {
                        reached = true;
                        yield return item;
                    }
                }
            }
        }




        /// <summary>
        /// ２つのコレクションを比較します
        /// </summary>
        /// <typeparam name="A">コレクションに含まれる要素の型</typeparam>
        /// <param name="self">比較されるコレクション</param>
        /// <param name="other">比較するコレクション</param>
        /// <returns>内容が同じかどうか</returns>
        public static bool IsSame<A>(this IEnumerable<A> self, IEnumerable<A> other)
        {
            var count = self.Count();
            if (count != other.Count())
            {
                return false;
            }

            for (var i = 0; i < count; i++)
            {
                if (!self.ElementAt(i).Equals(other.ElementAt(i)))
                {
                    return false;
                }
            }

            return true;
        }


        public static decimal? Marge(this IEnumerable<decimal> collection)
        {
            if (collection.Count() < 1)
            {
                return null;
            }

            var first = collection.First();
            if (collection.All(i => i == first))
            {
                return first;
            }
            else
            {
                return null;
            }
        }


        public static decimal? Marge<T>(this IEnumerable<T> collection, Func<T, decimal> f)
        {
            if (collection.Count() < 1)
            {
                return null;
            }

            var first = f(collection.First());
            if (collection.All(i => f(i) == first))
            {
                return first;
            }
            else
            {
                return null;
            }
        }


        public static int? Marge(this IEnumerable<int> collection)
        {
            if (collection.Count() < 1)
            {
                return null;
            }

            var first = collection.First();
            if (collection.All(i => i == first))
            {
                return first;
            }
            else
            {
                return null;
            }
        }


        public static int? Marge<T>(this IEnumerable<T> collection, Func<T, int> f)
        {
            if (collection.Count() < 1)
            {
                return null;
            }

            var first = f(collection.First());
            if (collection.All(i => f(i) == first))
            {
                return first;
            }
            else
            {
                return null;
            }
        }









        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> collection)
        {
            foreach (var i in collection)
            {
                foreach (var j in i)
                {
                    yield return j;
                }
            }
        }


        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<IEnumerable<T>>> collection)
        {
            foreach (var i in collection)
            {
                foreach (var j in i)
                {
                    foreach (var k in j)
                    {
                        yield return k;
                    }
                }
            }
        }


        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<IEnumerable<IEnumerable<T>>>> collection)
        {
            foreach (var i in collection)
            {
                foreach (var j in i)
                {
                    foreach (var k in j)
                    {
                        foreach (var l in k)
                        {
                            yield return l;
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<IEnumerable<IEnumerable<IEnumerable<T>>>>> collection)
        {
            foreach (var i in collection)
            {
                foreach (var j in i)
                {
                    foreach (var k in j)
                    {
                        foreach (var l in k)
                        {
                            foreach (var m in l)
                            {
                                yield return m;
                            }
                        }
                    }
                }
            }
        }




        public static IEnumerable<T> Intersperse<T>(this IEnumerable<T> collection, T item)
        {
            // リストの各要素の間にある要素を挿入(はさみこませる)
            var i = -1;

            foreach (var elem in collection)
            {
                checked { i++; }

                if (0 < i)
                {
                    yield return item;
                }

                yield return elem;
            }
        }


        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source, int times)
        {
            for (var i = 0; i < times; i++)
            {
                foreach (var elem in source)
                {
                    yield return elem;
                }
            }
        }


        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source)
        {
            while (true)
            {
                foreach (var elem in source)
                {
                    yield return elem;
                }
            }
        }

    }
}
