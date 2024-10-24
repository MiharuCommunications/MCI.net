//-----------------------------------------------------------------------
// <copyright file="Success.cs" company="Miharu Communications Inc.">
//     © 2024 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public sealed class Success<T> : Try<T>
    {
        private readonly T _value;


        internal Success(T value)
        {
            _value = value;
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
            return _value;
        }

        public override T GetOrElse(T def)
        {
            return _value;
        }

        public override T GetOrElse(Func<T> f)
        {
            return _value;
        }

        public override T GetOrElse(Func<Exception, T> f)
        {
            return _value;
        }

        public override Exception GetException()
        {
            throw new NullReferenceException();
        }



        public override Try<TResult> Select<TResult>(Func<T, TResult> f)
        {
            try
            {
                return new Success<TResult>(f(_value));
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
                f(_value);
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
                return f(_value);
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
                return f(_value);
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        public override Try<TC> SelectMany<TB, TC>(Func<T, Try<TB>> f, Func<T, TB, TC> g)
        {
            var x = _value;

            return f(x).SelectMany(y => new Success<TC>(g(x, y)));
        }

        public override Either<Exception, T> ToEither()
        {
            return new Right<Exception, T>(_value);
        }

        public override Option<T> ToOption()
        {
            return Option<T>.Return(_value);
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



        public override Try<T> Throw<TException>()
        {
            return this;
        }


        public override Try<T> Throw<TException>(Action<TException> when)
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
                if (f(_value))
                {
                    return new Success<T>(_value);
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
            f(_value);
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


        public override Try Throw<TException>()
        {
            return this;
        }


        public override Try Throw<TException>(Action<TException> when)
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
