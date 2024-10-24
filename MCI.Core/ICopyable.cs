//-----------------------------------------------------------------------
// <copyright file="ICopyable.cs" company="Miharu Communications Inc.">
//     © 2024 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{

    public interface ICopyable<TValue>
        where TValue : ICopyable<TValue>
    {
        TValue Copy();
    }
}
