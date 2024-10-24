//-----------------------------------------------------------------------
// <copyright file="IApplicable.cs" company="Miharu Communications Inc.">
//     © 2024 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    public interface IApplicable<TValue>
        where TValue : IApplicable<TValue>
    {
        void Apply(TValue other);
    }
}
