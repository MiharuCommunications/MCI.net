//-----------------------------------------------------------------------
// <copyright file="Failure.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public sealed class Failure<T> : Try<T>
    {
        private readonly Exception _exception;


        internal Failure(Exception exception)
        {
            _exception = exception;
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
            throw _exception;
        }

        public override T GetOrElse(T def)
        {
            return def;
        }

        public override T GetOrElse(Func<T> f)
        {
            return f();
        }

        public override T GetOrElse(Func<Exception, T> f)
        {
            return f(_exception);
        }

        public override Exception GetException()
        {
            return _exception;
        }


        public override Try<TResult> Select<TResult>(Func<T, TResult> f)
        {
            return new Failure<TResult>(_exception);
        }

        public override Try Select(Action<T> f)
        {
            return new Failure(_exception);
        }

        public override Try<TResult> SelectMany<TResult>(Func<T, Try<TResult>> f)
        {
            return new Failure<TResult>(_exception);
        }

        public override Try SelectMany(Func<T, Try> f)
        {
            return new Failure(_exception);
        }

        public override Try<TC> SelectMany<TB, TC>(Func<T, Try<TB>> f, Func<T, TB, TC> g)
        {
            return new Failure<TC>(_exception);
        }

        public override Either<Exception, T> ToEither()
        {
            return new Left<Exception, T>(_exception);
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
                return new Success<T>(f(_exception));
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
                return f(_exception);
            }
            catch (Exception ex)
            {
                return new Failure<T>(ex);
            }
        }



        public override Try<T> Throw<TException>()
        {
            if (_exception is TException)
            {
                throw _exception;
            }

            return this;
        }


        public override Try<T> Throw<TException>(Action<TException> when)
        {
            var e = _exception as TException;

            if (e == null)
            {
                return this;
            }

            when(e);
            throw e;
        }


        public override Option<Exception> ToException()
        {
            return Option<Exception>.Return(_exception);
        }


        public override Try<T> Where(Func<T, bool> f)
        {
            return new Failure<T>(_exception);
        }

        public override void ForEach(Action<T> f)
        {
        }


        public override Try Collapse()
        {
            return new Failure(_exception);
        }
    }



    public sealed class Failure : Try
    {
        private readonly Exception _exception;


        internal Failure(Exception exception)
        {
            _exception = exception;
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
        }


        public override Try Recover(Action<Exception> f)
        {
            try
            {
                f(_exception);

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
                return f(_exception);
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }


        public override Try Throw<TException>()
        {
            if (_exception is TException)
            {
                throw _exception;
            }

            return this;
        }


        public override Try Throw<TException>(Action<TException> when)
        {
            var e = _exception as TException;

            if (e == null)
            {
                return this;
            }

            when(e);
            throw e;
        }


        public override Option<Exception> ToException()
        {
            return Option<Exception>.Return(_exception);
        }

        public override Exception GetException()
        {
            return _exception;
        }
    }
}
