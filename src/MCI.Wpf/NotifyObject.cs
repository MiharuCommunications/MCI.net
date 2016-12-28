namespace Miharu.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class NotifyObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            var handler = Interlocked.CompareExchange(ref this.PropertyChanged, null, null);

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool SetProperty<T>(ref T store, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(store, value))
            {
                return false;
            }

            store = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
