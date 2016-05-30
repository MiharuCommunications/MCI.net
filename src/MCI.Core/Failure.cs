//-----------------------------------------------------------------------
// <copyright file="Failure.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class Failure<T> : Try<T>
    {
        private readonly Exception exception;


        internal Failure(Exception exception)
        {
            this.exception = exception;
        }


        public override bool IsSuccess
        {
            get
            {
                return false;
            }
        }

        public override bool IsFailure
        {
            get
            {
                return true;
            }
        }

        public override T Get()
        {
            throw this.exception;
        }

        public override T GetOrElse(T def)
        {
            return def;
        }

        public override T GetOrElse(Func<T> f)
        {
            return f();
        }

        public override Exception GetException()
        {
            return this.exception;
        }


        public override Try<TResult> Select<TResult>(Func<T, TResult> f)
        {
            return new Failure<TResult>(this.exception);
        }

        public override Try Select(Action<T> f)
        {
            return new Failure(this.exception);
        }

        public override Try<TResult> SelectMany<TResult>(Func<T, Try<TResult>> f)
        {
            return new Failure<TResult>(this.exception);
        }

        public override Try SelectMany(Func<T, Try> f)
        {
            return new Failure(this.exception);
        }

        public override Try<C> SelectMany<B, C>(Func<T, Try<B>> f, Func<T, B, C> g)
        {
            return new Failure<C>(this.exception);
        }

        public override Option<T> ToOption()
        {
            return Option<T>.Fail();
        }

        public override Try<T> OrElse(Try<T> def)
        {
            return def;
        }


        public override Try<T> Recover(Func<Exception, T> f)
        {
            try
            {
                return new Success<T>(f(this.exception));
            }
            catch (Exception ex)
            {
                return new Failure<T>(ex);
            }
        }

        public override Try<T> RecoverWith(Func<Exception, Try<T>> f)
        {
            try
            {
                return f(this.exception);
            }
            catch (Exception ex)
            {
                return new Failure<T>(ex);
            }
        }



        public override Try<T> Throw<E>()
        {
            if (this.exception is E)
            {
                throw this.exception;
            }

            return this;
        }


        public override Try<T> Throw<E>(Action<E> when)
        {
            if (this.exception is E)
            {
                when((E)this.exception);
                throw this.exception;
            }

            return this;
        }


        public override Option<Exception> ToException()
        {
            return Option<Exception>.Return(this.exception);
        }


        public override Try<T> Where(Func<T, bool> f)
        {
            return new Failure<T>(this.exception);
        }

        public override void ForEach(Action<T> f)
        {
            return;
        }


        public override Try Collapse()
        {
            return new Failure(this.exception);
        }
    }



    public sealed class Failure : Try
    {
        private readonly Exception exception;


        internal Failure(Exception exception)
        {
            this.exception = exception;
        }


        public override bool IsSuccess
        {
            get
            {
                return false;
            }
        }

        public override bool IsFailure
        {
            get
            {
                return true;
            }
        }


        public override Try Select(Action f)
        {
            return this;
        }


        public override Try SelectMany(Func<Try> f)
        {
            return this;
        }

        public override void ForEach(Action f)
        {
            return;
        }


        public override Try Recover(Action<Exception> f)
        {
            try
            {
                f(this.exception);
                return new Success();
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        public override Try RecoverWith(Func<Exception, Try> f)
        {
            try
            {
                return f(this.exception);
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }


        public override Try Throw<E>()
        {
            if (this.exception is E)
            {
                throw this.exception;
            }

            return this;
        }


        public override Try Throw<E>(Action<E> when)
        {
            if (this.exception is E)
            {
                when((E)this.exception);
                throw this.exception;
            }

            return this;
        }


        public override Option<Exception> ToException()
        {
            return Option<Exception>.Return(this.exception);
        }

        public override Exception GetException()
        {
            return this.exception;
        }
    }
}
