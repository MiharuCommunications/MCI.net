using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu
{
    public class Callback<T1> : IDisposable
    {
        private bool _disposed;
        private readonly List<Action<T1>> _callbacks;

        public Callback()
        {
            this._disposed = false;
            this._callbacks = new List<Action<T1>>();
        }


        public void Add(Action<T1> callback)
        {
            if (this._disposed)
            {
                ThrowHelper.ThrowObjectDisposedException("Callback");
            }

            this._callbacks.Add(callback);
        }

        public void Remove(Action<T1> callback)
        {
            if (this._disposed)
            {
                ThrowHelper.ThrowObjectDisposedException("Callback");
            }

            this._callbacks.Remove(callback);
        }

        public void Clear()
        {
            if (this._disposed)
            {
                ThrowHelper.ThrowObjectDisposedException("Callback");
            }

            this._callbacks.Clear();
        }

        public void Fire(T1 arg1)
        {
            foreach (var callback in this._callbacks)
            {
                callback(arg1);
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose 処理
                this._callbacks.Clear();
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
