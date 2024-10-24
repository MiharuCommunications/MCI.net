//-----------------------------------------------------------------------
// <copyright file="Option.cs" company="Miharu Communications Inc.">
//     © 2024 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using Miharu.Monads;

    /// <summary>
    /// Option Monad
    /// </summary>
    /// <typeparam name="T">type of value which Option monad hold</typeparam>
    public abstract class Option<T>
    {
        /// <summary>
        /// can't use from other assemblies
        /// </summary>
        protected internal Option()
        {
        }

        /// <summary>
        /// Get true if the option is an instance of None, false otherwise.
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// Get true if the option is an instance of Some, false otherwise.
        /// </summary>
        public abstract bool IsDefined { get; }

        /// <summary>
        /// <para>Return a Some containing the result of applying f to this Option's value if this Option is nonempty.</para>
        /// <para>Otherwise return None.</para>
        /// </summary>
        /// <typeparam name="B">a type of value which returned option hold</typeparam>
        /// <param name="f">function to apply</param>
        /// <returns></returns>
        public abstract Option<B> Select<B>(Func<T, B> f);

        /// <summary>
        /// <para>Return the result of applying f to this Option's value if this Option is nonempty.</para>
        /// <para>Otherwise return None.</para>
        /// </summary>
        /// <typeparam name="B"></typeparam>
        /// <param name="f"></param>
        /// <returns></returns>
        public abstract Option<B> SelectMany<B>(Func<T, Option<B>> f);

        /// <summary>
        /// a method for LINQ
        /// </summary>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public abstract Option<C> SelectMany<B, C>(Func<T, Option<B>> f, Func<T, B, C> g);

        /// <summary>
        /// <para>Return value of Some</para>
        /// <para>Otherwise throw NullReferenceException.</para>
        /// </summary>
        /// <exception cref="System.NullReferenceException">if this option is empty</exception>
        /// <returns>value of Some</returns>
        public abstract T Get();

        /// <summary>
        /// <para>Return this Option's value if this Option is nonempty.</para>
        /// <para>Otherwise return givven value</para>
        /// </summary>
        /// <param name="value">値がなかった場合のデフォルト値</param>
        /// <returns>格納されている値</returns>
        public abstract T GetOrElse(T value);


        public abstract T GetOrElse(Func<T> f);

        public abstract Option<T> OrElse(Func<Option<T>> f);

        public abstract Option<T> Recover(Func<T> f);

        public abstract Option<T> Where(Func<T, bool> f);
        public abstract void ForEach(Action<T> f);

        public abstract Either<T, R> ToLeft<R>(Func<R> f);

        public abstract Either<L, T> ToRight<L>(Func<L> f);

        public abstract Either<L, T> ToEither<L>(Func<L> f);

        public abstract Try<T> ToTry();
        public abstract Try<T> ToTry(Exception ex);

        public abstract bool Exists(Func<T, bool> p);

        public abstract int Count(Func<T, bool> p);

        /// <summary>
        /// 値を Option モナドに格納して返します。
        /// </summary>
        /// <param name="value">Option に格納する値</param>
        /// <returns>値を格納した Option モナド</returns>
        public static Option<T> Return(T value)
        {
            return new Some<T>(value);
        }

        /// <summary>
        /// 値を持たない Option モナドを返します。
        /// </summary>
        /// <returns>値を持たない Option モナド</returns>
        public static Option<T> Fail()
        {
            return new None<T>();
        }
    }


    public static class Option
    {
        public static Option<T> Return<T>(T value)
        {
            return new Some<T>(value);
        }

        public static Option<T> Fail<T>()
        {
            return new None<T>();
        }
    }
}
