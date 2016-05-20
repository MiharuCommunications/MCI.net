using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu
{
    public static class Option2Extensions
    {
        public static Option2<B> Select<A, B>(this Option2<A> opt, Func<A, B> f)
        {
            if (opt.HasValue)
            {
                return new Option2<B>(f(opt.Value));
            }
            else
            {
                return new Option2<B>();
            }
        }


        public static Option2<C> SelectMany<A, B, C>(this Option2<A> opt, Func<A, Option2<B>> f, Func<A, B, C> g)
        {
            if (opt.HasValue)
            {
                var x = opt.Value;

                return f(x).FlatMap(y => new Option2<C>(g(x, y)));
            }
            else
            {
                return new Option2<C>();
            }
        }


        public static Option2<A> Where<A>(this Option2<A> opt, Func<A, bool> f)
        {
            if (opt.HasValue)
            {
                var value = opt.Value;

                if (f(value))
                {
                    return new Option2<A>(value);
                }
            }

            return new Option2<A>();
        }


        public static void ForEach<A>(this Option2<A> opt, Action<A> f)
        {
            if (opt.HasValue)
            {
                f(opt.Value);
            }
        }

        public static Try<A> ToTry<A>(this Option2<A> opt, Exception ex)
        {
            if (opt.HasValue)
            {
                return Try<A>.Success(opt.Value);
            }
            else
            {
                return Try<A>.Fail(ex);
            }
        }
    }
}
