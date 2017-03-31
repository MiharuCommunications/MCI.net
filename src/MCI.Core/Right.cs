//-----------------------------------------------------------------------
// <copyright file="Right.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public sealed class Right<TL, TR> : Either<TL, TR>
    {
        public readonly TR Value;

        public Right(TR value)
        {
            Value = value;
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



        public override TA Fold<TA>(Func<TL, TA> fl, Func<TR, TA> fr)
        {
            return fr(Value);
        }

        public override Either<TR, TL> Swap()
        {
            return new Left<TR, TL>(Value);
        }

        public override Either<TL, TR2> Select<TR2>(Func<TR, TR2> f)
        {
            return new Right<TL, TR2>(f(Value));
        }

        public override Either<TL, TR2> SelectMany<TR2>(Func<TR, Either<TL, TR2>> f)
        {
            return f(Value);
        }

        public override Either<TL, TR3> SelectMany<TR2, TR3>(Func<TR, Either<TL, TR2>> f, Func<TR, TR2, TR3> g)
        {
            var x = Value;

            return f(x).SelectMany(y => new Right<TL, TR3>(g(x, y)));
        }

        public override Option<TR> ToOption()
        {
            return Option<TR>.Return(Value);
        }

        public override void ForEach(Action<TR> f)
        {
            f(Value);
        }

        public override bool Exists(Func<TR, bool> p)
        {
            return p(Value);
        }

        public override TR Get()
        {
            return Value;
        }

        public override TR GetOrElse(Func<TR> f)
        {
            return Value;
        }

        public override TR GetOrElse(TR value)
        {
            return Value;
        }

        public override Either<TL, TR> OrElse(Func<Either<TL, TR>> f)
        {
            return this;
        }

        public override Either<TL, TR> Recover(Func<TL, TR> f)
        {
            return this;
        }

        public override Either<TL, TR> RecoverWith(Func<TL, Either<TL, TR>> f)
        {
            return this;
        }
    }
}
