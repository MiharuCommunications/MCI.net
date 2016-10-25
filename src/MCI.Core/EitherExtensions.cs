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
    }
}
