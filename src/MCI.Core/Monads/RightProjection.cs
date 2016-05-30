//-----------------------------------------------------------------------
// <copyright file="RightProjection.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Monads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class RightProjection<L, R> : EitherProjection<R>
    {
        private Either<L, R> e;

        internal RightProjection(Either<L, R> e)
        {
            this.e = e;
        }

        public override bool IsDefined
        {
            get
            {
                return this.e.IsRight;
            }
        }

        public override bool IsEmpty
        {
            get
            {
                return this.e.IsLeft;
            }
        }

        public override R Get()
        {
            if (this.e.IsRight)
            {
                return ((Right<L, R>)this.e).Value;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public override R GetOrElse(R value)
        {
            if (this.e.IsRight)
            {
                return ((Right<L, R>)this.e).Value;
            }
            else
            {
                return value;
            }
        }

        public override R GetOrElse(Func<R> f)
        {
            if (this.e.IsRight)
            {
                return ((Right<L, R>)this.e).Value;
            }
            else
            {
                return f();
            }
        }

        public Either<L, X> Map<X>(Func<R, X> f)
        {
            if (this.e.IsRight)
            {
                return new Right<L, X>(f(((Right<L, R>)this.e).Value));
            }
            else
            {
                return new Left<L, X>(((Left<L, R>)this.e).Value);
            }
        }

        public Either<L, X> FlatMap<X>(Func<R, Either<L, X>> f)
        {
            if (this.e.IsRight)
            {
                return f(((Right<L, R>)this.e).Value);
            }
            else
            {
                return new Left<L, X>(((Left<L, R>)this.e).Value);
            }
        }

        public override Option<R> ToOption()
        {
            if (this.e.IsRight)
            {
                return new Some<R>(((Right<L, R>)this.e).Value);
            }
            else
            {
                return new None<R>();
            }
        }
    }
}
