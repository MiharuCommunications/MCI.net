//-----------------------------------------------------------------------
// <copyright file="Either.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using Miharu.Monads;

    public abstract class Either<L, R>
    {
        internal protected Either()
        {
            this.Left = new LeftProjection<L, R>(this);
            this.Right = new RightProjection<L, R>(this);
        }

        public abstract bool IsLeft { get; }

        public abstract bool IsRight { get; }

        public LeftProjection<L, R> Left { get; private set; }

        public RightProjection<L, R> Right { get; private set; }

        public abstract A Fold<A>(Func<L, A> fl, Func<R, A> fr);

        public abstract Either<R, L> Swap();



        public abstract Either<L, R2> Select<R2>(Func<R, R2> f);

        public abstract Either<L, R2> SelectMany<R2>(Func<R, Either<L, R2>> f);
        public abstract Either<L, R3> SelectMany<R2, R3>(Func<R, Either<L, R2>> f, Func<R, R2, R3> g);

        public abstract R Get();

        public abstract Option<R> ToOption();

        public abstract void ForEach(Action<R> f);

        public abstract bool Exists(Func<R, bool> p);

        public abstract R GetOrElse(R value);
        public abstract R GetOrElse(Func<R> f);

        public abstract Either<L, R> OrElse(Func<Either<L,R>> f);

        public abstract Either<L, R> Recover(Func<L, R> f);

    }
}
