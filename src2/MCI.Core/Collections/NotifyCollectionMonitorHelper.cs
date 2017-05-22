//-----------------------------------------------------------------------
// <copyright file="NotifyCollectionMonitorHelper.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Collections
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public static class NotifyCollectionMonitorHelper
    {
        public static IDisposable Create<T>(IList<T> collection, Func<T, Action> handler)
            where T : class
        {
            return new NotifyCollectionMonitor<T>(collection, handler);
        }


        public static IDisposable ListenAllAndEverPropertyChanged<T>(this IList<T> collection, PropertyChangedEventHandler handler)
            where T : class, INotifyPropertyChanged
        {
            return new NotifyCollectionMonitor<T>(collection, item =>
            {
                item.PropertyChanged += handler;

                return () =>
                {
                    item.PropertyChanged -= handler;
                };
            });
        }
    }
}
