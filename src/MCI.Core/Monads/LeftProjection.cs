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


        public Either<L2, R> Select<L2>(Func<L, L2> f)
        {
            if (this.e.IsLeft)
            {
                return new Left<L2, R>(f(((Left<L, R>)this.e).Value));
            }
            else
            {
                return new Right<L2, R>(((Right<L, R>)this.e).Value);
            }
        }

        public Either<L2, R> SelectMany<L2>(Func<L, Either<L2, R>> f)
        {
            if (this.e.IsLeft)
            {
                return f(((Left<L, R>)this.e).Value);
            }
            else
            {
                return new Right<L2, R>(((Right<L, R>)this.e).Value);
            }
        }

        public Either<L3, R> SelectMany<L2, L3>(Func<L, Either<L2, R>> f, Func<L, L2, L3> g)
        {
            if (this.e.IsLeft)
            {
                var x = ((Left<L, R>)this.e).Value;

                return f(x).Left.SelectMany(y => new Left<L3, R>(g(x, y)));
            }
            else
            {
                return new Right<L3, R>(((Right<L, R>)this.e).Value);
            }
        }
    }
}
