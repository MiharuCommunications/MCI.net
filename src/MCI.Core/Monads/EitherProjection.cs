//-----------------------------------------------------------------------
// <copyright file="EitherProjection.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Monads
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class EitherProjection<T>
    {
        internal protected EitherProjection()
        {
        }

        public abstract bool IsDefined { get; }

        public abstract bool IsEmpty { get; }

        public abstract T Get();

        public abstract T GetOrElse(T value);

        public abstract T GetOrElse(Func<T> f);

        public abstract Option<T> ToOption();
    }
}
