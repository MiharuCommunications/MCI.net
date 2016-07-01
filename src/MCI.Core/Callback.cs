using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu
{
    public class Callback<T1> : IDisposable
    {
        private bool disposed;
        private List<Action<T1>> callbacks;

        public Callback()
        {
            this.disposed = false;
            this.callbacks = new List<Action<T1>>();
        }


        public void Add(Action<T1> callback)
        {
            if (this.disposed)
            {
                ThrowHelper.ThrowObjectDisposedException("Callback");
            }

            this.callbacks.Add(callback);
        }

        public void Remove(Action<T1> callback)
        {
            if (this.disposed)
            {
                ThrowHelper.ThrowObjectDisposedException("Callback");
            }

            this.callbacks.Remove(callback);
        }

        public void Clear()
        {
            if (this.disposed)
            {
                ThrowHelper.ThrowObjectDisposedException("Callback");
            }

            this.callbacks.Clear();
        }

        public void Fire(T1 arg1)
        {
            foreach (var callback in this.callbacks)
            {
                callback(arg1);
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose 処理
                this.callbacks.Clear();
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
