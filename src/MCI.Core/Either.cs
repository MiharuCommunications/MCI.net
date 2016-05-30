//-----------------------------------------------------------------------
// <copyright file="Either.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

    }
}
