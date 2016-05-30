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

        public override Option<B> Select<B>(Func<T, B> f)
        {
            return new Some<B>(f(this.value));
        }

        public override Option<B> SelectMany<B>(Func<T, Option<B>> f)
        {
            return f(this.value);
        }

        public override Option<C> SelectMany<B, C>(Func<T, Option<B>> f, Func<T, B, C> g)
        {
            var x = this.value;

            return f(x).SelectMany(y => new Some<C>(g(x, y)));
        }


        public override Option<T> Where(Func<T, bool> f)
        {
            if (f(this.value))
            {
                return new Some<T>(this.value);
            }
            else
            {
                return new None<T>();
            }
        }


        public override void ForEach(Action<T> f)
        {
            f(this.value);
        }

        public override Try<T> ToTry(Exception ex)
        {
            return new Success<T>(this.value);
        }
    }
}
