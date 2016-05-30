using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miharu.Utils
{
    public class EventBinder<Handler> : IDisposable
        where Handler : class
    {
        private bool disposed;
        public bool IsBinding;

        private Handler handler;
        private Action<Handler> bind;
        private Action<Handler> unbind;


        public EventBinder(Action<Handler> bind, Action<Handler> unbind, Handler handler)
        {
            if (bind == null)
            {
                throw new ArgumentNullException("bind");
            }

            if (unbind == null)
            {
                throw new ArgumentNullException("unbind");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this.disposed = false;
            this.IsBinding = false;

            this.bind = bind;
            this.unbind = unbind;
            this.handler = handler;
        }

        public void Bind()
        {
            if (!this.IsBinding)
            {
                this.bind(this.handler);
                this.IsBinding = true;
            }
        }


        public void UnBind()
        {
            if (this.IsBinding)
            {
                this.unbind(this.handler);
                this.IsBinding = false;
            }
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.IsBinding)
                {
                    this.unbind(this.handler);
                }
            }

            this.disposed = true;
        }
    }
}
