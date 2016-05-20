//-----------------------------------------------------------------------
// <copyright file="Some.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public sealed class Some<T> : Option<T>
    {
        private readonly T value;


        internal Some(T value)
        {
            this.value = value;
        }




        public override bool IsEmpty
        {
            get
            {
                return false;
            }
        }

        public override bool IsDefined
        {
            get
            {
                return true;
            }
        }


        public override T Get()
        {
            return this.value;
        }

        public override T GetOrElse(T value)
        {
            return this.value;
        }


        public override T GetOrElse(Func<T> f)
        {
            return this.value;
        }

        public override Option<T> OrElse(Func<Option<T>> f)
        {
            return this;
        }

        public override Option<T> Recover(Func<T> f)
        {
            return this;
        }

        public override Option<B> Map<B>(Func<T, B> f)
        {
            return new Some<B>(f(this.value));
        }

        public override Option<B> FlatMap<B>(Func<T, Option<B>> f)
        {
            return f(this.value);
        }
    }
}
