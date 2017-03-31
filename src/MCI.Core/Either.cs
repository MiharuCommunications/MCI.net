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
        protected internal Either()
        {
            this.Left = new LeftProjection<L, R>(this);
            this.Right = new RightProjection<L, R>(this);
        }

        /// <summary>
        /// Get <code>true</code> if this is a <code>Left</code>, <code>false</code> otherwise.
        /// </summary>
        public abstract bool IsLeft { get; }

        /// <summary>
        /// Get true if this is a Right, false otherwise.
        /// </summary>
        public abstract bool IsRight { get; }

        /// <summary>
        /// Projects this Either as a Left.
        /// </summary>
        public LeftProjection<L, R> Left { get; private set; }

        /// <summary>
        /// Projects this Either as a Right.
        /// </summary>
        public RightProjection<L, R> Right { get; private set; }

        /// <summary>
        /// Applies <code>fl</code> if this is a Left or fr if this is a Right.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="fl"></param>
        /// <param name="fr"></param>
        /// <returns></returns>
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

        public abstract Either<L, R> RecoverWith(Func<L, Either<L, R>> f);

    }
}
