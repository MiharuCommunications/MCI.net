//-----------------------------------------------------------------------
// <copyright file="Try.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Try<A>
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
        public static Try<A> Execute(Func<A> f)
        {
            try
            {
                return new Success<A>(f());
            }
            catch (Exception ex)
            {
                return new Failure<A>(ex);
            }
        }

        [Obsolete("スタックトレースが消失するため非推奨")]
        public static Try<A> FlatExecute(Func<Try<A>> f)
        {
            try
            {
                return f();
            }
            catch (Exception ex)
            {
                return new Failure<A>(ex);
            }
        }

        public static Try<A> Success(A value)
        {
            return new Success<A>(value);
        }

        public static Try<A> Fail(Exception ex)
        {
            return new Failure<A>(ex);
        }

        /// <summary>
        /// Returns the value from this <c>Success</c> or throws the exception if this is a <c>Failure</c>.
        /// </summary>
        /// <returns></returns>
        public abstract A Get();

        /// <summary>
        /// Returns this <c>Try</c> if it's a <c>Success</c> or the given <c>def</c> argument if this is a <c>Failure</c>.
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public abstract A GetOrElse(A def);

        public abstract A GetOrElse(Func<A> f);

        public abstract A GetOrElse(Func<Exception, A> f);

        public abstract Exception GetException();

        public abstract Try<B> Select<B>(Func<A, B> f);

        public abstract Try Select(Action<A> f);

        public abstract Try<B> SelectMany<B>(Func<A, Try<B>> f);

        public abstract Try SelectMany(Func<A, Try> f);

        public abstract Try<C> SelectMany<B, C>(Func<A, Try<B>> f, Func<A, B, C> g);

        public abstract Try<A> Recover(Func<Exception, A> f);

        public abstract Try<A> RecoverWith(Func<Exception, Try<A>> f);

        public abstract Try<A> OrElse(Try<A> def);

        public abstract Option<A> ToOption();

        public abstract Either<Exception, A> ToEither();

        public abstract Option<Exception> ToException();

        public abstract Try<A> Throw<E>() where E : Exception;

        public abstract Try<A> Throw<E>(Action<E> when) where E : Exception;

        public abstract Try<A> Where(Func<A, bool> f);

        public abstract void ForEach(Action<A> f);

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

        public abstract Try Throw<E>() where E : Exception;

        public abstract Try Throw<E>(Action<E> when) where E : Exception;

        public abstract Option<Exception> ToException();
    }
}
