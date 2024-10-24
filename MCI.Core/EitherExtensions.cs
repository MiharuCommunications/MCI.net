//-----------------------------------------------------------------------
// <copyright file="EitherExtensions.cs" company="Miharu Communications Inc.">
//     © 2024 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public static class EitherExtensions
    {
        public static Either<IFailedReason, T> Flatten<T>(this Either<IFailedReason, Either<IFailedReason, T>> source)
        {
            if (source.IsLeft)
            {
                return new Left<IFailedReason, T>(source.Left.Get());
            }
            else
            {
                return source.Right.Get();
            }
        }

        public static T Merge<T>(this Either<T, T> either)
        {
            if (either.IsRight)
            {
                return either.Right.Get();
            }
            else
            {
                return either.Left.Get();
            }
        }

        public static Try<TR> ToTry<TL, TR>(this Either<TL, TR> either)
            where TL : Exception
        {
            if (either.IsRight)
            {
                return Try<TR>.Success(either.Get());
            }
            else
            {
                return Try<TR>.Fail(either.Left.Get());
            }
        }
    }
}
