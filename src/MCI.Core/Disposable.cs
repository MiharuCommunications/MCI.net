namespace Miharu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Disposable : IDisposable
    {
        private bool disposed;

        private Action callback;

        public Disposable(Action callback)
        {
            this.disposed = false;
            this.callback = callback;
        }


        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;

            if (disposing)
            {
                try
                {
                    this.callback();
                }
                catch
                {
                }
                finally
                {
                    this.callback = null;
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
