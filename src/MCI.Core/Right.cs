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

    public sealed class Right<L, R> : Either<L, R>
    {
        public Right(R value)
            : base()
        {
            this.Value = value;
        }

        public override bool IsLeft
        {
            get
            {
                return false;
            }
        }

        public override bool IsRight
        {
            get
            {
                return true;
            }
        }


        public readonly R Value;

        public override A Fold<A>(Func<L, A> fl, Func<R, A> fr)
        {
            return fr(this.Value);
        }

        public override Either<R, L> Swap()
        {
            return new Left<R, L>(this.Value);
        }

        public override Either<L, R2> Select<R2>(Func<R, R2> f)
        {
            return new Right<L, R2>(f(this.Value));
        }

        public override Either<L, R2> SelectMany<R2>(Func<R, Either<L, R2>> f)
        {
            return f(this.Value);
        }

        public override Either<L, R3> SelectMany<R2, R3>(Func<R, Either<L, R2>> f, Func<R, R2, R3> g)
        {
            var x = this.Value;

            return f(x).SelectMany(y => new Right<L, R3>(g(x, y)));
        }

        public override Option<R> ToOption()
        {
            return Option<R>.Return(this.Value);
        }

        public override void ForEach(Action<R> f)
        {
            f(this.Value);
        }

        public override bool Exists(Func<R, bool> p)
        {
            return p(this.Value);
        }

        public override R Get()
        {
            return this.Value;
        }

        public override R GetOrElse(Func<R> f)
        {
            return this.Value;
        }

        public override R GetOrElse(R value)
        {
            return this.Value;
        }

        public override Either<L, R> OrElse(Func<Either<L, R>> f)
        {
            return this;
        }

        public override Either<L, R> Recover(Func<L, R> f)
        {
            return this;
        }

        public override Either<L, R> RecoverWith(Func<L, Either<L, R>> f)
        {
            return this;
        }
    }
}
