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


        public Either<L, R2> Select<R2>(Func<R, R2> f)
        {
            if (this.e.IsRight)
            {
                return new Right<L, R2>(f(((Right<L, R>)this.e).Value));
            }
            else
            {
                return new Left<L, R2>(((Left<L, R>)this.e).Value);
            }
        }

        public Either<L, R2> SelectMany<R2>(Func<R, Either<L, R2>> f)
        {
            if (this.e.IsRight)
            {
                return f(((Right<L, R>)this.e).Value);
            }
            else
            {
                return new Left<L, R2>(((Left<L, R>)this.e).Value);
            }
        }

        public Either<L, R3> SelectMany<R2, R3>(Func<R, Either<L, R2>> f, Func<R, R2, R3> g)
        {
            if (this.e.IsRight)
            {
                var x = ((Right<L, R>)this.e).Value;

                return f(x).Right.SelectMany(y => new Right<L, R3>(g(x, y)));
            }
            else
            {
                return new Left<L, R3>(((Left<L, R>)this.e).Value);
            }
        }


        public override int Count(Func<R, bool> p)
        {
            if (this.e.IsRight)
            {
                var value = ((Right<L, R>)this.e).Value;

                if (p(value))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public override bool Exists(Func<R, bool> p)
        {
            if (this.e.IsRight)
            {
                var value = ((Right<L, R>)this.e).Value;

                return p(value);
            }
            else
            {
                return false;
            }
        }
    }
}
