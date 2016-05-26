//-----------------------------------------------------------------------
// <copyright file="None.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public sealed class None<T> : Option<T>
    {
        internal None()
        {
        }

        public override bool IsEmpty
        {
            get
            {
                return true;
            }
        }

        public override bool IsDefined
        {
            get
            {
                return false;
            }
        }


        public override T Get()
        {
            throw new NullReferenceException();
        }

        public override T GetOrElse(T value)
        {
            return value;
        }


        public override T GetOrElse(Func<T> f)
        {
            return f();
        }


        public override Option<T> OrElse(Func<Option<T>> f)
        {
            return f();
        }


        public override Option<T> Recover(Func<T> f)
        {
            return new Some<T>(f());
        }

        public override Option<B> Select<B>(Func<T, B> f)
        {
            return new None<B>();
        }

        public override Option<B> SelectMany<B>(Func<T, Option<B>> f)
        {
            return new None<B>();
        }

        public override Option<C> SelectMany<B, C>(Func<T, Option<B>> f, Func<T, B, C> g)
        {
            return new None<C>();
        }


        public override Option<T> Where(Func<T, bool> f)
        {
            return new None<T>();
        }

        public override void ForEach(Action<T> f)
        {
            return;
        }

        public override Try<T> ToTry(Exception ex)
        {
            return new Failure<T>(ex);
        }
    }
}
