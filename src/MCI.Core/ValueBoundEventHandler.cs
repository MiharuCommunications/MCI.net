//-----------------------------------------------------------------------
// <copyright file="ValueBoundEventHandler.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    /// <summary>
    /// <para>値を渡すことの出来るイベント用のイベントハンドラ</para>
    /// </summary>
    /// <typeparam name="TValue">渡す値の型</typeparam>
    /// <param name="sender">イベントを発生させたオブジェクト</param>
    /// <param name="e">値を含むイベント引数</param>
    public delegate void ValueBoundEventHandler<TValue>(object sender, ValueBoundEventArgs<TValue> e);
}
