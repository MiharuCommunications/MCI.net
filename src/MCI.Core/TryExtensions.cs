//-----------------------------------------------------------------------
// <copyright file="TryExtensions.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public static class TryExtensions
    {
        public static Try<A> Where<A>(this Try<A> source, Func<A, bool> f)
        {
            if (source.IsSuccess)
            {
                var value = source.Get();

                try
                {
                    if (f(value))
                    {
                        return new Success<A>(value);
                    }
                    else
                    {
                        return new Failure<A>(new Exception("Where fail"));
                    }
                }
                catch (Exception e)
                {
                    return new Failure<A>(e);
                }
            }
            else
            {
                return new Failure<A>(source.GetException());
            }
        }


        public static void ForEach<A>(this Try<A> source, Action<A> f)
        {
            if (source.IsSuccess)
            {
                f(source.Get());
            }
        }
    }
}
