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
