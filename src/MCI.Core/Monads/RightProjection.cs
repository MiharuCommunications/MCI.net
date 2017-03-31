//-----------------------------------------------------------------------
// <copyright file="RightProjection.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Monads
{
    using System;

    public sealed class RightProjection<TL, TR> : EitherProjection<TR>
    {
        private readonly Either<TL, TR> _e;

        internal RightProjection(Either<TL, TR> e)
        {
            _e = e;
        }

        public override bool IsDefined
        {
            get
            {
                return _e.IsRight;
            }
        }

        public override bool IsEmpty
        {
            get
            {
                return _e.IsLeft;
            }
        }

        public override TR Get()
        {
            if (_e.IsRight)
            {
                return ((Right<TL, TR>)_e).Value;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public override TR GetOrElse(TR value)
        {
            if (_e.IsRight)
            {
                return ((Right<TL, TR>)_e).Value;
            }
            else
            {
                return value;
            }
        }

        public override TR GetOrElse(Func<TR> f)
        {
            if (_e.IsRight)
            {
                return ((Right<TL, TR>)_e).Value;
            }
            else
            {
                return f();
            }
        }

        public override Option<TR> ToOption()
        {
            if (_e.IsRight)
            {
                return new Some<TR>(((Right<TL, TR>)_e).Value);
            }
            else
            {
                return new None<TR>();
            }
        }


        public Either<TL, TR2> Select<TR2>(Func<TR, TR2> f)
        {
            if (_e.IsRight)
            {
                return new Right<TL, TR2>(f(((Right<TL, TR>)_e).Value));
            }
            else
            {
                return new Left<TL, TR2>(((Left<TL, TR>)_e).Value);
            }
        }

        public Either<TL, TR2> SelectMany<TR2>(Func<TR, Either<TL, TR2>> f)
        {
            if (_e.IsRight)
            {
                return f(((Right<TL, TR>)_e).Value);
            }
            else
            {
                return new Left<TL, TR2>(((Left<TL, TR>)_e).Value);
            }
        }

        public Either<TL, TR3> SelectMany<TR2, TR3>(Func<TR, Either<TL, TR2>> f, Func<TR, TR2, TR3> g)
        {
            if (_e.IsRight)
            {
                var x = ((Right<TL, TR>)_e).Value;

                return f(x).Right.SelectMany(y => new Right<TL, TR3>(g(x, y)));
            }
            else
            {
                return new Left<TL, TR3>(((Left<TL, TR>)_e).Value);
            }
        }


        public override int Count(Func<TR, bool> p)
        {
            if (_e.IsRight)
            {
                var value = ((Right<TL, TR>)_e).Value;

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

        public override bool Exists(Func<TR, bool> p)
        {
            if (_e.IsRight)
            {
                var value = ((Right<TL, TR>)_e).Value;

                return p(value);
            }
            else
            {
                return false;
            }
        }
    }
}
