//-----------------------------------------------------------------------
// <copyright file="LeftProjection.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Monads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class LeftProjection<L, R> : EitherProjection<L>
    {
        private Either<L, R> e;

        internal LeftProjection(Either<L, R> e)
        {
            this.e = e;
        }

        public override bool IsDefined
        {
            get
            {
                return this.e.IsLeft;
            }
        }

        public override bool IsEmpty
        {
            get
            {
                return this.e.IsRight;
            }
        }

        public override L Get()
        {
            if (this.e.IsLeft)
            {
                return ((Left<L, R>)this.e).Value;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public override L GetOrElse(L value)
        {
            if (this.e.IsLeft)
            {
                return ((Left<L, R>)this.e).Value;
            }
            else
            {
                return value;
            }
        }

        public override L GetOrElse(Func<L> f)
        {
            if (this.e.IsLeft)
            {
                return ((Left<L, R>)this.e).Value;
            }
            else
            {
                return f();
            }
        }

        public Either<X, R> Map<X>(Func<L, X> f)
        {
            if (this.e.IsLeft)
            {
                return new Left<X, R>(f(((Left<L, R>)this.e).Value));
            }
            else
            {
                return new Right<X, R>(((Right<L, R>)this.e).Value);
            }
        }

        public Either<X, R> FlatMap<X>(Func<L, Either<X, R>> f)
        {
            if (this.e.IsLeft)
            {
                return f(((Left<L, R>)this.e).Value);
            }
            else
            {
                return new Right<X, R>(((Right<L, R>)this.e).Value);
            }
        }

        public override Option<L> ToOption()
        {
            if (this.e.IsLeft)
            {
                return new Some<L>(((Left<L, R>)this.e).Value);
            }
            else
            {
                return new None<L>();
            }
        }
    }
}
