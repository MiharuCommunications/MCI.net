//-----------------------------------------------------------------------
// <copyright file="Option.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using Miharu.Monads;

    /// <summary>
    /// Option モナド
    /// </summary>
    /// <typeparam name="T">格納する値の型</typeparam>
    public abstract class Option<T>
    {
        /// <summary>
        /// 使用不可
        /// </summary>
        internal protected Option()
        {
        }

        /// <summary>
        /// 空かどうか
        /// </summary>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// 値が含まれるかどうか
        /// </summary>
        public abstract bool IsDefined { get; }


        public abstract Option<B> Select<B>(Func<T, B> f);
        public abstract Option<B> SelectMany<B>(Func<T, Option<B>> f);
        public abstract Option<C> SelectMany<B, C>(Func<T, Option<B>> f, Func<T, B, C> g);

        /// <summary>
        /// <para>値を取り出します。</para>
        /// <para>値がない場合例外 NullReferenceException が発生します。</para>
        /// </summary>
        /// <exception cref="System.NullReferenceException">値がない場合</exception>
        /// <returns>格納されている値</returns>
        public abstract T Get();

        /// <summary>
        /// <para>格納された値を取り出します。</para>
        /// <para>格納された値がない場合は、引数で渡した値が返ります。</para>
        /// </summary>
        /// <param name="value">値がなかった場合のデフォルト値</param>
        /// <returns>格納されている値</returns>
        public abstract T GetOrElse(T value);
        public abstract T GetOrElse(Func<T> f);

        public abstract Option<T> OrElse(Func<Option<T>> f);

        public abstract Option<T> Recover(Func<T> f);

        public abstract Option<T> Where(Func<T, bool> f);
        public abstract void ForEach(Action<T> f);

        public abstract Try<T> ToTry(Exception ex);

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
