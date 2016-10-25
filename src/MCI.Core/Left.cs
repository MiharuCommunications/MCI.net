//-----------------------------------------------------------------------
// <copyright file="Right.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class Left<L, R> : Either<L, R>
    {
        public Left(L value)
            : base()
        {
            this.Value = value;
        }

        public override bool IsLeft
        {
            get
            {
                return true;
            }
        }

        public override bool IsRight
        {
            get
            {
                return false;
            }
        }

        public readonly L Value;

        public override A Fold<A>(Func<L, A> fl, Func<R, A> fr)
        {
            return fl(this.Value);
        }

        public override Either<R, L> Swap()
        {
            return new Right<R, L>(this.Value);
        }


        public override Either<L, R2> Select<R2>(Func<R, R2> f)
        {
            return new Left<L, R2>(this.Value);
        }

        public override Either<L, R2> SelectMany<R2>(Func<R, Either<L, R2>> f)
        {
            return new Left<L, R2>(this.Value);
        }

        public override Either<L, R3> SelectMany<R2, R3>(Func<R, Either<L, R2>> f, Func<R, R2, R3> g)
        {
            return new Left<L, R3>(this.Value);
        }

        public override Option<R> ToOption()
        {
            return Option<R>.Fail();
        }

        public override void ForEach(Action<R> f)
        {
            return;
        }
    }
}
