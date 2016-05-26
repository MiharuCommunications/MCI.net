//-----------------------------------------------------------------------
// <copyright file="Success.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public sealed class Success<T> : Try<T>
    {
        private readonly T value;


        internal Success(T value)
        {
            this.value = value;
        }


        public override bool IsSuccess
        {
            get
            {
                return true;
            }
        }

        public override bool IsFailure
        {
            get
            {
                return false;
            }
        }

        public override T Get()
        {
            return this.value;
        }

        public override T GetOrElse(T def)
        {
            return this.value;
        }

        public override T GetOrElse(Func<T> f)
        {
            return this.value;
        }

        public override Exception GetException()
        {
            throw new NullReferenceException();
        }



        public override Try<TResult> Select<TResult>(Func<T, TResult> f)
        {
            try
            {
                return new Success<TResult>(f(this.value));
            }
            catch (Exception ex)
            {
                return new Failure<TResult>(ex);
            }
        }

        public override Try Select(Action<T> f)
        {
            try
            {
                f(this.value);
                return new Success();
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        public override Try<TResult> SelectMany<TResult>(Func<T, Try<TResult>> f)
        {
            try
            {
                return f(this.value);
            }
            catch (Exception ex)
            {
                return new Failure<TResult>(ex);
            }
        }

        public override Try SelectMany(Func<T, Try> f)
        {
            try
            {
                return f(this.value);
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        public override Try<C> SelectMany<B, C>(Func<T, Try<B>> f, Func<T, B, C> g)
        {
            var x = this.value;

            return f(x).SelectMany(y => new Success<C>(g(x, y)));
        }

        public override Option<T> ToOption()
        {
            return Option<T>.Return(this.value);
        }


        public override Try<T> OrElse(Try<T> def)
        {
            return this;
        }

        public override Try<T> Recover(Func<Exception, T> f)
        {
            return this;
        }

        public override Try<T> RecoverWith(Func<Exception, Try<T>> f)
        {
            return this;
        }



        public override Try<T> Throw<E>()
        {
            return this;
        }


        public override Try<T> Throw<E>(Action<E> when)
        {
            return this;
        }


        public override Option<Exception> ToException()
        {
            return Option<Exception>.Fail();
        }


        public override Try<T> Where(Func<T, bool> f)
        {
            try
            {
                if (f(this.value))
                {
                    return new Success<T>(this.value);
                }
                else
                {
                    return new Failure<T>(new Exception("Where fail"));
                }
            }
            catch (Exception e)
            {
                return new Failure<T>(e);
            }
        }

        public override void ForEach(Action<T> f)
        {
            f(this.value);
        }


        public override Try Collapse()
        {
            return new Success();
        }
    }



    public sealed class Success : Try
    {
        internal Success()
        {
        }

        public override bool IsSuccess
        {
            get
            {
                return true;
            }
        }

        public override bool IsFailure
        {
            get
            {
                return false;
            }
        }


        public override Try Select(Action f)
        {
            try
            {
                f();
                return this;
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        public override Try SelectMany(Func<Try> f)
        {
            try
            {
                return f();
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        public override void ForEach(Action f)
        {
            f();
        }

        public override Try Recover(Action<Exception> f)
        {
            return this;
        }

        public override Try RecoverWith(Func<Exception, Try> f)
        {
            return this;
        }


        public override Try Throw<E>()
        {
            return this;
        }


        public override Try Throw<E>(Action<E> when)
        {
            return this;
        }


        public override Option<Exception> ToException()
        {
            return new None<Exception>();
        }

        public override Exception GetException()
        {
            throw new NullReferenceException();
        }
    }
}
