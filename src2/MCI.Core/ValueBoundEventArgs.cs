//-----------------------------------------------------------------------
// <copyright file="ValueBoundEventArgs.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;

    /// <summary>
    /// 値を渡すことの出来るイベント用のイベント引数
    /// </summary>
    /// <typeparam name="T">渡す値の型</typeparam>
    public class ValueBoundEventArgs<T> : EventArgs
    {
        /// <summary>
        /// イベントで渡す値を与えて、引数を初期化します。
        /// </summary>
        /// <param name="value">イベントで渡す値</param>
        public ValueBoundEventArgs(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// イベントで渡す値
        /// </summary>
        public T Value { get; private set; }
    }
}
