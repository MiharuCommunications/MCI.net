//-----------------------------------------------------------------------
// <copyright file="Try.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    public abstract class Try<T>
    {
        protected internal Try()
        {
        }

        /// <summary>
        /// Get <c>true</c> if the <c>Try</c> is a <c>Failure</c>, <c>false</c> otherwise.
        /// </summary>
        public abstract bool IsFailure { get; }

        /// <summary>
        /// Get <c>true</c> if the <c>Try</c> is a <c>Success</c>, <c>false</c> otherwise.
        /// </summary>
        public abstract bool IsSuccess { get; }

        [Obsolete("スタックトレースが消失するため非推奨")]
        public static Try<T> Execute(Func<T> f)
        {
            try
            {
                return new Success<T>(f());
            }
            catch (Exception ex)
            {
                return new Failure<T>(ex);
            }
        }

        [Obsolete("スタックトレースが消失するため非推奨")]
        public static Try<T> FlatExecute(Func<Try<T>> f)
        {
            try
            {
                return f();
            }
            catch (Exception ex)
            {
                return new Failure<T>(ex);
            }
        }

        public static Try<T> Success(T value)
        {
            return new Success<T>(value);
        }

        public static Try<T> Fail(Exception ex)
        {
            return new Failure<T>(ex);
        }

        /// <summary>
        /// Returns the value from this <c>Success</c> or throws the exception if this is a <c>Failure</c>.
        /// </summary>
        /// <returns></returns>
        public abstract T Get();

        /// <summary>
        /// Returns this <c>Try</c> if it's a <c>Success</c> or the given <c>def</c> argument if this is a <c>Failure</c>.
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public abstract T GetOrElse(T def);

        public abstract T GetOrElse(Func<T> f);

        public abstract T GetOrElse(Func<Exception, T> f);

        public abstract Exception GetException();

        public abstract Try<TB> Select<TB>(Func<T, TB> f);

        public abstract Try Select(Action<T> f);

        public abstract Try<TB> SelectMany<TB>(Func<T, Try<TB>> f);

        public abstract Try SelectMany(Func<T, Try> f);

        public abstract Try<TC> SelectMany<TB, TC>(Func<T, Try<TB>> f, Func<T, TB, TC> g);

        public abstract Try<T> Recover(Func<Exception, T> f);

        public abstract Try<T> RecoverWith(Func<Exception, Try<T>> f);

        public abstract Try<T> OrElse(Try<T> def);

        public abstract Option<T> ToOption();

        public abstract Either<Exception, T> ToEither();

        public abstract Option<Exception> ToException();

        public abstract Try<T> Throw<TException>() where TException : Exception;

        public abstract Try<T> Throw<TException>(Action<TException> when) where TException : Exception;

        public abstract Try<T> Where(Func<T, bool> f);

        public abstract void ForEach(Action<T> f);

        public abstract Try Collapse();
    }

    public abstract class Try
    {
        protected internal Try()
        {
        }

        public abstract bool IsSuccess { get; }

        public abstract bool IsFailure { get; }

        [Obsolete("スタックトレースが消失するため非推奨")]
        public static Try Execute(Action f)
        {
            try
            {
                f();
                return new Success();
            }
            catch (Exception ex)
            {
                return new Failure(ex);
            }
        }

        [Obsolete("スタックトレースが消失するため非推奨")]
        public static Try FlatExecute(Func<Try> f)
        {
            try
            {
                return f();
            }
            catch (Exception e)
            {
                return new Failure(e);
            }
        }

        public static Try Success()
        {
            return new Success();
        }

        public static Try<T> Success<T>(T value)
        {
            return new Success<T>(value);
        }

        public static Try Fail(Exception ex)
        {
            return new Failure(ex);
        }

        public abstract Exception GetException();

        public abstract Try Select(Action f);

        public abstract Try SelectMany(Func<Try> f);

        public abstract void ForEach(Action f);

        public abstract Try Recover(Action<Exception> f);

        public abstract Try RecoverWith(Func<Exception, Try> f);

        public abstract Try Throw<TException>() where TException : Exception;

        public abstract Try Throw<TException>(Action<TException> when) where TException : Exception;

        public abstract Option<Exception> ToException();
    }
}
