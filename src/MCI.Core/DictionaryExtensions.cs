//-----------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, NTValue> Map<TKey, TValue, NTValue>(this Dictionary<TKey, TValue> dict, Func<TValue, NTValue> f)
        {
            var ndict = new Dictionary<TKey, NTValue>();

            foreach (var kv in dict)
            {
                ndict[kv.Key] = f(dict[kv.Key]);
            }

            return ndict;
        }

        public static Dictionary<TKey, TValue> FilterKeys<TKey, TValue>(this Dictionary<TKey, TValue> dict, Func<TKey, bool> f)
        {
            var filterd = new Dictionary<TKey, TValue>();

            foreach (var key in dict.Keys.ToArray())
            {
                if (f(key))
                {
                    filterd[key] = dict[key];
                }
            }

            return filterd;
        }

        public static void Remove<TKey, TValue>(this Dictionary<TKey, TValue> dict, Func<TKey, TValue, bool> f)
        {
            foreach (var key in dict.Keys.ToArray())
            {
                if (dict.ContainsKey(key))
                {
                    if (f(key, dict[key]))
                    {
                        dict.Remove(key);
                    }
                }
            }
        }

        public static Dictionary<TKey, TValue> Copy<TKey, TValue>(this Dictionary<TKey, TValue> original)
        {
            var copy = new Dictionary<TKey, TValue>();
            var keys = original.Keys;

            foreach (var key in keys)
            {
                copy[key] = original[key];
            }

            return copy;
        }
    }
}
