//-----------------------------------------------------------------------
// <copyright file="DisposableCollection.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class DisposableCollection : IDisposable
    {
        private bool _disposed;
        private readonly object _lockObject;
        private readonly List<IDisposable> _collection;


        public DisposableCollection()
        {
            this._disposed = false;
            this._lockObject = new object();

            this._collection = new List<IDisposable>();
        }


        public void Add(IDisposable disposable)
        {
            lock (this._lockObject)
            {
                if (this._disposed)
                {
                    ThrowHelper.ThrowObjectDisposedException("DisposableCollection");
                }

                this._collection.Add(disposable);
            }
        }


        private void Dispose(bool disposing)
        {
            lock (this._lockObject)
            {
                if (this._disposed)
                {
                    return;
                }

                this._disposed = true;

                if (disposing)
                {
                    foreach (var disposable in this._collection)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
