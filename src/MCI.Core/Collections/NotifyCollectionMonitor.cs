//-----------------------------------------------------------------------
// <copyright file="NotifyCollectionMonitor.cs" company="Miharu Communications Inc.">
//     © 2015 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public class NotifyCollectionMonitor<T> : IDisposable
        where T : class
    {
        private Func<T, Action> handler;
        private Dictionary<T, Action> removeCallbacks;

        private T[] previousCollection;
        private IList<T> targetCollection;
        private INotifyCollectionChanged notifyCollection;

        private bool disposed;


        public NotifyCollectionMonitor(IList<T> collection, Func<T, Action> handler)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            this.notifyCollection = collection as INotifyCollectionChanged;
            if (this.notifyCollection == null)
            {
                throw new ArgumentException("collection has been required to implement INotifyCollectionChanged");
            }


            // 増加イベントがあると
            // 削除時のコールバック関数が実行される
            // モニター自信が消えると、全ての削除用イベントが発行される
            this.disposed = false;

            this.handler = handler;
            this.removeCallbacks = new Dictionary<T, Action>();

            this.previousCollection = collection.ToArray();
            this.targetCollection = collection;

            foreach (var item in collection)
            {
                if (this.removeCallbacks.ContainsKey(item))
                {
                    continue;
                }

                this.removeCallbacks[item] = this.handler(item);
            }

            this.notifyCollection.CollectionChanged += this.targetCollection_CollectionChanged;
        }


        private void targetCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (var i in e.NewItems)
                {
                    var item = i as T;

                    if (item == null)
                    {
                        continue;
                    }

                    if (this.removeCallbacks.ContainsKey(item))
                    {
                        continue;
                    }

                    this.removeCallbacks[item] = this.handler(item);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (var i in e.OldItems)
                {
                    var item = i as T;

                    if (item == null)
                    {
                        continue;
                    }

                    if (this.removeCallbacks.ContainsKey(item))
                    {
                        this.removeCallbacks[item]();
                        this.removeCallbacks.Remove(item);
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // 前のだけにあるものは remove
                foreach (var item in this.previousCollection)
                {
                    if (!this.targetCollection.Contains(item))
                    {
                        if (this.removeCallbacks.ContainsKey(item))
                        {
                            this.removeCallbacks[item]();
                            this.removeCallbacks.Remove(item);
                        }
                    }
                }

                // 新しいのだけにあるものは add
                foreach (var item in this.targetCollection)
                {
                    if (!this.previousCollection.Contains(item))
                    {
                        if (this.removeCallbacks.ContainsKey(item))
                        {
                            continue;
                        }

                        this.removeCallbacks[item] = this.handler(item);
                    }
                }
            }

            this.previousCollection = this.targetCollection.ToArray();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.notifyCollection.CollectionChanged -= this.targetCollection_CollectionChanged;
                this.targetCollection = null;
                this.previousCollection = null;

                foreach (var key in this.removeCallbacks.Keys.ToArray())
                {
                    this.removeCallbacks[key]();
                    this.removeCallbacks.Remove(key);
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
