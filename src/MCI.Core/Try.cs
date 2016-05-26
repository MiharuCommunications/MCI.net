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
        internal protected Try()
        {
        }

        public abstract bool IsSuccess { get; }
        public abstract bool IsFailure { get; }

        public abstract A Get();
        public abstract A GetOrElse(A def);
        public abstract A GetOrElse(Func<A> f);
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
        public abstract Option<Exception> ToException();

        public abstract Try<A> Throw<E>() where E : Exception;
        public abstract Try<A> Throw<E>(Action<E> when) where E : Exception;

        public abstract Try<A> Where(Func<A, bool> f);

        public abstract void ForEach(Action<A> f);

        public abstract Try Collapse();



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
    }


    public abstract class Try
    {
        internal protected Try()
        {
        }

        public abstract bool IsSuccess { get; }
        public abstract bool IsFailure { get; }

        public abstract Exception GetException();

        public abstract Try Select(Action f);
        public abstract Try SelectMany(Func<Try> f);
        public abstract void ForEach(Action f);

        public abstract Try Recover(Action<Exception> f);
        public abstract Try RecoverWith(Func<Exception, Try> f);

        public abstract Try Throw<E>() where E : Exception;
        public abstract Try Throw<E>(Action<E> when) where E : Exception;
        public abstract Option<Exception> ToException();

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
    }
}
