namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Disposable : IDisposable
    {
        private bool _disposed;

        private Action _callback;

        public Disposable(Action callback)
        {
            this._disposed = false;
            this._callback = callback;
        }


        private void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;

            if (disposing)
            {
                try
                {
                    this._callback();
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    this._callback = null;
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
