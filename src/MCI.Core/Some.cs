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
        private readonly T _value;


        internal Some(T value)
        {
            this._value = value;
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
            return this._value;
        }

        public override T GetOrElse(T value)
        {
            return this._value;
        }


        public override T GetOrElse(Func<T> f)
        {
            return this._value;
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
            return new Some<B>(f(this._value));
        }

        public override Option<B> SelectMany<B>(Func<T, Option<B>> f)
        {
            return f(this._value);
        }

        public override Option<C> SelectMany<B, C>(Func<T, Option<B>> f, Func<T, B, C> g)
        {
            var x = this._value;

            return f(x).SelectMany(y => new Some<C>(g(x, y)));
        }


        public override Option<T> Where(Func<T, bool> f)
        {
            if (f(this._value))
            {
                return new Some<T>(this._value);
            }
            else
            {
                return new None<T>();
            }
        }


        public override void ForEach(Action<T> f)
        {
            f(this._value);
        }

        public override Either<T, R> ToLeft<R>(Func<R> f)
        {
            return new Left<T, R>(this._value);
        }

        public override Either<L, T> ToRight<L>(Func<L> f)
        {
            return new Right<L, T>(this._value);
        }

        public override Either<L, T> ToEither<L>(Func<L> f)
        {
            return new Right<L, T>(this._value);
        }

        public override Try<T> ToTry()
        {
            return Try<T>.Success(this._value);
        }

        public override Try<T> ToTry(Exception ex)
        {
            return new Success<T>(this._value);
        }

        public override int Count(Func<T, bool> p)
        {
            if (p(this._value))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override bool Exists(Func<T, bool> p)
        {
            return p(this._value);
        }
    }
}
