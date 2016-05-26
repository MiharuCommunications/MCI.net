//-----------------------------------------------------------------------
// <copyright file="OptionExtensions.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class OptionExtensions
    {
        // https://github.com/LangExt/LangExt/blob/master/LangExt/Option.QueryExpr.cs
        // を参考にしました。

        /*
        public static Option<B> Select<A, B>(this Option<A> opt, Func<A, B> f)
        {
            if (opt.IsDefined)
            {
                return new Some<B>(f(opt.Get()));
            }
            else
            {
                return new None<B>();
            }
        }


        public static Option<C> SelectMany<A, B, C>(this Option<A> opt, Func<A, Option<B>> f, Func<A, B, C> g)
        {
            if (opt.IsDefined)
            {
                var x = opt.Get();

                return f(x).FlatMap(y => new Some<C>(g(x, y)));
            }
            else
            {
                return new None<C>();
            }
        }
        */


        public static Option<A> Where<A>(this Option<A> opt, Func<A, bool> f)
        {
            if (opt.IsDefined)
            {
                var value = opt.Get();

                if (f(value))
                {
                    return new Some<A>(value);
                }
            }

            return new None<A>();
        }


        public static void ForEach<A>(this Option<A> opt, Action<A> f)
        {
            if (opt.IsDefined)
            {
                f(opt.Get());
            }
        }

        public static Try<A> ToTry<A>(this Option<A> opt, Exception ex)
        {
            if (opt.IsDefined)
            {
                return Try<A>.Success(opt.Get());
            }
            else
            {
                return Try<A>.Fail(ex);
            }
        }
    }
}
