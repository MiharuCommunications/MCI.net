//-----------------------------------------------------------------------
// <copyright file="EitherExtensions.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using Miharu.Monads;

    public static class EitherExtensions
    {
        public static A Merge<A>(this Either<A, A> either)
        {
            if (either.IsRight)
            {
                return ((Right<A, A>)either).Value;
            }
            else
            {
                return ((Left<A, A>)either).Value;
            }
        }

        public static Option<B> Select<A, B>(this EitherProjection<A> p, Func<A, B> f)
        {
            if (p.IsDefined)
            {
                return new Some<B>(f(p.Get()));
            }
            else
            {
                return new None<B>();
            }
        }

        public static Option<C> SelectMany<A, B, C>(this EitherProjection<A> p, Func<A, EitherProjection<B>> f, Func<A, B, C> g)
        {
            if (p.IsDefined)
            {
                var x = p.Get();

                return f(x).ToOption().SelectMany(y => new Some<C>(g(x, y)));
            }
            else
            {
                return new None<C>();
            }
        }

        public static Option<A> Where<A>(this EitherProjection<A> p, Func<A, bool> f)
        {
            if (p.IsDefined)
            {
                var value = p.Get();

                if (f(value))
                {
                    return new Some<A>(value);
                }
                else
                {
                    return new None<A>();
                }
            }
            else
            {
                return new None<A>();
            }
        }
    }
}
