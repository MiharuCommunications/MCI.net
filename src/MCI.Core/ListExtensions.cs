//-----------------------------------------------------------------------
// <copyright file="ListExtensions.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;

    public static class ListExtensions
    {
        public static List<B> Map<A, B>(this List<A> collection, Func<A, B> f)
        {
            var result = new List<B>(collection.Count);
            foreach (var item in collection)
            {
                result.Add(f(item));
            }

            return result;
        }

        public static List<B> FlatMap<A, B>(this List<A> collection, Func<A, List<B>> f)
        {
            var result = new List<B>();
            foreach (var item in collection)
            {
                result.AddRange(f(item));
            }

            return result;
        }


        public static void ForEach<A>(this List<A> collection, Action<A> f)
        {
            foreach (var item in collection)
            {
                f(item);
            }
        }




        public static B FoldLeft<A, B>(this List<A> collection, B start, Func<B, A, B> f)
        {
            var temp = start;
            foreach (var item in collection)
            {
                temp = f(temp, item);
            }

            return temp;
        }


        public static B FoldRight<A, B>(this List<A> collection, Func<A, B, B> f, B start)
        {
            var temp = start;
            for (var i = collection.Count - 1; 0 <= i; i--)
            {
                temp = f(collection[i], temp);
            }

            return temp;
        }



    }
}
