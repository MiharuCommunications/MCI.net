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
        private bool disposed;
        private object lockObject;
        private List<IDisposable> collection;


        public DisposableCollection()
        {
            this.disposed = false;
            this.lockObject = new object();

            this.collection = new List<IDisposable>();
        }


        public void Add(IDisposable disposable)
        {
            lock (this.lockObject)
            {
                if (this.disposed)
                {
                    ThrowHelper.ThrowObjectDisposedException("DisposableCollection");
                }

                this.collection.Add(disposable);
            }
        }


        private void Dispose(bool disposing)
        {
            lock (this.lockObject)
            {
                if (this.disposed)
                {
                    return;
                }

                this.disposed = true;

                if (disposing)
                {
                    foreach (var disposable in this.collection)
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
