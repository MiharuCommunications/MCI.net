//-----------------------------------------------------------------------
// <copyright file="LeftProjection.cs" company="Miharu Communications Inc.">
//     © 2024 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Monads
{
    using System;

    public sealed class LeftProjection<TL, TR> : EitherProjection<TL>
    {
        private readonly Either<TL, TR> _e;

        internal LeftProjection(Either<TL, TR> e)
        {
            _e = e;
        }

        public override bool IsDefined
        {
            get
            {
                return _e.IsLeft;
            }
        }

        public override bool IsEmpty
        {
            get
            {
                return _e.IsRight;
            }
        }

        public override TL Get()
        {
            if (_e.IsLeft)
            {
                return ((Left<TL, TR>)_e).Value;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public override TL GetOrElse(TL value)
        {
            if (_e.IsLeft)
            {
                return ((Left<TL, TR>)_e).Value;
            }
            else
            {
                return value;
            }
        }

        public override TL GetOrElse(Func<TL> f)
        {
            if (_e.IsLeft)
            {
                return ((Left<TL, TR>)_e).Value;
            }
            else
            {
                return f();
            }
        }


        public override Option<TL> ToOption()
        {
            if (_e.IsLeft)
            {
                return new Some<TL>(((Left<TL, TR>)_e).Value);
            }
            else
            {
                return new None<TL>();
            }
        }


        public Either<TL2, TR> Select<TL2>(Func<TL, TL2> f)
        {
            if (_e.IsLeft)
            {
                return new Left<TL2, TR>(f(((Left<TL, TR>)_e).Value));
            }
            else
            {
                return new Right<TL2, TR>(((Right<TL, TR>)_e).Value);
            }
        }

        public Either<TL2, TR> SelectMany<TL2>(Func<TL, Either<TL2, TR>> f)
        {
            if (_e.IsLeft)
            {
                return f(((Left<TL, TR>)_e).Value);
            }
            else
            {
                return new Right<TL2, TR>(((Right<TL, TR>)_e).Value);
            }
        }

        public Either<TL3, TR> SelectMany<TL2, TL3>(Func<TL, Either<TL2, TR>> f, Func<TL, TL2, TL3> g)
        {
            if (_e.IsLeft)
            {
                var x = ((Left<TL, TR>)_e).Value;

                return f(x).Left.SelectMany(y => new Left<TL3, TR>(g(x, y)));
            }
            else
            {
                return new Right<TL3, TR>(((Right<TL, TR>)_e).Value);
            }
        }

        public override int Count(Func<TL, bool> p)
        {
            if (_e.IsLeft)
            {
                var value = ((Left<TL, TR>)_e).Value;

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

        public override bool Exists(Func<TL, bool> p)
        {
            if (_e.IsLeft)
            {
                var value = ((Left<TL, TR>)_e).Value;

                return p(value);
            }
            else
            {
                return false;
            }
        }
    }
}
