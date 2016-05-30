//-----------------------------------------------------------------------
// <copyright file="IDictionaryExtensions.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IDictionaryExtensions
    {
        public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dict, Func<TKey, TValue, bool> f)
        {
            foreach (var key in dict.Keys.ToArray())
            {
                if (f(key, dict[key]))
                {
                    dict.Remove(key);
                }
            }
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue def)
        {
            TValue result;

            if (dict.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                return def;
            }
        }

        public static Option<TValue> Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue result;

            if (dict.TryGetValue(key, out result))
            {
                return Option<TValue>.Return(result);
            }
            else
            {
                return Option<TValue>.Fail();
            }
        }


        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dict, Action<TKey, TValue> action)
        {
            foreach (var key in dict.Keys.ToArray())
            {
                TValue value;

                if (dict.TryGetValue(key, out value))
                {
                    action(key, value);
                }
            }
        }
    }
}
