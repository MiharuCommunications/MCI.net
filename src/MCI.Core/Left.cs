//-----------------------------------------------------------------------
// <copyright file="Right.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public sealed class Left<TL, TR> : Either<TL, TR>
    {
        public readonly TL Value;

        public Left(TL value)
        {
            Value = value;
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

        public override TA Fold<TA>(Func<TL, TA> fl, Func<TR, TA> fr)
        {
            return fl(Value);
        }

        public override Either<TR, TL> Swap()
        {
            return new Right<TR, TL>(Value);
        }


        public override Either<TL, TR2> Select<TR2>(Func<TR, TR2> f)
        {
            return new Left<TL, TR2>(Value);
        }

        public override Either<TL, TR2> SelectMany<TR2>(Func<TR, Either<TL, TR2>> f)
        {
            return new Left<TL, TR2>(Value);
        }

        public override Either<TL, TR3> SelectMany<TR2, TR3>(Func<TR, Either<TL, TR2>> f, Func<TR, TR2, TR3> g)
        {
            return new Left<TL, TR3>(Value);
        }

        public override Option<TR> ToOption()
        {
            return Option<TR>.Fail();
        }

        public override void ForEach(Action<TR> f)
        {
        }

        public override bool Exists(Func<TR, bool> p)
        {
            return false;
        }

        public override TR Get()
        {
            throw new InvalidOperationException("This Either is Left");
        }

        public override TR GetOrElse(TR value)
        {
            return value;
        }

        public override Either<TL, TR> OrElse(Func<Either<TL, TR>> f)
        {
            return f();
        }

        public override TR GetOrElse(Func<TR> f)
        {
            return f();
        }

        public override Either<TL, TR> Recover(Func<TL, TR> f)
        {
            return new Right<TL, TR>(f(Value));
        }

        public override Either<TL, TR> RecoverWith(Func<TL, Either<TL, TR>> f)
        {
            return f(Value);
        }
    }
}
