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
    }
}
